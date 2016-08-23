using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.Model.Network;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class ServerAgent
    {
        private void handleConnect(string packet)
        {
            switch (packet.Substring(0, 2))
            {
                case "Ax": //Reception de la liste des serveurs
                    globals.isInQueue = false;
                    Thread.Sleep(200);
                    toClient.send(packet);
                    if (server_id == "")
                    {
                        string servlist = packet.Substring(5);
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

                    send("AX" + server_id);
                    break;

                case "AY": //Reception des infos de connexion du serveur choisi
                    Thread.Sleep(100);
                    string ip, port, coupe = "";
                    coupe = packet.Substring(3);
                    ip = coupe.Split(':')[0];
                    port = coupe.Split(':')[1].Split(';')[0];
                    GUID = coupe.Split(':')[1].Split(';')[1];

                    //On falsifie le packet
                    packet = packet.Replace(ip, "127.0.0.1");
                    packet = packet.Replace(port, "5546");

                    toClient.send(packet);
                    globals.clientGame = new ClientAgent("127.0.0.1", 5546, globals);
                    toClient = globals.clientGame;
                    //Deconnexion du serveur d'authentification, et connection au serveur de jeu 
                    globals.serverGame.GUID = GUID;
                    globals.serverGame.toClient = globals.clientGame;
                    globals.serverGame.Handle(ip, Int32.Parse(port));
                    toClient.toServ = globals.serverGame;
                    globals.serverConnect.close();
                    globals.serverConnect.shouldStop = true;
                    globals.writeToMainBox("Connexion au serveur de jeu...\n", "Green");
                    globals.writeToDebugBox("Connexion au serveur de jeu...\n", "Green");

                    Thread.Sleep(100);
                    break;
                case "AL"://Reception de la liste des persos
                    Thread.Sleep(100);
                    toClient.send(packet);
                    globals.writeToMainBox("Connecté au serveur de jeu\n", "Green");
                    globals.writeToDebugBox("Selection personnage\n", "Blue");
                    if (character_id == "")
                    {
                        string[] perso = packet.Split('|');

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
                    player.id = character_id;
                    send("AS" + character_id);
                    send("Af");

                    break;

                case "AS"://Reception des infos générales du perso (nom, lvl, inventaire...)
                    Thread.Sleep(100);
                    string[] player_stats = packet.Split('|');
                    player.pseudo = player_stats[2];
                    player.level = Int32.Parse(player_stats[3]);
                    globals.isConnected = true;
                    string[] inv = player_stats[10].Split(';');
                    foreach (string item in inv)
                    {
                        if (item != "")
                        {
                            string[] item_stats = item.Split('~');
                            int it_id = int.Parse(item_stats[1], System.Globalization.NumberStyles.HexNumber);
                            int it_qte = int.Parse(item_stats[2], System.Globalization.NumberStyles.HexNumber);
                            if (it_id < 10565)
                            {
                                player.inventaire.Add(new Item(item_stats[0], it_id, globals.objects[it_id], it_qte, (item_stats[3] != "")));
                            }
                        }

                    }
                    var msg = new InventoryChangedMessage() { inventory = player.inventaire };
                    Messenger.Default.Send<InventoryChangedMessage>(msg);
                    toClient.send(packet);
                    break;
                case "am":
                    break;

                default:
                    toClient.send(packet);
                    break;

            }
        }
    }
}
