using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBot
{
    class HarvestHandler
    {

        private static int caseDeFauche;
        public static int idRessource;
        /*public void GetFauche()
        {
            if ((TabUtilisateur.InvokeRequired))
            {
                TabUtilisateur.Invoke(new ThreadDelegate(GetFauche));

            }
            else
            {

                if (isIdle())
                {

                    for (int i = 0; i <= TabUtilisateur.ListeRessources.Items.Count - 1; i++)
                    {
                        if ((TabUtilisateur.ListeRessources.Items(i).SubItems(0).Text == nomRessource))
                        {

                            if ((TabUtilisateur.ListeRessources.Items(i).SubItems(2).Text == "Non coupé"))
                            {
                                int caseAFaucher = TabUtilisateur.ListeRessources.Items(i).SubItems(1).Text;

                                if (caseAFaucher != caseActuelle)
                                {
                                    Faucher(caseAFaucher);
                                    return;
                                }

                            }
                        }

                    }

                    ChangerMap();

                }

            }

        }*/


        public static void Recolter(int caseFauche)
        {
            Thread ThreadFauche = new Thread(Recolte);
            ThreadFauche.Name = "HarvestThread";
            ThreadFauche.IsBackground = true;
            caseDeFauche = caseFauche;
            ThreadFauche.Start();

        }


        private static void Recolte()
        {
            /* if ((TabUtilisateur.InvokeRequired))
             {
                 TabUtilisateur.Invoke(new ThreadDelegate(Fauche));

             }
             else
             {*/
            idRessource = Globals.actualResources[caseDeFauche];
            int caseFauche = caseDeFauche;
            if ((Globals.bloqueGA == 0))
            {
                Globals.bloqueGA = 1;

                Random Rand = new Random();

                string path = "";
                Pathfinding pather = new Pathfinding();
                if ((Globals.mapDataActuelle[caseFauche].movement == 2))
                {
                    path = pather.pathing(Globals.mapDataActuelle, Globals.caseActuelle, caseFauche);
                }
                else
                {
                    path = pather.pathing(Globals.mapDataActuelle, Globals.caseActuelle, caseFauche, true);
                }


                if (!string.IsNullOrEmpty(path))
                {
                    Globals.game.send("GA001" + path);
                    Globals.isMoving = true;

                    Globals.wait((long)MoveHandler.distance(Globals.caseActuelle, caseFauche) * 330);

                    Globals.game.send("GA500" + caseFauche + ";" + idRessource);
                    Globals.game.send("GKK0");
                    Globals.bloqueGA = 0;
                    Globals.isMoving = false;


                }
                else
                {
                    Globals.bloqueGA = 0;

                    /*
                    for (int i = 0; i <= TabUtilisateur.ListeRessources.Items.Count - 1; i++)
                    {
                        if ((TabUtilisateur.ListeRessources.Items(i).SubItems(0).Text == nomRessource))
                        {

                            if ((TabUtilisateur.ListeRessources.Items(i).SubItems(2).Text == "Non coupé"))
                            {
                                int caseAFaucher = TabUtilisateur.ListeRessources.Items(i).SubItems(1).Text;

                                if (caseAFaucher != caseActuelle)
                                {
                                    TabUtilisateur.ListeRessources.Items.RemoveAt(i);
                                    return;
                                }

                            }

                        }

                    }*/

                }

                //}

            }

        }


        public static void WaitRecolte(int tempsDeRecolte)
        {
            Thread ThreadWaitRecolte = new Thread(WaitingFauche);
            Globals.tempsRecolte = tempsDeRecolte;
            ThreadWaitRecolte.Name = "ThreadRecolte";
            ThreadWaitRecolte.IsBackground = true;
            ThreadWaitRecolte.Start();

        }


        public static void WaitingFauche()
        {
            Random Rand = new Random();
            int tempsDeFauche = Globals.tempsRecolte;

            Globals.isHarvesting = true;

            Thread.Sleep(tempsDeFauche);
            Globals.game.send("GKK1");

            Globals.isHarvesting = false;

        }
    }
}
