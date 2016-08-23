using AbrakBotWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Services
{
    public class MoveHandler
    {
        private Globals globals;
        private Player player;
        private int CaseDuDeplacement;

        public MoveHandler(Globals glob, Player play)
        {
            this.globals = glob;
            this.player = play;
        }

        //Lance le deplacement vers une case
        public void SeDeplacer(int caseFin)
        {
            Thread ThreadDeplac = new Thread(SeDeplace);
            CaseDuDeplacement = caseFin;
            ThreadDeplac.Name = "DeplacementThread";
            ThreadDeplac.IsBackground = true;
            ThreadDeplac.Start();

        }

        //Se deplace vers une case
        private void SeDeplace()
        {
            int caseFin = CaseDuDeplacement;
            if ((globals.bloqueGA == 0))
            {
                globals.bloqueGA = 1;

                //calucl du chemin vers la case
                string path = "";
                Pathfinding pather = new Pathfinding(globals);
                path = pather.pathing(globals.mapDataActuelle, globals.caseActuelle, caseFin, true);

                if (!string.IsNullOrEmpty(path))
                {
                    //Demande de l'autorisation du serveur pour se deplacer vers la case
                    globals.serverGame.send("GA001" + path);
                    globals.isMoving = true;

                    if ((distance(globals.caseActuelle, caseFin) < 6))
                    {
                        Thread.Sleep((int)distance(globals.caseActuelle, caseFin) * 300);
                    }
                    else
                    {
                        Thread.Sleep((int)distance(globals.caseActuelle, caseFin) * 250);
                    }
                    //On est arrives
                    globals.serverGame.send("GKK" + globals.idActionActuelle);
                    globals.caseActuelle = caseFin;
                    globals.writeToDebugBox("(via GKK) CaseActuelle : " + caseFin + "\n", "Orange");

                }
                else
                {
                    globals.writeToDebugBox("Error on path from " + globals.caseActuelle + " to " + caseFin + " !\n", "Red");
                    globals.writeToMainBox("Error on path from " + globals.caseActuelle + " to " + caseFin + " !\n", "Red");
                    globals.serverGame.send("GI");

                }

                Thread.Sleep(500);

                globals.bloqueGA = 0;
                globals.isMoving = false;
            }

        }

        //Lance le deplacement vers une autre map
        public void SeDeplacerMap(int caseFin)
        {
            Thread ThreadMap = new Thread(SeDeplaceMap);
            ThreadMap.Name = "ThreadMap";
            CaseDuDeplacement = caseFin;
            ThreadMap.IsBackground = true;
            ThreadMap.Start();

        }

        //Se deplace vers une autre map
        private void SeDeplaceMap()
        {
            int caseFin = CaseDuDeplacement;
            if ((globals.bloqueGA == 0))
            {
                globals.bloqueGA = 1;

                Random Rand = new Random();

                /*if (inParty)
                {
                    sock.Envoyer("BM$|.go" + Rand.Next(1000, 9999) + " " + caseFin.ToString);
                }*/

                Thread.Sleep(Rand.Next(500, 1000));
                

                string path = "";
                Pathfinding pather = new Pathfinding(globals);
                path = pather.pathing(globals.mapDataActuelle, globals.caseActuelle, caseFin, true);


                if (!string.IsNullOrEmpty(path))
                {
                    globals.serverGame.send("GA001" + path);
                    globals.isMoving = true;

                    if ((distance(globals.caseActuelle, caseFin) < 6))
                    {
                        Thread.Sleep((int)distance(globals.caseActuelle, caseFin) * 300);
                    }
                    else
                    {
                        Thread.Sleep((int)distance(globals.caseActuelle, caseFin) * 250);
                    }

                    globals.serverGame.send("GKK" + globals.idActionActuelle);
                    globals.caseActuelle = caseFin;
                    globals.writeToDebugBox("(via GKK) CaseActuelle : " + caseFin + "\n", "Orange");


                }
                else
                {
                    globals.writeToDebugBox("Error on path from " + globals.caseActuelle + " to " + caseFin + " !", "Red");
                    globals.writeToMainBox("Error on path from " + globals.caseActuelle + " to " + caseFin + " !\n", "Red");
                    globals.serverGame.send("GI");

                }

                Thread.Sleep(500);
                globals.bloqueGA = 0;

            }

        }

        

        //Calcule la distance entre deux points
        public double distance(int pos1, int pos2)
        {
            double num18 = 0;
            int num = pos1;
            int num2 = 15;
            double d = (num / ((num2 * 2) - 1));
            double num4 = Math.Ceiling(d) - 1;
            double num5 = (num - (num4 * ((num2 * 2) - 1)));
            double num6 = num5 % num2;
            double num7 = (num4 - num6);
            double num8 = ((num - ((num2 - 1) * num7)) / num2);
            int num9 = pos2;
            int num10 = 15;
            double num11 = (num9 / ((num10 * 2) - 1));
            double num12 = Math.Ceiling(num11) - 1;
            double num13 = (num9 - (num12 * ((num10 * 2) - 1)));
            double num14 = num13 % num10;
            double num15 = (num12 - num14);
            double num16 = ((num9 - ((num10 - 1) * num15)) / num10);
            num18 = Math.Sqrt((Math.Pow(Convert.ToDouble(Convert.ToDecimal((num16 - num8))), 2) + Math.Pow(Convert.ToDouble(Convert.ToDecimal((num15 - num7))), 2)));
            return num18;
        }
        

    }
}
