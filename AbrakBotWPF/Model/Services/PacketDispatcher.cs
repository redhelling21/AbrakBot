﻿using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Services
{
    public class PacketDispatcher
    {
        private Globals globals;
        private Player player;

        public PacketDispatcher(Globals globals, Player player)
        {
            this.globals = globals;
            this.player = player;
        }

        public void ReceiveData(Queue<string> pck_queue)
        {
            if (pck_queue.Count != 0)
            {
                for (int i = 0; i < pck_queue.Count; i++)
                {
                    string Data = pck_queue.Dequeue();
                    string donnee = "";
                    if (Data != null && Data != "")
                    {
                        //On affiche pas les packets am, il y en a trop
                        if (Data.Substring(0, 2) != "am")
                        {
                            globals.writeToDebugBox("rcv : ", "Red");
                            globals.writeToDebugBox(Data + "\n", "Black");
                        }
                        donnee = Data.Substring(0, 2);
                        switch (donnee)
                        {
                            case "GD": //Concerne la map
                                Thread.Sleep(100);
                                string subcat = Data.Substring(2, 1);
                                switch (subcat)
                                {
                                    case "M"://Reception des infos sur la map actuelle
                                        if (!globals.isInGame)
                                        {
                                            globals.isInGame = true;
                                            globals.writeToMainBox("En jeu.\n", "Green");
                                            globals.writeToDebugBox("En jeu.\n", "Green");
                                        }
                                        globals.mapLoaded = false;
                                        globals.isMoving = false;
                                        string[] map_datas = Data.Split('|');
                                        globals.currentMapId = Int32.Parse(map_datas[1]);
                                        string indice = map_datas[2];
                                        string clef = map_datas[3];
                                        globals.mapHandler.LoadMap(globals.currentMapId, indice, clef);
                                        globals.game.send("GI");
                                        break;
                                    case "F"://Reception des infos sur l'etat des ressources de la map
                                        string[] res_datas = Data.Split('|');
                                        if (res_datas[1].Split(';')[2] == "0")
                                        {
                                            if (globals.actualResources.ContainsKey(Int32.Parse(res_datas[1].Split(';')[0]))) {
                                                globals.actualResources.Remove(Int32.Parse(res_datas[1].Split(';')[0]));
                                            }
                                        }else
                                        {
                                            int caseRepop = Int32.Parse(res_datas[1].Split(';')[0]);
                                            if (!globals.actualResources.ContainsKey(caseRepop) && globals.idResourcesTranslate.ContainsKey(globals.mapDataActuelle[caseRepop].layerObject2Num))
                                            {
                                                globals.actualResources.Add(caseRepop, new Ressource(globals.idResourcesTranslate[globals.mapDataActuelle[caseRepop].layerObject2Num], caseRepop, globals.ressources[globals.mapDataActuelle[caseRepop].layerObject2Num], true));
                                            }
                                        }
                                        break;
                                    case "K":
                                        globals.mapLoaded = true;
                                        break;
                                }
                                break;
                            case "GM"://Quelque chose a bouge sur la map (monstre, joueur...)

                                globals.moveHandler.handleMove(Data);
                                break;
                            case "Ow"://Infos sur les pods
                                Thread.Sleep(100);
                                string[] elems = Data.Substring(2).Split('|');
                                player.pods_max = Int32.Parse(elems[1]);
                                player.pods = Int32.Parse(elems[0]);
                                if(Math.Round(((float)(player.pods) / player.pods_max) * 100) > globals.podsPercentLimit)
                                {
                                    globals.writeToMainBox("Inventaire plein à plus de " + globals.podsPercentLimit + "%. Retour à la banque.\n", "FireBrick");
                                    globals.needsBank = true;
                                }
                                break;
                            case "As"://Infos détaillées sur le perso
                                Thread.Sleep(100);
                                string[] player_stats = Data.Substring(2).Split('|');
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
                                string[] player_stats2 = Data.Split('|');
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
                            case "JS"://Infos sur les metiers du perso
                                string[] metierArray = Data.Split('|');
                                player.harvestables.Clear();
                                player.metiers.Clear();
                                int index = 0;
                                foreach(string metier in metierArray)
                                {
                                    if( index != 0)
                                    {
                                        string[] hres = metier.Split(';')[1].Split(',');
                                        player.metiers.Add(new Metier());
                                        player.metiers[index - 1].id = Int32.Parse(metier.Split(';')[0]);
                                        //TODO : changer pour avoir le vrai nom de chaque metier
                                        player.metiers[index - 1].nom = "Bucheron";
                                        foreach(string str in hres)
                                        {
                                            if (globals.idResourcesTranslate.ContainsValue(Int32.Parse(str.Split('~')[0])))
                                            {
                                                player.harvestables.Add(Int32.Parse(str.Split('~')[0]));
                                            }
                                        }
                                    }
                                    index++;
                                }
                                globals.updateMetiers();
                                break;
                            case "JX"://Infos sur l'xp des metiers du perso
                                string[] metierXPArray = Data.Split('|');
                                int index2 = 0;
                                foreach (string metier in metierXPArray)
                                {
                                    if (index2 != 0)
                                    {
                                        string[] xpstats = metier.Split(';');
                                        foreach (Metier m in player.metiers)
                                        {
                                            if(m.id == Int32.Parse(xpstats[0]))
                                            {
                                                m.level = Int32.Parse(xpstats[1]);
                                                m.xp_min = Int32.Parse(xpstats[2]);
                                                m.xp = Int32.Parse(xpstats[3]);
                                                m.xp_max = Int32.Parse(xpstats[4]);
                                            }
                                        }
                                    }
                                    index2++;
                                }
                                globals.updateMetiers();
                                break;
                            case "GA"://Autorisation de se déplacer

                                if ((Data == "GA;0"))
                                {
                                    //Rien

                                }else if (Data.Substring(0, 5) == "GA;4;")
                                {
                                    string pate = Data.Split(';')[3];
	                                globals.caseActuelle = Int32.Parse(Data.Split(',')[0]);
                                    globals.writeToDebugBox("(Via GA;4) CaseActuelle : " + Int32.Parse(Data.Split(',')[0]) + "\n", "Orange");

                                }
                                else if (Data.Substring(0, 6) == "GA;900")
                                {
	                                //.sock.Envoyer("GA902" + Gettok(packet, ";", 3));

                                }
                                else if (Data.Substring(0, 7) == "GA1;501")
                                {

                                    string pate = Data.Split(';')[3];
                                    int tempsDeRecolte = Convert.ToInt32(pate.Split(',')[1]);
                                    HarvestHandler handler = new HarvestHandler(globals);
                                    handler.WaitRecolte(tempsDeRecolte);
                                }
                                else if (Data.Substring(0, 7) == "GA0;501")
                                {

                                    string pate = Data.Split(';')[3];
                                    int caseRecolte = Convert.ToInt32(pate.Split(',')[0]);
                                    globals.actualResources.Remove(caseRecolte);
                                    globals.isHarvesting = false;
                                }
                                else if (Data.Substring(0, 6) == "GA0;1;")
                                {

                                    if (!globals.isFighting)
                                    {
                                        string cherche = Data.Split(';')[3];
                                        int id = Int32.Parse(Data.Split(';')[2]);
                                        if (id == Config.defaultCharacterId)
                                        {
                                            cherche = cherche.Substring(cherche.Length - 2);
                                            int trouve = -1;

                                            for (int j = 0; j <= 1024; j++)
                                            {
                                                if ((globals.cases[j] == cherche))
                                                {
                                                    trouve = j;
                                                    j = 1025;
                                                }
                                            }

                                            if ((trouve != -1))
                                            {
                                                globals.caseActuelle = trouve;
                                                globals.writeToDebugBox("(via GA0;1) CaseActuelle : " + trouve + "\n", "Orange");
                                            }
                                        }
                                    }

                                }

                                break;
                            case "rp"://Pas trop sur, je crois que c'est un genre de ping régulier
                                if (Data.Substring(2, 3) == "ong")
                                {
                                    globals.game.send("rpong");
                                }
                                break;
                            case "cM"://Message chat //TODO : A TERMINER AVEC L'INSERTION D'ITEMS
                                switch (Data.Substring(2, 1))
                                {
                                    case "K":
                                        string sub = Data.Substring(3, 1);
                                        string[] parts = Data.Split('|');
                                        switch (sub)
                                        {
                                            case ":":
                                                globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] (Commerce) " + parts[2] + " : " + parts[3] + "\n", "SaddleBrown");
                                                break;
                                            case "?":
                                                globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] (Recrutement) " + parts[2] + " : " + parts[3] + "\n", "LightGray");
                                                break;
                                            case "F":
                                                globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] de " + parts[2] + " : " + parts[3] + "\n", "DeepSkyBlue");
                                                break;
                                            case "T":
                                                globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] a " + parts[2] + " : " + parts[3] + "\n", "DeepSkyBlue");
                                                break;
                                            default:
                                                globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] " + parts[2] + " : " + parts[3] + "\n", "Black");
                                                break;
                                        }
                                        break;
                                    case "E":
                                        if (Data.Substring(3, 1) == "f")
                                        {
                                            globals.writeToMainBox("Le joueur n'existe pas ou n'est pas en ligne", "Firebrick");
                                        }
                                        break;
                                }

                                break;
                            case "am":
                                //Aucune idée de ce que c'est
                                break;
                            case "DC"://Dialog create 
                                globals.isInDialog = true;
                                break;
                            case "DQ"://Question pnj
                                int idQuestion = Int32.Parse(Data.Substring(2).Split(';')[0]);
                                if(idQuestion == 318)//Banque
                                {
                                    globals.game.send("DR318|259");//Acces a la banque
                                }
                                break;
                            case "EC":
                                //Type echange
                                break;
                            case "DV":
                                //Lancement echange
                                globals.isInDialog = false;
                                globals.isInExchange = true;
                                break;
                            case "Es"://Echange d'un item ok
                                //
                                break;
                            case "EL"://Contenu banque
                                //Inutile pour l'instant
                                break;
                            case "OR"://item remove
                                string idUnique = Data.Substring(2);
                                Item toDelete = null;
                                foreach (Item item in player.inventaire)
                                {
                                    if(item.uniqueID == idUnique)
                                    {
                                        toDelete = item;
                                    }
                                }
                                player.inventaire.Remove(toDelete);
                                globals.removingItem = false;
                                break;
                            case "EV"://fin echange
                                globals.isInExchange = false;
                                break;
                            default:
                                globals.writeToDebugBox("Case inconnu\n", "Blue");
                                break;

                        }
                    }
                    else
                    {
                        globals.writeToDebugBox("Aucune données reçues\n", "Firebrick");
                    }
                }
            }
        }
    }
}
