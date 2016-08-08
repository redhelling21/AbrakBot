using AbrakBotWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class PacketDispatcher
    {
        private void handleJob(string packet)
        {
            switch(packet.Substring(0, 2))
            {
                case "JS"://Infos sur les metiers du perso
                    string[] metierArray = packet.Split('|');
                    player.harvestables.Clear();
                    player.metiers.Clear();
                    int index = 0;
                    foreach (string metier in metierArray)
                    {
                        if (index != 0)
                        {
                            string[] hres = metier.Split(';')[1].Split(',');
                            player.metiers.Add(new Metier());
                            player.metiers[index - 1].id = Int32.Parse(metier.Split(';')[0]);
                            //TODO : changer pour avoir le vrai nom de chaque metier
                            player.metiers[index - 1].nom = "Bucheron";
                            foreach (string str in hres)
                            {
                                if (globals.idResourcesTranslate.ContainsValue(Int32.Parse(str.Split('~')[0])))
                                {
                                    player.harvestables.Add(Int32.Parse(str.Split('~')[0]));
                                }
                            }
                        }
                        index++;
                    }
                    globals.updateMetiers();
                    break;
                case "JX"://Infos sur l'xp des metiers du perso
                    string[] metierXPArray = packet.Split('|');
                    int index2 = 0;
                    foreach (string metier in metierXPArray)
                    {
                        if (index2 != 0)
                        {
                            string[] xpstats = metier.Split(';');
                            foreach (Metier m in player.metiers)
                            {
                                if (m.id == Int32.Parse(xpstats[0]))
                                {
                                    m.level = Int32.Parse(xpstats[1]);
                                    m.xp_min = Int32.Parse(xpstats[2]);
                                    m.xp = Int32.Parse(xpstats[3]);
                                    m.xp_max = Int32.Parse(xpstats[4]);
                                }
                            }
                        }
                        index2++;
                    }
                    globals.updateMetiers();
                    break;
            }
        }
    }
}
