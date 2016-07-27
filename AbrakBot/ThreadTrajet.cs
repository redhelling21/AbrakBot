using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBot
{
    class ThreadTrajet
    {
        private static Thread thread;
        private static string actualCoords;

        public static void handleTrajet()
        {
            
            thread = new System.Threading.Thread(new System.Threading.ThreadStart(run));
            thread.Name = "TrajetThread";
            thread.IsBackground = true;
            thread.Start();
            
        }

        private static void run()
        {
            while (Globals.isRunning)
            {
                Thread.Sleep(500);
                actualCoords = Globals.maps[Globals.currentMapId].Replace(" ", string.Empty);
                if (Globals.listFight.ContainsKey(actualCoords))
                {

                }
                else if (Globals.listHarvest.ContainsKey(actualCoords))
                {
                    List<int> cases = new List<int>();
                    foreach(KeyValuePair<Int32, Int32> entry in Globals.actualResources)
                    {
                        if (Player.harvestables.Contains(entry.Value))
                        {
                            cases.Add(entry.Key);
                        }
                    }
                    foreach(int hcase in cases){
                        HarvestHandler.Recolter(hcase);
                        Thread.Sleep(200);
                    }
                    bool[] commandes = Globals.listHarvest[actualCoords];
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
                                MoveHandler.SeDeplacerMap(Globals.tpHaut);
                                break;
                            case 1:
                                MoveHandler.SeDeplacerMap(Globals.tpBas);
                                break;
                            case 2:
                                MoveHandler.SeDeplacerMap(Globals.tpGauche);
                                break;
                            case 3:
                                MoveHandler.SeDeplacerMap(Globals.tpDroite);
                                break;
                        }
                    }
                }
                else if (Globals.listMovements.ContainsKey(actualCoords))
                {
                    bool[] commandes = Globals.listMovements[actualCoords];
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
                                MoveHandler.SeDeplacerMap(Globals.tpHaut);
                                break;
                            case 1:
                                MoveHandler.SeDeplacerMap(Globals.tpBas);
                                break;
                            case 2:
                                MoveHandler.SeDeplacerMap(Globals.tpGauche);
                                break;
                            case 3:
                                MoveHandler.SeDeplacerMap(Globals.tpDroite);
                                break;
                        }
                    }
                }
                while (Globals.isMoving)
                {
                    Thread.Sleep(100);
                }
                
            }
        }
    }
}
