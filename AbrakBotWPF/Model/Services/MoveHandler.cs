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
        private int CaseDuDeplacement;

        public MoveHandler(Globals glob)
        {
            this.globals = glob;
        }

        public void SeDeplacer(int caseFin)
        {
            Thread ThreadDeplac = new Thread(SeDeplace);
            CaseDuDeplacement = caseFin;
            ThreadDeplac.Name = "DeplacementThread";
            ThreadDeplac.IsBackground = true;
            ThreadDeplac.Start();

        }


        private void SeDeplace()
        {
            int caseFin = CaseDuDeplacement;
            if ((globals.bloqueGA == 0))
            {
                globals.bloqueGA = 1;

                string path = "";
                Pathfinding pather = new Pathfinding(globals);
                path = pather.pathing(globals.mapDataActuelle, globals.caseActuelle, caseFin, true);

                if (!string.IsNullOrEmpty(path))
                {
                    globals.game.send("GA001" + path);
                    globals.isMoving = true;

                    if ((distance(globals.caseActuelle, caseFin) < 6))
                    {
                        globals.wait((long)distance(globals.caseActuelle, caseFin) * 300);
                    }
                    else
                    {
                        globals.wait((long)distance(globals.caseActuelle, caseFin) * 250);
                    }

                    globals.game.send("GKK0");


                }
                else
                {
                    globals.writeToDebugBox("Error on path from " + globals.caseActuelle + " to " + caseFin + " !\n", "Red");
                    //TabUtilisateur.ListPlayers.Items.Clear();
                    //TabUtilisateur.ListMonster.Items.Clear();
                    globals.game.send("GI");

                }

                globals.wait(500);

                globals.bloqueGA = 0;
                globals.isMoving = false;
            }

        }


        public void SeDeplacerMap(int caseFin)
        {
            Thread ThreadMap = new Thread(SeDeplaceMap);
            ThreadMap.Name = "ThreadMap";
            CaseDuDeplacement = caseFin;
            ThreadMap.IsBackground = true;
            ThreadMap.Start();

        }


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

                globals.wait(Rand.Next(500, 1000));
                

                string path = "";
                Pathfinding pather = new Pathfinding(globals);
                path = pather.pathing(globals.mapDataActuelle, globals.caseActuelle, caseFin, true);


                if (!string.IsNullOrEmpty(path))
                {
                    globals.game.send("GA001" + path);
                    globals.isMoving = true;

                    if ((distance(globals.caseActuelle, caseFin) < 6))
                    {
                        globals.wait((long)distance(globals.caseActuelle, caseFin) * 300);
                    }
                    else
                    {
                        globals.wait((long)distance(globals.caseActuelle, caseFin) * 250);
                    }

                    globals.game.send("GKK0");


                }
                else
                {
                    globals.writeToDebugBox("Error on path from " + globals.caseActuelle + " to " + caseFin + " !", "Red");
                    globals.game.send("GI");

                }

                globals.wait(500);

                //changeDeMap = 0;
                globals.bloqueGA = 0;

            }

        }

        
        public void ChangerMap()
        {
            
            Random rand = new Random();


            if (globals.lastChangementMap == 0)
            {
                int random = rand.Next(1, 4);
                int random2 = rand.Next(1000, 9999);
                bool fatal = false;
                if ((random == 4))
                {
                    if (globals.tpHaut != -1)
                    {
                        SeDeplacerMap(globals.tpHaut);
                        fatal = true;
                        globals.lastChangementMap = 1;
                    }
                    else
                    {
                        random = rand.Next(1, 3);
                    }
                }
                if ((random == 3))
                {
                    if (globals.tpBas != -1)
                    {
                        SeDeplacerMap(globals.tpBas);
                        fatal = true;
                        globals.lastChangementMap = 2;
                    }
                    else
                    {
                        random = rand.Next(1, 2);
                    }
                }
                if ((random == 2))
                {
                    if (globals.tpGauche != -1)
                    {
                        SeDeplacerMap(globals.tpGauche);
                        fatal = true;
                        globals.lastChangementMap = 3;
                    }
                    else
                    {
                        random = 1;
                    }
                }
                if ((random == 1))
                {
                    if (globals.tpDroite != -1)
                    {
                        SeDeplacerMap(globals.tpDroite);
                        fatal = true;
                        globals.lastChangementMap = 4;
                    }
                }

            }
            else if ((globals.lastChangementMap == 1))
            {
                if (globals.tpBas != -1)
                {
                    SeDeplacerMap(globals.tpBas);
                    globals.lastChangementMap = 0;
                }

            }
            else if ((globals.lastChangementMap == 2))
            {
                if (globals.tpHaut != -1)
                {
                    SeDeplacerMap(globals.tpHaut);
                    globals.lastChangementMap = 0;
                }

            }
            else if ((globals.lastChangementMap == 3))
            {
                if (globals.tpDroite != -1)
                {
                    SeDeplacerMap(globals.tpDroite);
                    globals.lastChangementMap = 0;
                }

            }
            else if ((globals.lastChangementMap == 4))
            {
                if (globals.tpGauche != -1)
                {
                    SeDeplacerMap(globals.tpGauche);
                    globals.lastChangementMap = 0;
                }

            }

        }


        public void handleMove(string packet)
        {
            if (!globals.isFighting)
            {
                string extraData = packet.Substring(2);
                string[] datas = extraData.Split('|');

                for (int i = 0; i <= datas.Length - 1; i++)
                {

                    if (datas[i] != "" && datas[i].Substring(0, 1) == "+")
                    {
                        string[] playerData = datas[i].Split(';');

                        string typeS = playerData[5];
                        int type = 0;
                        if (typeS.Contains(","))
                        {
                            type = Int32.Parse(typeS.Split(',')[0]);
                        }
                        else
                        {
                            type = Int32.Parse(typeS);
                        }


                        if ((type > 0))
                        {
                            int cell = Int32.Parse(playerData[0]);
                            int dir = Int32.Parse(playerData[1]);
                            string id = playerData[3];
                            string nom = playerData[4];
                            string guilde = playerData[16];
                            if (string.IsNullOrEmpty(guilde))
                                guilde = "Aucune";

                            string[] aligmentData = playerData[8].Split(',');
                            int align = Int32.Parse(aligmentData[0]);
                            int align2 = Int32.Parse(aligmentData[1]);
                            string alignement = "";
                            switch (align)
                            {
                                case 0:
                                    alignement = "Neutre";
                                    break;
                                case 1:
                                    alignement = "Bontarien";
                                    break;
                                case 2:
                                    alignement = "Brakmarien";
                                    break;
                            }
                            if ((string.IsNullOrEmpty(alignement)))
                            {
                                switch (align2)
                                {
                                    case 0:
                                        alignement = "Neutre";
                                        break;
                                    case 1:
                                        alignement = "Bontarien";
                                        break;
                                    case 2:
                                        alignement = "Brakmarien";
                                        break;
                                }
                            }

                            if ((id == Config.defaultCharacterId.ToString()))
                            {
                                globals.caseActuelle = cell;
                            }

                            int lvlCrypt = Int32.Parse(aligmentData[3]);
                            string level = (lvlCrypt - Int32.Parse(id)).ToString();

                            /*var _with2 = _with1.TabUtilisateur.ListPlayers;

                            _with2.Items.Add(id);
                            _with2.Items(_with2.Items.Count - 1).SubItems.Add(nom);
                            _with2.Items(_with2.Items.Count - 1).SubItems.Add(level);
                            _with2.Items(_with2.Items.Count - 1).SubItems.Add(guilde);
                            _with2.Items(_with2.Items.Count - 1).SubItems.Add(alignement);*/



                        }
                        else if (type == -3)
                        {
                            string cell = playerData[0];
                            string id = playerData[3];

                            /*var _with3 = _with1.TabUtilisateur.ListMonster;

                            _with3.Items.Add(id);
                            _with3.Items(_with3.Items.Count - 1).SubItems.Add(cell);*/


                        }


                    }
                    else if (datas[i] != "" && datas[i].Substring(0, 1) == "-")
                    {
                        //var _with4 = _with1.TabUtilisateur.ListPlayers;
                        /*
                        string id = datas[i].Substring(1);
                        for (int list = 0; list <= _with4.Items.Count - 1; list++)
                        {
                            if ((list >= _with4.Items.Count))
                                break; // TODO: might not be correct. Was : Exit For
                            if ((_with4.Items(list).SubItems.Count == 5))
                            {
                                if ((_with4.Items(list).SubItems(0).Text == id))
                                {
                                    _with4.Items.RemoveAt(list);
                                    return;
                                }
                            }
                        }


                        var _with5 = _with1.TabUtilisateur.ListMonster;

                        string id = Strings.Mid(datas(i), 2);
                        for (int list = 0; list <= _with5.Items.Count - 1; list++)
                        {
                            if ((list >= _with5.Items.Count))
                                break; // TODO: might not be correct. Was : Exit For
                            if ((_with5.Items(list).SubItems.Count == 2))
                            {
                                if ((_with5.Items(list).SubItems(0).Text == id))
                                {
                                    _with5.Items.RemoveAt(list);
                                    return;
                                }
                            }
                        }*/


                    }

                }

            }


        }


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
