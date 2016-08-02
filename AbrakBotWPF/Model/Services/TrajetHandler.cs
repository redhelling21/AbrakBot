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
                    globals.writeToDebugBox("Trajet banque\n", "Purple");
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
                                globals.writeToDebugBox("Interaction pnj\n", "Purple");
                                globals.game.send("DC-1");
                                emptyBag();
                                globals.needsBank = false;
                                break;
                            default:
                                globals.writeToDebugBox("Go sur la case" + commandes[index] + "\n", "Purple");
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
                    }
                    else if (globals.listHarvest.ContainsKey(actualCoords) || globals.listHarvest.ContainsKey(actualCoordsOpt))
                    {
                        if (globals.listHarvest.ContainsKey(actualCoordsOpt))
                        {
                            actualCoords = actualCoordsOpt;
                        }
                        globals.writeToDebugBox("Map de recolte\n", "LimeGreen");
                        List<int> cases = new List<int>();
                        foreach (KeyValuePair<Int32, Ressource> entry in globals.actualResources)
                        {
                            if (player.harvestables.Contains(entry.Value.id))
                            {
                                if (entry.Key != 0)
                                {
                                    cases.Add(entry.Key);
                                }
                            }
                        }
                        globals.writeToDebugBox(cases.Count + " cases a recolter\n", "LimeGreen");
                        HarvestHandler handler = new HarvestHandler(globals);
                        foreach (int hcase in cases)
                        {
                            globals.writeToDebugBox("Lancement recolte case\n", "LimeGreen");
                            handler.Recolter(hcase);
                            globals.isHarvesting = true;
                            while (globals.isHarvesting)
                            {
                                Thread.Sleep(200);
                            }
                            globals.writeToDebugBox("Case recoltee\n", "LimeGreen");
                            Thread.Sleep(200);
                        }
                        globals.writeToDebugBox("Changement de map\n", "LimeGreen");
                        List<int> commandes = globals.listMovements[actualCoords];
                        bool isSelected = false;
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
                    else if (globals.listMovements.ContainsKey(actualCoords) || globals.listMovements.ContainsKey(actualCoordsOpt))
                    {
                        if (globals.listMovements.ContainsKey(actualCoordsOpt))
                        {
                            actualCoords = actualCoordsOpt;
                        }
                        globals.writeToDebugBox("Map mouvement\n", "LimeGreen");
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
                globals.writeToDebugBox("Wait dialog end\n", "Purple");
                Thread.Sleep(200);
            }
            List<Item> tempInv = new List<Item>(player.inventaire);
            foreach(Item item in tempInv)
            {
                if (!item.isEquipped)
                {
                    globals.writeToDebugBox("Giving " + item.libelle + "\n", "Purple");
                    int uniqueIDDec = int.Parse(item.uniqueID, System.Globalization.NumberStyles.HexNumber);
                    globals.game.send("EMO+" + uniqueIDDec + "|" + item.quantite);
                    globals.removingItem = true;
                    while (globals.removingItem)
                    {
                        globals.writeToDebugBox("Waiting remove item\n", "Purple");
                        Thread.Sleep(200);
                    }
                }
            }
            globals.writeToDebugBox("Inventaire vidé\n", "Purple");
        }
    }
}
