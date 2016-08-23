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
    public partial class ServerAgent
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
                    
                    player.initiative = Int32.Parse(player_stats[7]);
                    player.prospection = Int32.Parse(player_stats[8]);
                    player.PA = Int32.Parse(player_stats[9].Split(',')[4]);
                    player.PM = Int32.Parse(player_stats[10].Split(',')[4]);

                    string[] force_stats = player_stats[11].Split(',');
                    string[] vita_stats = player_stats[12].Split(',');
                    string[] sagesse_stats = player_stats[13].Split(',');
                    string[] intel_stats = player_stats[16].Split(',');
                    string[] chance_stats = player_stats[14].Split(',');
                    string[] agi_stats = player_stats[15].Split(',');

                    player.force = Int32.Parse(force_stats[0]) + Int32.Parse(force_stats[1]) + Int32.Parse(force_stats[2]) + Int32.Parse(force_stats[3]);
                    player.vie = Int32.Parse(vita_stats[0]) + Int32.Parse(vita_stats[1]) + Int32.Parse(vita_stats[2]) + Int32.Parse(vita_stats[3]);
                    player.sagesse = Int32.Parse(sagesse_stats[0]) + Int32.Parse(sagesse_stats[1]) + Int32.Parse(sagesse_stats[2]) + Int32.Parse(sagesse_stats[3]);
                    player.intelligence = Int32.Parse(intel_stats[0]) + Int32.Parse(intel_stats[1]) + Int32.Parse(intel_stats[2]) + Int32.Parse(intel_stats[3]);
                    player.chance = Int32.Parse(chance_stats[0]) + Int32.Parse(chance_stats[1]) + Int32.Parse(chance_stats[2]) + Int32.Parse(chance_stats[3]);
                    player.agilite = Int32.Parse(agi_stats[0]) + Int32.Parse(agi_stats[1]) + Int32.Parse(agi_stats[2]) + Int32.Parse(agi_stats[3]);
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
                        globals.serverGame.send("DR318|259");//Acces a la banque
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
                case "SL":
                    string[] sorts = packet.Substring(2).Split(';');
                    foreach(string str in sorts)
                    {
                        if(str != "")
                        {
                            string[] sort = str.Split('~');
                            player.sorts.Add(new Model.Classes.Sort(Int32.Parse(sort[0]), globals.sorts[Int32.Parse(sort[0])], Int32.Parse(sort[1])));
                        }
                    }
                    var msg3 = new SpellsChangedMessage() { spells = player.sorts };
                    Messenger.Default.Send<SpellsChangedMessage>(msg3);
                    break;
                default:
                    //globals.writeToDebugBox("Packet inconnu\n", "Blue");
                    break;
            }
            toClient.send(packet);
        }
    }
}
