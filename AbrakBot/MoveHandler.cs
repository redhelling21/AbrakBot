using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBot
{
    class MoveHandler
    {

        private static int CaseDuDeplacement;
        private static int bloqueGA;
        public static void SeDeplacer(int caseFin)
        {
            Thread ThreadDeplac = new Thread(SeDeplace);
            int CaseDuDeplacement = caseFin;
            ThreadDeplac.IsBackground = true;
            ThreadDeplac.Start();

        }


        private static void SeDeplace()
        {
            int caseFin = CaseDuDeplacement;
            if ((bloqueGA == 0))
            {
                bloqueGA = 1;

                Globals.writeToDebugBox("Moving from " + Globals.caseActuelle + " to " + caseFin + "\n", System.Drawing.Color.Orange);

                string path = "";
                Pathfinding pather = new Pathfinding();
                path = pather.pathing(Globals.mapDataActuelle, Globals.caseActuelle, caseFin, true);

                if (!string.IsNullOrEmpty(path))
                {
                    TCPPacketHandler.send("GA001" + path);
                    Globals.isMoving = true;

                    if ((distance(Globals.caseActuelle, caseFin) < 6))
                    {
                        Globals.wait((long)distance(Globals.caseActuelle, caseFin) * 300);
                    }
                    else
                    {
                        Globals.wait((long)distance(Globals.caseActuelle, caseFin) * 250);
                    }

                    TCPPacketHandler.send("GKK0");


                }
                else
                {
                    Globals.writeToDebugBox("Error on path from " + Globals.caseActuelle + " to " + caseFin + " !\n", System.Drawing.Color.Red);
                    //TabUtilisateur.ListPlayers.Items.Clear();
                    //TabUtilisateur.ListMonster.Items.Clear();
                    TCPPacketHandler.send("GI");

                }

                Globals.wait(500);

                bloqueGA = 0;
                Globals.isMoving = false;

            }

        }


        public static void SeDeplacerMap(int caseFin)
        {
            Thread ThreadMap = new Thread(SeDeplaceMap);
            CaseDuDeplacement = caseFin;
            ThreadMap.IsBackground = true;
            ThreadMap.Start();

        }


        private static void SeDeplaceMap()
        {
            int caseFin = CaseDuDeplacement;
            if ((bloqueGA == 0))
            {
                bloqueGA = 1;

                Random Rand = new Random();

                /*if (inParty)
                {
                    sock.Envoyer("BM$|.go" + Rand.Next(1000, 9999) + " " + caseFin.ToString);
                }*/

                Globals.wait(Rand.Next(500, 1000));

                Globals.writeToDebugBox("Moving from " + Globals.caseActuelle + " to " + caseFin + "\n", System.Drawing.Color.Orange);

                string path = "";
                Pathfinding pather = new Pathfinding();
                path = pather.pathing(Globals.mapDataActuelle, Globals.caseActuelle, caseFin, true);


                if (!string.IsNullOrEmpty(path))
                {
                    TCPPacketHandler.send("GA001" + path);
                    Globals.isMoving = true;

                    if ((distance(Globals.caseActuelle, caseFin) < 6))
                    {
                        Globals.wait((long)distance(Globals.caseActuelle, caseFin) * 300);
                    }
                    else
                    {
                        Globals.wait((long)distance(Globals.caseActuelle, caseFin) * 250);
                    }

                    TCPPacketHandler.send("GKK0");


                }
                else
                {
                    Globals.writeToDebugBox("Error on path from " + Globals.caseActuelle + " to " + caseFin + " !", System.Drawing.Color.Red);
                    TCPPacketHandler.send("GI");

                }

                Globals.wait(500);

                //changeDeMap = 0;
                bloqueGA = 0;
                Globals.isMoving = false;

            }

        }

        
        public void ChangerMap()
        {
            
            Random rand = new Random();


            if (Globals.lastChangementMap == 0)
            {
                int random = rand.Next(1, 4);
                int random2 = rand.Next(1000, 9999);
                bool fatal = false;
                if ((random == 4))
                {
                    if (Globals.tpHaut != -1)
                    {
                        SeDeplacerMap(Globals.tpHaut);
                        fatal = true;
                        Globals.lastChangementMap = 1;
                    }
                    else
                    {
                        random = rand.Next(1, 3);
                    }
                }
                if ((random == 3))
                {
                    if (Globals.tpBas != -1)
                    {
                        SeDeplacerMap(Globals.tpBas);
                        fatal = true;
                        Globals.lastChangementMap = 2;
                    }
                    else
                    {
                        random = rand.Next(1, 2);
                    }
                }
                if ((random == 2))
                {
                    if (Globals.tpGauche != -1)
                    {
                        SeDeplacerMap(Globals.tpGauche);
                        fatal = true;
                        Globals.lastChangementMap = 3;
                    }
                    else
                    {
                        random = 1;
                    }
                }
                if ((random == 1))
                {
                    if (Globals.tpDroite != -1)
                    {
                        SeDeplacerMap(Globals.tpDroite);
                        fatal = true;
                        Globals.lastChangementMap = 4;
                    }
                }

            }
            else if ((Globals.lastChangementMap == 1))
            {
                if (Globals.tpBas != -1)
                {
                    SeDeplacerMap(Globals.tpBas);
                    Globals.lastChangementMap = 0;
                }

            }
            else if ((Globals.lastChangementMap == 2))
            {
                if (Globals.tpHaut != -1)
                {
                    SeDeplacerMap(Globals.tpHaut);
                    Globals.lastChangementMap = 0;
                }

            }
            else if ((Globals.lastChangementMap == 3))
            {
                if (Globals.tpDroite != -1)
                {
                    SeDeplacerMap(Globals.tpDroite);
                    Globals.lastChangementMap = 0;
                }

            }
            else if ((Globals.lastChangementMap == 4))
            {
                if (Globals.tpGauche != -1)
                {
                    SeDeplacerMap(Globals.tpGauche);
                    Globals.lastChangementMap = 0;
                }

            }

        }


        public static void handleMove(string packet)
        {
            if (!Globals.isFighting)
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
                                Globals.caseActuelle = cell;
                                Globals.writeToDebugBox("Basic cell " + cell + " found", System.Drawing.Color.Orange);
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


        public static double distance(int pos1, int pos2)
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
