using AbrakBotWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Services
{
    public class TrajetHandler
    {
        private Thread thread;
        private string actualCoords;
        private Globals globals;
        private Player player;

        public TrajetHandler(Globals glob, Player player)
        {
            this.globals = glob;
            this.player = player;
        }

        public void handleTrajet()
        {
            
            thread = new System.Threading.Thread(new System.Threading.ThreadStart(run));
            thread.Name = "TrajetThread";
            thread.IsBackground = true;
            thread.Start();
            
        }

        //Gere un trajet
        private void run()
        {
            while (globals.isRunning)
            {
                Thread.Sleep(500);
                //On attend que le perso ne bouge plus
                globals.writeToDebugBox("waiting for not moving\n", "Navy");
                while (globals.isMoving || !globals.mapLoaded)
                {
                    Thread.Sleep(200);
                }
                globals.writeToDebugBox("end waiting\n", "Navy");
                //On cherche a savoir ce qu'il faut faire sur la map actuelle (bouger, recolter, ou combattre)
                
                actualCoords = globals.maps[globals.currentMapId].Replace(" ", string.Empty);
                string actualCoordsOpt = "mapid(" + globals.currentMapId + ")";
                if (globals.needsBank)
                {
                    if (globals.listBanque.ContainsKey(actualCoords) || globals.listBanque.ContainsKey(actualCoordsOpt))
                    {
                        if (globals.listBanque.ContainsKey(actualCoordsOpt))
                        {
                            actualCoords = actualCoordsOpt;
                        }
                        List<int> commandes = globals.listBanque[actualCoords];
                        Random rnd = new Random();
                        int index = (int)Math.Round((double)rnd.Next(commandes.Count));

                        switch (commandes[index])
                        {
                            case 10001:
                                globals.moveHandler.SeDeplacerMap(globals.tpHaut);
                                break;
                            case 10002:
                                globals.moveHandler.SeDeplacerMap(globals.tpBas);
                                break;
                            case 10003:
                                globals.moveHandler.SeDeplacerMap(globals.tpGauche);
                                break;
                            case 10004:
                                globals.moveHandler.SeDeplacerMap(globals.tpDroite);
                                break;
                            case 9999://Banque
                                globals.serverGame.send("DC-1");
                                emptyBag();
                                globals.needsBank = false;
                                break;
                            default:
                                globals.moveHandler.SeDeplacerMap(commandes[index]);
                                break;
                        }

                    }
                }
                else
                {
                    if (globals.listFight.ContainsKey(actualCoords) || globals.listFight.ContainsKey(actualCoordsOpt))
                    {
                        if (globals.listFight.ContainsKey(actualCoordsOpt))
                        {
                            actualCoords = actualCoordsOpt;
                        }
                        
                        Random rnd = new Random();
                        while (globals.monsterGroups.Count > 0)
                        {
                            Thread.Sleep(500);
                            if (globals.needsBank)
                            {
                                break;
                            }
                            int ind = (int)Math.Round((double)rnd.Next(globals.monsterGroups.Count));
                            MonsterGroup grp = globals.monsterGroups[ind];
                            if (grp.level > globals.lvlMaxMonstres || grp.level < globals.lvlMinMonstres || grp.nbMonstres > globals.nbMaxMonstres || grp.nbMonstres < globals.nbMinMonstres)
                            {
                                globals.monsterGroups.Remove(grp);
                            }
                            else
                            {
                                if (globals.caseActuelle != grp.caseGroupe & grp.caseGroupe != -1)
                                {
                                    globals.writeToMainBox("(Info) Monstre trouvé : " + grp.nbMonstres + ", niveau total : " + grp.level + "\n", "Black");
                                    globals.moveHandler.SeDeplacer(grp.caseGroupe);
                                    Thread.Sleep(4000);
                                    while (globals.isFighting)
                                    {
                                        Thread.Sleep(2000);
                                    }
                                    if (globals.monsterGroups.Contains(grp))
                                    {
                                        globals.monsterGroups.Remove(grp);
                                    }
                                }else
                                {
                                    globals.monsterGroups.Remove(grp);
                                }
                            }

                        }
                        globals.writeToDebugBox("Changement de map\n", "LimeGreen");
                        List<int> commandes = globals.listFight[actualCoords];
                        Random rnd2 = new Random();
                        int index = (int)Math.Round((double)rnd2.Next(commandes.Count));

                        switch (commandes[index])
                        {
                            case 10001:
                                globals.moveHandler.SeDeplacerMap(globals.tpHaut);
                                break;
                            case 10002:
                                globals.moveHandler.SeDeplacerMap(globals.tpBas);
                                break;
                            case 10003:
                                globals.moveHandler.SeDeplacerMap(globals.tpGauche);
                                break;
                            case 10004:
                                globals.moveHandler.SeDeplacerMap(globals.tpDroite);
                                break;
                            default:
                                globals.moveHandler.SeDeplacerMap(commandes[index]);
                                break;
                        }

                    }
                    else if (globals.listHarvest.ContainsKey(actualCoords) || globals.listHarvest.ContainsKey(actualCoordsOpt))
                    {
                        if (globals.listHarvest.ContainsKey(actualCoordsOpt))
                        {
                            actualCoords = actualCoordsOpt;
                        }
                        
                        Random rnd = new Random();
                        HarvestHandler handler = new HarvestHandler(globals);
                        int count_timeout = 0;
                        while (globals.actualResources.Count > 0)
                        {
                            if (globals.needsBank)
                            {
                                break;
                            }
                            int ind = (int)Math.Round((double)rnd.Next(globals.actualResources.Count));
                            KeyValuePair<int, Ressource> entry = globals.actualResources.ElementAt(ind);
                            if (!player.harvestables.Contains(entry.Value.id) || entry.Key == 0)
                            {
                                globals.actualResources.Remove(entry.Key);
                            }else
                            {
                                handler.Recolter(entry.Key);
                                globals.isHarvesting = true;
                                int count = 0;
                                while (globals.isHarvesting)
                                {
                                    Thread.Sleep(200);
                                    count++;
                                    if (count >= 150)
                                    {
                                        globals.writeToMainBox("Timeout temps de recolte\n", "FireBrick");
                                        globals.isHarvesting = false;
                                        count_timeout++;
                                    }
                                }
                                Thread.Sleep(200);
                            }
                            if(count_timeout > 3)
                            {
                                break;
                            }
                            
                        }
                        globals.writeToDebugBox("Changement de map\n", "LimeGreen");
                        List<int> commandes = globals.listHarvest[actualCoords];
                        bool isSelected = false;
                        Random rnd2 = new Random();
                        int index = (int)Math.Round((double)rnd2.Next(commandes.Count));

                        switch (commandes[index])
                        {
                            case 10001:
                                globals.moveHandler.SeDeplacerMap(globals.tpHaut);
                                break;
                            case 10002:
                                globals.moveHandler.SeDeplacerMap(globals.tpBas);
                                break;
                            case 10003:
                                globals.moveHandler.SeDeplacerMap(globals.tpGauche);
                                break;
                            case 10004:
                                globals.moveHandler.SeDeplacerMap(globals.tpDroite);
                                break;
                            default:
                                globals.moveHandler.SeDeplacerMap(commandes[index]);
                                break;
                        }
                    }
                    else if (globals.listMovements.ContainsKey(actualCoords) || globals.listMovements.ContainsKey(actualCoordsOpt))
                    {
                        if (globals.listMovements.ContainsKey(actualCoordsOpt))
                        {
                            actualCoords = actualCoordsOpt;
                        }
                        List<int> commandes = globals.listMovements[actualCoords];
                        Random rnd = new Random();
                        int index = (int)Math.Round((double)rnd.Next(commandes.Count));

                        switch (commandes[index])
                        {
                            case 10001:
                                globals.moveHandler.SeDeplacerMap(globals.tpHaut);
                                break;
                            case 10002:
                                globals.moveHandler.SeDeplacerMap(globals.tpBas);
                                break;
                            case 10003:
                                globals.moveHandler.SeDeplacerMap(globals.tpGauche);
                                break;
                            case 10004:
                                globals.moveHandler.SeDeplacerMap(globals.tpDroite);
                                break;
                            default:
                                globals.moveHandler.SeDeplacerMap(commandes[index]);
                                break;
                        }

                    }
                }
                
            }
        }

        private void emptyBag()
        {
            while (!globals.isInExchange)
            {
                Thread.Sleep(200);
            }
            List<Item> tempInv = new List<Item>(player.inventaire);
            foreach(Item item in tempInv)
            {
                if (!item.isEquipped)
                {
                    globals.writeToDebugBox("Giving " + item.quantite + " " + item.libelle + "\n", "Purple");
                    int uniqueIDDec = int.Parse(item.uniqueID, System.Globalization.NumberStyles.HexNumber);
                    globals.serverGame.send("EMO+" + uniqueIDDec + "|" + item.quantite);
                    globals.removingItem = true;
                    while (globals.removingItem)
                    {
                        globals.writeToDebugBox("Waiting remove item\n", "Purple");
                        Thread.Sleep(1000);
                    }
                }
            }
            globals.writeToDebugBox("Inventaire vidé\n", "Purple");
            globals.serverGame.send("EV");
        }
    }
}
