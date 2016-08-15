using AbrakBotWPF.Model.Classes;
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
        private int idSort = 141; //TEMPORAIRE
        private int cible = 1; //TEMPORAIRE
        private int nombreLance = 4; //TEMPORAIRE
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
            globals.game.send("GR1");

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
            globals.game.send("GA903" + packetCombat + ";" + packetCombat);

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
                globals.game.send("GT");

            }
            else if (packet.Length > 2 && packet.Substring(0, 3) == "GTS")
            {
                string lID = packet.Split('|')[0].Substring(3);
                if ((lID == player.id))
                {

                    if ((idSort != 0))
                    {
                        if (cible == 1)
                        {
                            caseDeLance = toFrappe;
                        }
                        else if (cible == 2)
                        {
                            caseDeLance = toHeal;
                        }
                        else if (cible == 3)
                        {
                            caseDeLance = globals.caseActuelle;
                        }

                        int maxAvance = 1;

                        if (globals.sortsMin[idSort] != -1)
                        {
                            maxAvance = globals.sortsMin[idSort];
                        }

                        if ((goalDistance(globals.caseActuelle, caseDeLance) > maxAvance))
                        {
                            Pathfinding pather = new Pathfinding(globals);
                            string path = pather.pathing(globals.mapDataActuelle, globals.caseActuelle, caseDeLance, false, true, 3); //TEMPORAIRE (NB PM)
                            globals.game.send("GA001" + path);
                            globals.game.send("GKK0");
                        }

                        if (globals.sortsMax[idSort] != -1)
                        {
                            int distNeed = globals.sortsMax[idSort] + 1;
                            if (goalDistance(globals.caseActuelle, caseDeLance) <= distNeed)
                            {
                                for (int i = 0; i < nombreLance; i++)
                                {
                                    globals.game.send("GA300" + idSort + ";" + caseDeLance);
                                    globals.game.send("GKK0");
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < nombreLance; i++)
                            {
                                globals.game.send("GA300" + idSort + ";" + caseDeLance);
                                globals.game.send("GKK0");
                            }
                        }

                    }

                    globals.game.send("Gt");

                }
                globals.game.send("GT");

            }
            else if (packet.Length > 2 && packet.Substring(0, 3) == "GAF")
            {
                string lowlPacket = packet.Substring(3);
                globals.game.send("GKK" + lowlPacket.Split('|')[0]);

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
                globals.isFighting = false;
                globals.game.send("GC1");
                Thread.Sleep(500);
                //if ((_with1.autoLaunch))
                //{
                    int Quota = player.pdv_max / 2;
                    int Vie = player.pdv;
                    if ((Vie < Quota))
                    {
                        globals.game.send("eU1");
                        Regenerate(Quota * 1000);
                    }
                    /*if ((nombreDeCombats >= AuBoutDeCombats) & (ChangerDeMap) & _with1.enRegen == 0)
                    {
                        _with1.ChangerMap();
                    }*/
                //}
                //_with1.TimerLaunch.Enabled = true;

            }
            else if (packet.Substring(0, 2) == "GV")
            {
                globals.isFighting = false;
                globals.game.send("GC1");

            }


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
