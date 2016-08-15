using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class PacketDispatcher
    {
        private void handleDivers(string packet)
        {
            switch(packet.Substring(0, 2))
            {
                case "As"://Infos détaillées sur le perso
                    Thread.Sleep(100);
                    string[] player_stats = packet.Substring(2).Split('|');
                    string[] xp_stats = player_stats[0].Split(',');
                    player.xp_bas = Int32.Parse(xp_stats[1]);
                    player.xp_max = Int32.Parse(xp_stats[2]);
                    player.xp = Int32.Parse(xp_stats[0]);

                    player.kamas = Int32.Parse(player_stats[1]);
                    string[] pdv_stats = player_stats[5].Split(',');
                    player.pdv_max = Int32.Parse(pdv_stats[1]);
                    player.pdv = Int32.Parse(pdv_stats[0]);

                    string[] en_stats = player_stats[6].Split(',');
                    player.energie_max = Int32.Parse(en_stats[1]);
                    player.energie = Int32.Parse(en_stats[0]);


                    break;
                case "AS"://Reception des infos générales du perso (nom, lvl, inventaire...)
                    Thread.Sleep(100);
                    string[] player_stats2 = packet.Split('|');
                    player.pseudo = player_stats2[2];
                    player.level = Int32.Parse(player_stats2[3]);
                    globals.isConnected = true;
                    string[] inv = player_stats2[10].Split(';');
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
                    break;
                case "al"://?
                    globals.game.send("GC1");
                    break;
                case "fC"://Nombre de combats sur la map actuelle //TODO : A REMPLIR
                    globals.game.send("BD");
                    break;
                case "rp"://Pas trop sur, je crois que c'est un genre de ping régulier
                    if (packet.Substring(2, 3) == "ong")
                    {
                        globals.game.send("rpong");
                    }
                    break;

                case "am":
                    //Aucune idée de ce que c'est
                    break;
                case "DC"://Dialog create 
                    globals.isInDialog = true;
                    break;
                case "DQ"://Question pnj
                    int idQuestion = Int32.Parse(packet.Substring(2).Split(';')[0]);
                    if (idQuestion == 318)//Banque
                    {
                        globals.game.send("DR318|259");//Acces a la banque
                    }
                    break;
                case "DV":
                    //Lancement echange
                    globals.isInDialog = false;
                    globals.isInExchange = true;
                    break;
                case "IQ":
                    if(packet.Substring(2).Split('|')[0] == player.id)
                    {
                        var msg2 = new HarvestedResourceMessage() { qte = Int32.Parse(packet.Substring(2).Split('|')[1]) };
                        Messenger.Default.Send<HarvestedResourceMessage>(msg2);
                    }
                    break;
                default:
                    globals.writeToDebugBox("Packet inconnu\n", "Blue");
                    break;
            }
        }
    }
}
