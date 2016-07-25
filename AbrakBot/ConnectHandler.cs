using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBot
{
    class ConnectHandler
    {
        private static string GUID = "";
        private static string server_id = Config.defaultServerId.HasValue ? Config.defaultServerId.Value.ToString(CultureInfo.InvariantCulture) : "";
        private static string character_id = Config.defaultCharacterId.HasValue ? Config.defaultCharacterId.Value.ToString(CultureInfo.InvariantCulture) : "";

        public static void ReceiveData(Queue<string> pck_queue)
        {
            if (pck_queue.Count != 0)
            {
                for (int i = 0; i < pck_queue.Count; i++)
                {
                    string Data = pck_queue.Dequeue();

                    if (Data != "")
                    {
                        Globals.writeToDebugBox("rcv : ", Color.Red);
                        Globals.writeToDebugBox(Data + "\n", Color.Black);
                        string donnée = "", pass = Config.mdp, key = "";
                        try
                        {
                            donnée = Data.Substring(0, 2);
                        }catch(NullReferenceException e)
                        {
                            Globals.writeToDebugBox("Aucune données recues", Color.Firebrick);
                        }
                        
                        
                        switch (donnée)
                        {
                            case "HC": //Debut de la connexion
                                Thread.Sleep(100);

                                key = Data.Substring(2, 32);
                                TCPPacketHandler.send("1.29.1");
                                TCPPacketHandler.send(Config.username + "\n" + CryptPassword(key, pass));
                                TCPPacketHandler.send("Af");
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
                                Globals.writeToMainBox("Connecté avec succès\n", Color.Green);
                                Globals.writeToDebugBox("Connecté avec succès\n", Color.Green);
                                Globals.writeToMainBox("Serveur choisi : n°" + server_id + "\n", Color.Green);
                                TCPPacketHandler.send("AX" + server_id);
                                break;

                            case "Ad": //?
                                Thread.Sleep(100);

                                TCPPacketHandler.send("Ax");
                                break;

                            case "AY": //Reception des infos de connexion du serveur choisi
                                Thread.Sleep(100);
                                string ip, port, coupe = "";
                                coupe = Data.Substring(3);
                                ip = coupe.Split(':')[0];
                                port = coupe.Split(':')[1].Split(';')[0];
                                GUID = coupe.Split(':')[1].Split(';')[1];
                                Globals.writeToDebugBox("IP : " + ip + "\n", Color.Purple);
                                Globals.writeToDebugBox("Port : " + port + "\n", Color.Purple);
                                Globals.writeToDebugBox("GUID : " + GUID + "\n", Color.Purple);

                                TCPPacketHandler.close();
                                Globals.writeToMainBox("Connexion au serveur de jeu...\n", Color.Green);
                                Globals.writeToDebugBox("Connexion au serveur de jeu...\n", Color.Green);
                                TCPPacketHandler.Handle(ip, Int32.Parse(port));
                             
                                Thread.Sleep(100);
                                break;

                            case "HG"://?
                                Thread.Sleep(100);

                                TCPPacketHandler.send("AT" + GUID);
                                break;

                            case "AT"://?
                                Thread.Sleep(100);

                                TCPPacketHandler.send("Ak0");
                                TCPPacketHandler.send("AV");
                                break;

                            case "AV"://?
                                Thread.Sleep(100);

                                TCPPacketHandler.send("Agfr");
                                TCPPacketHandler.send("AL");
                                TCPPacketHandler.send("Af");
                                break;

                            case "AL"://Reception de la liste des persos
                                Thread.Sleep(100);
                                Globals.writeToMainBox("Connecté au serveur de jeu\n", Color.Green);
                                Globals.writeToDebugBox("Selection personnage\n", Color.Blue);
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
                                TCPPacketHandler.send("AS" + character_id);
                                TCPPacketHandler.send("Af");

                                break;

                            case "AS"://Reception des infos générales du perso (nom, lvl, inventaire...)
                                Thread.Sleep(100);
                                string[] player_stats = Data.Split('|');
                                Player.pseudo = player_stats[2];
                                Player.level = Int32.Parse(player_stats[3]);
                                Globals.isConnected = true;
                                string[] inv = player_stats[10].Split(';');
                                foreach (string item in inv)
                                {
                                    if(item != "")
                                    {
                                        string[] item_stats = item.Split('~');
                                        int it_id = int.Parse(item_stats[1], System.Globalization.NumberStyles.HexNumber);
                                        if (it_id < 10565)
                                        {
                                            Player.inventaire.Add(new Item(item_stats[1], Globals.objects[it_id], Int32.Parse(item_stats[2])));
                                        }
                                    }
                                    
                                }
                                break;
                            case "Al": //Merde, un truc s'est mal passé
                                switch(Data.Substring(2, 2))
                                {
                                    case "Ef":
                                        Globals.writeToMainBox("Mot de passe incorrect\n", Color.Firebrick);
                                        break;
                                    case "Eb":
                                        Globals.writeToMainBox("Votre compte a été banni\n", Color.Firebrick);
                                        break;
                                    case "En":
                                        Globals.writeToMainBox("La connexion ne s'est pas terminée\n", Color.Firebrick);
                                        break;
                                    case "Ea":
                                        Globals.writeToMainBox("Vous êtes déjà en cours de connexion\n", Color.Firebrick);
                                        break;
                                    case "Ec":
                                        Globals.writeToMainBox("Vous êtes déjà connecté au serveur de jeu\n", Color.Firebrick);
                                        break;
                                    case "Ed":
                                        Globals.writeToMainBox("Ce compte était déjà en ligne, vous venez de le déconnecter\n", Color.Firebrick);
                                        break;
                                    case "Ew":
                                        Globals.writeToMainBox("Serveur plein\n", Color.Firebrick);
                                        break;
                                    default:
                                        Globals.writeToMainBox("Une erreur inconnue s'est produite lors de la connexion\n", Color.Firebrick);
                                        break;
                                }
                                TCPPacketHandler.close();
                                Globals.disconnect();
                                break;
                           
                            default:
                                Globals.writeToDebugBox("Case inconnu\n", Color.Blue);
                                break;

                        }
                    }
                }
            }
        }

        static string CryptPassword(string Key, string Password)
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

        static string CryptIp(string sExtraData)
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
