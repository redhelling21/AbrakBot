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
                if (globals.listFight.ContainsKey(actualCoords))
                {

                }
                else if (globals.listHarvest.ContainsKey(actualCoords))
                {
                    globals.writeToDebugBox("Map de recolte\n","LimeGreen");
                    List<int> cases = new List<int>();
                    foreach(KeyValuePair<Int32, Ressource> entry in globals.actualResources)
                    {
                        if (player.harvestables.Contains(entry.Value.id))
                        {
                            if(entry.Key != 0)
                            {
                                cases.Add(entry.Key);
                            }
                        }
                    }
                    globals.writeToDebugBox(cases.Count + " cases a recolter\n", "LimeGreen");
                    HarvestHandler handler = new HarvestHandler(globals);
                    foreach (int hcase in cases){
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
                    bool[] commandes = globals.listHarvest[actualCoords];
                    bool isSelected = false;
                    Random rnd = new Random();
                    int index = -1;
                    while (!isSelected)
                    {
                        int rnd0 = (int)Math.Round((double)rnd.Next(4));
                        if (commandes[rnd0])
                        {
                            index = rnd0;
                            isSelected = true;
                        }
                    }
                    if (index != -1)
                    {
                        switch (index)
                        {
                            case 0:
                                globals.moveHandler.SeDeplacerMap(globals.tpHaut);
                                break;
                            case 1:
                                globals.moveHandler.SeDeplacerMap(globals.tpBas);
                                break;
                            case 2:
                                globals.moveHandler.SeDeplacerMap(globals.tpGauche);
                                break;
                            case 3:
                                globals.moveHandler.SeDeplacerMap(globals.tpDroite);
                                break;
                        }
                    }
                }
                else if (globals.listMovements.ContainsKey(actualCoords))
                {
                    globals.writeToDebugBox("Map mouvement\n", "LimeGreen");
                    bool[] commandes = globals.listMovements[actualCoords];
                    bool isSelected = false;
                    Random rnd = new Random();
                    int index = -1;
                    while (!isSelected)
                    {
                        int rnd0 = (int)Math.Round((double)rnd.Next(4));
                        if (commandes[rnd0])
                        {
                            index = rnd0;
                            isSelected = true;
                        }
                    }
                    if(index != -1)
                    {
                        switch (index)
                        {
                            case 0:
                                globals.moveHandler.SeDeplacerMap(globals.tpHaut);
                                break;
                            case 1:
                                globals.moveHandler.SeDeplacerMap(globals.tpBas);
                                break;
                            case 2:
                                globals.moveHandler.SeDeplacerMap(globals.tpGauche);
                                break;
                            case 3:
                                globals.moveHandler.SeDeplacerMap(globals.tpDroite);
                                break;
                        }
                    }
                }
            }
        }
    }
}
