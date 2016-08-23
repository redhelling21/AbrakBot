using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class ServerAgent
    {
        private void handleGA(string packet)
        {
            if ((packet == "GA;0"))
            {
                //Rien

            }
            else if (packet.Length > 4 && packet.Substring(0, 5) == "GA;4;")
            {
                string pate = packet.Split(';')[3];
                globals.caseActuelle = Int32.Parse(packet.Split(',')[0]);
                globals.writeToDebugBox("(Via GA;4) CaseActuelle : " + Int32.Parse(packet.Split(',')[0]) + "\n", "Orange");

            }
            else if (packet.Length > 5 && packet.Substring(0, 6) == "GA;900")
            {
                //.sock.Envoyer("GA902" + Gettok(packet, ";", 3));

            }
            else if (packet.Length > 6 && packet.Split(';').Length > 1 && packet.Split(';')[1] == "501") //GA0;501;
            {
                globals.idActionActuelle = packet.Split(';')[0].Substring(2);
                string pate = packet.Split(';')[3];
                int tempsDeRecolte = Convert.ToInt32(pate.Split(',')[1]);
                HarvestHandler handler = new HarvestHandler(globals);
                handler.WaitRecolte(tempsDeRecolte);
            }
            /*else if (packet.Length > 6 && packet.Substring(3, 4) == ";501")
            {

                string pate = packet.Split(';')[3];
                int caseRecolte = Convert.ToInt32(pate.Split(',')[0]);
                globals.actualResources.Remove(caseRecolte);
                globals.isHarvesting = false;
            }*/
            else if (packet.Length > 5 && packet.Split(';').Length > 1 && packet.Split(';')[1] == "1")//GA0;1;
            {

                if (!globals.isFighting)
                {
                    string cherche = packet.Split(';')[3];
                    int id = Int32.Parse(packet.Split(';')[2]);
                    if (id == Int32.Parse(player.id))
                    {
                        globals.idActionActuelle = packet.Split(';')[0].Substring(2);
                        cherche = cherche.Substring(cherche.Length - 2);
                        int trouve = -1;

                        for (int j = 0; j <= 1024; j++)
                        {
                            if ((globals.cases[j] == cherche))
                            {
                                trouve = j;
                                j = 1025;
                            }
                        }

                        if ((trouve != -1))
                        {
                            globals.caseActuelle = trouve;
                            globals.writeToDebugBox("(via GA0;1) CaseActuelle : " + trouve + "\n", "Orange");
                        }
                    }else if(id > 0)
                    {
                        cherche = cherche.Substring(cherche.Length - 2);
                        int trouve = -1;

                        for (int j = 0; j <= 1024; j++)
                        {
                            if ((globals.cases[j] == cherche))
                            {
                                trouve = j;
                                j = 1025;
                            }
                        }
                        if ((trouve != -1))
                        {
                            foreach(MonsterGroup grp in globals.monsterGroups)
                            {
                                if(grp.id == id)
                                {
                                    grp.caseGroupe = trouve;
                                }
                            }
                        }
                    }
                }
            }
            toClient.send(packet);
        }
    }
}
