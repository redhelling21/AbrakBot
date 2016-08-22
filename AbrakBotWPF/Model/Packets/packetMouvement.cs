using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class PacketDispatcher
    {
        private void handleGM(string packet)
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

                            if ((id == player.id))
                            {
                                globals.caseActuelle = cell;
                                globals.writeToDebugBox("(via GM) CaseActuelle : " + cell + "\n", "Orange");
                            }

                            int lvlCrypt = Int32.Parse(aligmentData[3]);
                            string level = (lvlCrypt - Int32.Parse(id)).ToString();



                        }
                        else if (type == -3)
                        {
                            //Groupe de monstres
                            string[] levels = playerData[7].Split(',');
                            int levelTotal = 0;
                            int nbMonstres = 0;
                            foreach(string str in levels)
                            {
                                levelTotal += Int32.Parse(str);
                                nbMonstres += 1;
                            }
                            globals.monsterGroups.Add(new Model.Classes.MonsterGroup(Int32.Parse(playerData[0].Substring(1)), levelTotal, Int32.Parse(playerData[3]), nbMonstres));
                        }


                    }
                    else if (datas[i] != "" && datas[i].Substring(0, 1) == "-")
                    {



                    }

                }

            }


        }

    }
}
