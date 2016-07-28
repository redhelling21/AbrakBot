using System;
using System.Threading;

namespace AbrakBotWPF.Model.Services
{
    class HarvestHandler
    {

        private  int caseDeRecolte;
        public  int idRessource;
        private Globals globals;

        public HarvestHandler(Globals globals)
        {
            this.globals = globals;
        }
        public void Recolter(int caseRecolte)
        {
            globals.writeToDebugBox("Recolte de " + caseRecolte + "\n", "Navy");
            Thread ThreadRecolte = new Thread(Recolte);
            ThreadRecolte.Name = "HarvestThread";
            ThreadRecolte.IsBackground = true;
            caseDeRecolte = caseRecolte;
            ThreadRecolte.Start();

        }


        private void Recolte()
        {
            /* if ((TabUtilisateur.InvokeRequired))
             {
                 TabUtilisateur.Invoke(new ThreadDelegate(Recolte));

             }
             else
             {*/
            idRessource = globals.actualResources[caseDeRecolte];
            int caseRecolte = caseDeRecolte;
            if ((globals.bloqueGA == 0))
            {
                globals.bloqueGA = 1;

                Random Rand = new Random();

                string path = "";

                globals.writeToDebugBox("Calcul du chemin\n", "Navy");
                Pathfinding pather = new Pathfinding(globals);
                if ((globals.mapDataActuelle[caseRecolte].movement == 2))
                {
                    path = pather.pathing(globals.mapDataActuelle, globals.caseActuelle, caseRecolte);
                }
                else
                {
                    path = pather.pathing(globals.mapDataActuelle, globals.caseActuelle, caseRecolte, true);
                }


                if (!string.IsNullOrEmpty(path))
                {
                    globals.writeToDebugBox("Envoi du chemin\n", "Navy");
                    globals.game.send("GA001" + path);
                    globals.isMoving = true;
                    globals.writeToDebugBox("isMoving true\n", "Navy");
                    globals.wait((long)globals.moveHandler.distance(globals.caseActuelle, caseRecolte) * 330);

                    globals.game.send("GA500" + caseRecolte + ";" + idRessource);
                    globals.game.send("GKK0");
                    globals.bloqueGA = 0;
                    globals.isMoving = false;
                    globals.writeToDebugBox("isMoving false\n", "Navy");


                }
                else
                {
                    globals.bloqueGA = 0;


                }
                

            }

        }


        public void WaitRecolte(int tempsDeRecolte)
        {
            globals.writeToDebugBox("Attente recolte\n", "Navy");
            Thread ThreadWaitRecolte = new Thread(WaitingRecolte);
            globals.tempsRecolte = tempsDeRecolte;
            ThreadWaitRecolte.Name = "ThreadRecolte";
            ThreadWaitRecolte.IsBackground = true;
            ThreadWaitRecolte.Start();

        }


        public void WaitingRecolte()
        {
            Random Rand = new Random();
            int tempsDeRecolte = globals.tempsRecolte;
            globals.writeToDebugBox("tps recolte : " + tempsDeRecolte + "\n", "Navy");
            globals.isHarvesting = true;
            globals.writeToDebugBox("isharvesting true", "Navy");
            Thread.Sleep(tempsDeRecolte);
            globals.game.send("GKK1");

            globals.isHarvesting = false;
            globals.writeToDebugBox("isharvesting false", "Navy");
        }
    }
}
