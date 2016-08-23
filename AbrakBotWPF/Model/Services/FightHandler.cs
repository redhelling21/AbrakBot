using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Services
{
    public class FightHandler
    {

        private string packetCombat;
        private Globals globals;
        private Player player;
        private int toFrappe;
        private int toHeal;
        private int caseDeLance;
        //private int idSort = 141; //TEMPORAIRE
        //private int cible = 1; //TEMPORAIRE
        //private int nombreLance = 4; //TEMPORAIRE
        private int nombreDeCombats = 0; //TEMPORAIRE
        private int tempsRegenerate = 0;

        public FightHandler(Globals glob, Player play)
        {
            this.globals = glob;
            this.player = play;
        }

        public void LaunchBattle()
        {
            /*Threading.Thread ThreadBattle = new Threading.Thread(Launching);
            ThreadBattle.IsBackground = true;
            ThreadBattle.Start();*/

        }


        private void Launching()
        {


            /*Thread.Sleep(500);
            
                int cell = Convert.ToInt32(TabUtilisateur.ListMonster.Items(0).SubItems(1).Text);
                if (caseActuelle != cell & cell != -1)
                {
                    Debug("Monster " + cell + " found");
                    SeDeplacer(cell);
                }
                else
                {
                    Debug("Error on moster " + cell + ", deleting");
                    TabUtilisateur.ListMonster.Items.RemoveAt(0);
                }

            */

            

        }

        public void SendGo()
        {
            Thread ThreadGo = new Thread(SendGoing);
            ThreadGo.IsBackground = true;
            ThreadGo.Start();

        }
        
        private void SendGoing()
        {
            Random Rand = new Random();

            /*if (autoLaunch & BloqueGroupe & inParty)
            {
                sock.Envoyer("fP");
            }*/

            /*if (autoLaunch & BloqueSpectateur)
            {
                int random = Rand.Next(300, 700);
                System.Threading.Thread.Sleep(random);
                sock.Envoyer("fS");
            }*/

            /*if (autoLaunch & inParty)
            {
                int random = Rand.Next(1900, 2300);
                System.Threading.Thread.Sleep(random);
            }
            else
            {
                int random = Rand.Next(300, 800);
                System.Threading.Thread.Sleep(random);
            }*/
            globals.serverGame.send("GR1");

        }


        public void JoinCombat(string combat)
        {
            packetCombat = combat;
            Thread ThreadJoin = new Thread(JoiningCombat);
            ThreadJoin.IsBackground = true;
            ThreadJoin.Start();

        }


        private void JoiningCombat()
        {
            Random Rand = new Random();
            System.Threading.Thread.Sleep(Rand.Next(900, 1600));
            globals.serverGame.send("GA903" + packetCombat + ";" + packetCombat);

        }



        public void PacketCombat(string packet)
        {
            if (packet.Length > 2 && packet.Substring(0, 3) == "GJK")
            {
                globals.isFighting = true;


            }
            if (packet.Substring(0, 2) == "GP")
            {
                globals.isFighting = true;
                SendGo();


            }
            else if (packet.Length > 2 && packet.Substring(0, 3) == "Gc+")
            {
                /*if (((_with1.joinChef) | (_with1.joinAll)))
                {
                    string idCombat = Strings.Mid(packet, 4);
                    idCombat = Gettok(idCombat, ";", 1);
                    if (((idCombat == _with1.idChef) | (_with1.joinAll)))
                    {
                        _with1.JoinCombat(idCombat);
                    }

                }*/


            }
            else if (packet.Length > 2 && packet.Substring(0, 4) == "GTM|")
            {
                int i = 0;
                string[] listStr = packet.Split('|');
                for (i = 1; i < listStr.Length; i++)
                {
                    string[] groupe = listStr[i].Split(';');
                    string estMort = groupe[1];

                    if ((estMort == "0"))
                    {
                        string afrappe = groupe[5];
                        string idMob = groupe[0];
                        float output;
                        if (float.TryParse(idMob, out output))
                        {
                            int idMobInt = Int32.Parse(idMob);

                            if (float.TryParse(afrappe, out output))
                            {
                                if ((idMob == player.id))
                                {
                                    globals.caseActuelle = Int32.Parse(afrappe);
                                }
                            }
                        }

                    }
                }

                int meilleurDistance = 999;
                int meilleurDistanceHeal = 999;

                for (i = 1; i < listStr.Length; i++)
                {
                    string[] groupe = listStr[i].Split(';');
                    string estMort = groupe[1];


                    if ((estMort == "0"))
                    {
                        string afrappe = groupe[5];
                        string idMob = groupe[0];
                        float output;
                        if (float.TryParse(idMob, out output))
                        {
                            int idMobInt = Int32.Parse(idMob);

                            if (float.TryParse(afrappe, out output))
                            {
                                if ((idMobInt < 0))
                                {
                                    if ((goalDistance(globals.caseActuelle, Int32.Parse(afrappe)) < meilleurDistance))
                                    {
                                        toFrappe = Int32.Parse(afrappe);
                                        meilleurDistance = goalDistance(globals.caseActuelle, Int32.Parse(afrappe));
                                    }

                                }
                                else if ((idMobInt > 0) & idMob != player.id)
                                {
                                    if ((goalDistance(globals.caseActuelle, Int32.Parse(afrappe)) < meilleurDistanceHeal))
                                    {
                                        toHeal = Int32.Parse(afrappe);
                                        meilleurDistanceHeal = goalDistance(globals.caseActuelle, Int32.Parse(afrappe));
                                    }
                                }
                            }

                        }

                    }
                }

            }
            else if (packet.Length > 2 && packet.Substring(0, 3) == "GTR")
            {
                globals.serverGame.send("GT");

            }
            else if (packet.Length > 2 && packet.Substring(0, 3) == "GTS")
            {
                string lID = packet.Split('|')[0].Substring(3);
                if ((lID == player.id))
                {
                    //Lancement du tour
                    foreach(SortCombat sort in player.sortsCombat.OrderBy(o => o.priorite).ToList())
                    {
                        if ((sort.id != 0))
                        {
                            if (sort.target == "Ennemi")
                            {
                                caseDeLance = toFrappe;
                            }
                            else if (sort.target == "Invocation")
                            {
                                caseDeLance = toHeal;
                            }
                            else if (sort.target == "Soi-même")
                            {
                                caseDeLance = globals.caseActuelle;
                            }

                            int maxAvance = 1;

                            if (globals.sortsMin[sort.id] != -1)
                            {
                                maxAvance = globals.sortsMin[sort.id];
                            }

                            if ((goalDistance(globals.caseActuelle, caseDeLance) > maxAvance))
                            {
                                Pathfinding pather = new Pathfinding(globals);
                                string path = pather.pathing(globals.mapDataActuelle, globals.caseActuelle, caseDeLance, false, true, player.PM); //TEMPORAIRE (NB PM)
                                if(path != "")
                                {
                                    globals.serverGame.send("GA001" + path);
                                    Thread.Sleep(2000);
                                    globals.serverGame.send("GKK0");
                                }else
                                {
                                    globals.writeToMainBox("Error on path from " + globals.caseActuelle + " to " + caseDeLance + " !\n", "Red");
                                }
                            }

                            if (globals.sortsMax[sort.id] != -1)
                            {
                                int distNeed = globals.sortsMax[sort.id] + 1;
                                if (goalDistance(globals.caseActuelle, caseDeLance) <= distNeed)
                                {
                                    globals.serverGame.send("GA300" + sort.id + ";" + caseDeLance);
                                    
                                    globals.serverGame.send("GKK0");
                                }
                            }
                            else
                            {
                                globals.serverGame.send("GA300" + sort.id + ";" + caseDeLance);
                                
                                globals.serverGame.send("GKK0");
                            }
                        }
                        Thread.Sleep(1000);
                    }
                    globals.serverGame.send("Gt");

                }
                globals.serverGame.send("GT");

            }
            else if (packet.Length > 2 && packet.Substring(0, 3) == "GAF")
            {
                string lowlPacket = packet.Substring(3);
                globals.serverGame.send("GKK" + lowlPacket.Split('|')[0]);
            }
            else if (packet.Length > 2 && packet.Substring(0, 2) == "GE")
            {
                nombreDeCombats += 1;
                /*
                if (((nombreDeCombats >= AuBoutDeCombats) && (ChangerDeMap)))
                {
                    _with1.changeDeMap = 1;
                }*/
                //_with1.TimerLaunch.Enabled = false;
                string[] fightStats = packet.Substring(2).Split('|');
                string[] fightStats2 = fightStats[3].Split(';');
                var msg = new EndedFightMessage() { duree = Int32.Parse(fightStats[0].Split(';')[0]), xp = Int32.Parse(fightStats2[8]), kamas = Int32.Parse(fightStats2[12]), objects = fightStats2[11].Split(',').Length };
                Messenger.Default.Send<EndedFightMessage>(msg);
                globals.isFighting = false;
                globals.serverGame.send("GC1");
                Thread.Sleep(500);
                //if ((_with1.autoLaunch))
                //{
                    int Quota = player.pdv_max / 2;
                    int Vie = player.pdv;
                    if ((Vie < Quota))
                    {
                        globals.serverGame.send("eU1");
                        Regenerate(Quota * 1000);
                    }
                /*if ((nombreDeCombats >= AuBoutDeCombats) & (ChangerDeMap) & _with1.enRegen == 0)
                {
                    _with1.ChangerMap();
                }*/
                //}
                //_with1.TimerLaunch.Enabled = true;
                /*var msg = new AddLineToBoxMessage() { text = text, color = color, boxType = "main" };
                Messenger.Default.Send<AddLineToBoxMessage>(msg);*/

            }
            else if (packet.Substring(0, 2) == "GV")
            {
                globals.isFighting = false;
                globals.serverGame.send("GC1");

            }

            globals.serverGame.toClient.send(packet);
        }

        public int goalDistance(int pos1, int pos2)
        {

            int _loc7 = Math.Abs(MapHandler.getX(pos1) - MapHandler.getX(pos2));
            int _loc8 = Math.Abs(MapHandler.getY(pos1) - MapHandler.getY(pos2));

            return _loc7 + _loc8;
        }

        public void Regenerate(int time)
        {
            tempsRegenerate = time;
            Thread thread = new Thread(Regenerating);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Regenerating()
        {
            globals.isRegenerating = true;

            Thread.Sleep(tempsRegenerate);

            globals.isRegenerating = false;

        }
    }
}
