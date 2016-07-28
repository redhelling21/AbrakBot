using AbrakBotWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Services
{
    public class ConnectHandler
    {
        public string GUID = "";
        private string server_id = Config.defaultServerId.HasValue ? Config.defaultServerId.Value.ToString(CultureInfo.InvariantCulture) : "";
        private string character_id = Config.defaultCharacterId.HasValue ? Config.defaultCharacterId.Value.ToString(CultureInfo.InvariantCulture) : "";

        private Globals globals;
        private Player player;

        public ConnectHandler(Globals globals, Player player)
        {
            this.globals = globals;
            this.player = player;
        }

        //Traite un packet recu
        public void ReceiveData(Queue<string> pck_queue)
        {
            if (pck_queue.Count != 0)
            {
                for (int i = 0; i < pck_queue.Count; i++)
                {
                    string Data = pck_queue.Dequeue();

                    if (Data != "")
                    {
                        //Signifie l'arrivee d'un packet
                        globals.writeToDebugBox("rcv : ", "Red");
                        globals.writeToDebugBox(Data + "\n", "Black");
                        string donnée = "", pass = Config.mdp, key = "";
                        //Recupere les deux premieres lettres du packet.
                        try
                        {
                            donnée = Data.Substring(0, 2);
                        }catch(NullReferenceException e)
                        {
                            globals.writeToDebugBox("Aucune données recues", "Firebrick");
                        }
                        
                        
                        switch (donnée)
                        {
                            case "HC": //Debut de la connexion
                                Thread.Sleep(100);
                                key = Data.Substring(2, 32);
                                globals.connect.send("1.29.1");
                                globals.connect.send(Config.username + "\n" + CryptPassword(key, pass));
                                globals.connect.send("Af");
                                break;

                            case "Ax": //Reception de la liste des serveurs
                                Thread.Sleep(2000);
                                if (server_id == "")
                                {
                                    string servlist = Data.Substring(5);
                                    string[] list = servlist.Split('|');
                                    Console.WriteLine("Liste des serveurs :");
                                    foreach (string serv in list)
                                    {
                                        Console.WriteLine("Serveur " + serv.Split(',')[0] + " : " + serv.Split(',')[1] + " personnage(s)");
                                    }
                                    Console.WriteLine("Quel serveur choisir ?");
                                    server_id = Console.ReadLine();
                                }
                                globals.writeToMainBox("Connecté avec succès\n", "Green");
                                globals.writeToDebugBox("Connecté avec succès\n", "Green");
                                globals.writeToMainBox("Serveur choisi : n°" + server_id + "\n", "Green");
                                globals.connect.send("AX" + server_id);
                                break;

                            case "Ad": //?
                                Thread.Sleep(100);

                                globals.connect.send("Ax");
                                break;

                            case "AY": //Reception des infos de connexion du serveur choisi
                                Thread.Sleep(100);
                                string ip, port, coupe = "";
                                coupe = Data.Substring(3);
                                ip = coupe.Split(':')[0];
                                port = coupe.Split(':')[1].Split(';')[0];
                                GUID = coupe.Split(':')[1].Split(';')[1];
                                globals.writeToDebugBox("IP : " + ip + "\n", "Purple");
                                globals.writeToDebugBox("Port : " + port + "\n", "Purple");
                                globals.writeToDebugBox("GUID : " + GUID + "\n", "Purple");
                                //Deconnexion du serveur d'authentification, et connection au serveur de jeu 
                                globals.game = new TCPPacketHandler(globals, player);
                                globals.game.connectHandler.GUID = GUID;
                                globals.connect.close();
                                globals.connect.shouldStop = true;
                                globals.writeToMainBox("Connexion au serveur de jeu...\n", "Green");
                                globals.writeToDebugBox("Connexion au serveur de jeu...\n", "Green");
                                globals.game.Handle(ip, Int32.Parse(port));
                             
                                Thread.Sleep(100);
                                break;

                            case "HG"://?
                                Thread.Sleep(100);

                                globals.game.send("AT" + GUID);
                                break;

                            case "AT"://?
                                Thread.Sleep(100);

                                globals.game.send("Ak0");
                                globals.game.send("AV");
                                break;

                            case "AV"://?
                                Thread.Sleep(100);

                                globals.game.send("Agfr");
                                globals.game.send("AL");
                                globals.game.send("Af");
                                break;

                            case "AL"://Reception de la liste des persos
                                Thread.Sleep(100);
                                globals.writeToMainBox("Connecté au serveur de jeu\n", "Green");
                                globals.writeToDebugBox("Selection personnage\n", "Blue");
                                if (character_id == "")
                                {
                                    string[] perso = Data.Split('|');

                                    foreach (string perso_indiv in perso)
                                    {
                                        string[] perso1 = perso_indiv.Split(';');
                                        if (!(perso1.Length < 3))
                                        {
                                            Console.WriteLine(perso1[0] + " - " + perso1[1] + " Niv." + perso1[2]);
                                        }
                                    }
                                    Console.WriteLine("Quel perso ?");
                                    character_id = Console.ReadLine();
                                }
                                globals.game.send("AS" + character_id);
                                globals.game.send("Af");

                                break;

                            case "AS"://Reception des infos générales du perso (nom, lvl, inventaire...)
                                Thread.Sleep(100);
                                string[] player_stats = Data.Split('|');
                                player.pseudo = player_stats[2];
                                player.level = Int32.Parse(player_stats[3]);
                                globals.isConnected = true;
                                string[] inv = player_stats[10].Split(';');
                                foreach (string item in inv)
                                {
                                    if(item != "")
                                    {
                                        string[] item_stats = item.Split('~');
                                        int it_id = int.Parse(item_stats[1], System.Globalization.NumberStyles.HexNumber);
                                        int it_qte = int.Parse(item_stats[2], System.Globalization.NumberStyles.HexNumber);
                                        if (it_id < 10565)
                                        {
                                            player.inventaire.Add(new Item(item_stats[1], globals.objects[it_id], it_qte));
                                        }
                                    }
                                    
                                }
                                break;
                            case "Al": //Merde, un truc s'est mal passé
                                switch(Data.Substring(2, 2))
                                {
                                    case "Ef":
                                        globals.writeToMainBox("Mot de passe incorrect\n", "Firebrick");
                                        break;
                                    case "Eb":
                                        globals.writeToMainBox("Votre compte a été banni\n", "Firebrick");
                                        break;
                                    case "En":
                                        globals.writeToMainBox("La connexion ne s'est pas terminée\n", "Firebrick");
                                        break;
                                    case "Ea":
                                        globals.writeToMainBox("Vous êtes déjà en cours de connexion\n", "Firebrick");
                                        break;
                                    case "Ec":
                                        globals.writeToMainBox("Vous êtes déjà connecté au serveur de jeu\n", "Firebrick");
                                        break;
                                    case "Ed":
                                        globals.writeToMainBox("Ce compte était déjà en ligne, vous venez de le déconnecter\n", "Firebrick");
                                        break;
                                    case "Ew":
                                        globals.writeToMainBox("Serveur plein\n", "Firebrick");
                                        break;
                                    case "K0":
                                        break;
                                    default:
                                        globals.writeToMainBox("Une erreur inconnue s'est produite lors de la connexion\n", "Firebrick");
                                        break;
                                }
                                if(Data.Substring(2, 2) != "K0")
                                {
                                    globals.connect.close();
                                    globals.connect.shouldStop = true;
                                    globals.disconnect();
                                }
                                
                                break;
                           
                            default:
                                globals.writeToDebugBox("Case inconnu\n", "Blue");
                                break;

                        }
                    }
                }
            }
        }

        private string CryptPassword(string Key, string Password)
        {
            char[] chArray = new char[] {
                   'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
                   'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F',
                   'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
                   'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'};
            string str = "#1";
            for (int i = 0; i < Password.Length; i++)
            {
                char ch = Password[i];
                char ch2 = Key[i];
                int num2 = ch / '\x0010';
                int num3 = ch % '\x0010';
                int index = (num2 + ch2) % chArray.Length;
                int num5 = (num3 + ch2) % chArray.Length;
                str = str + chArray[index] + chArray[num5];
            }
            return str;
        }

        private string CryptIp(string sExtraData)
        {
            string loc8, loc9, loc7, loc5 = "";
            loc8 = sExtraData.Substring(0, 8);
            loc9 = sExtraData.Substring(8, 3);
            loc7 = sExtraData.Substring(11);
            int loc12, loc13, loc10;
            for (int loc11 = 0; loc11 < 8; loc11 += 2)
            {
                byte code_ascii = (byte)loc8[loc11];
                loc12 = code_ascii - 48;
                byte code_ascii2 = (byte)loc8[loc11 + 1];
                loc13 = code_ascii2 - 48;
                loc10 = (((loc12 & 15) << 4) | (loc13 & 15));
                loc5 += "." + loc10;
            } // end while
            loc5 = loc5.Substring(1);
            return loc5;
        }
    }
}
