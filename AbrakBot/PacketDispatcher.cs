using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBot
{
    class PacketDispatcher
    {
        public void ReceiveData(Queue<string> pck_queue)
        {
            if (pck_queue.Count != 0)
            {
                for (int i = 0; i < pck_queue.Count; i++)
                {
                    string Data = pck_queue.Dequeue();
                    string donnee = "";
                    if (Data != null && Data != "")
                    {
                        Globals.writeToDebugBox("rcv : ", Color.Red);
                        Globals.writeToDebugBox(Data + "\n", Color.Black);
                        donnee = Data.Substring(0, 2);
                        //Console.WriteLine("Donnee " + donnee);
                        switch (donnee)
                        {
                            case "GD": //Debut de la connexion
                                Thread.Sleep(100);
                                string subcat = Data.Substring(2, 1);
                                switch (subcat)
                                {
                                    case "M"://Reception des infos sur la map actuelle
                                        if (!Globals.isInGame)
                                        {
                                            Globals.isInGame = true;
                                            Globals.writeToMainBox("En jeu.\n", Color.Green);
                                            Globals.writeToDebugBox("En jeu.\n", Color.Green);
                                        }

                                        Globals.isMoving = false;
                                        string[] map_datas = Data.Split('|');
                                        Globals.currentMapId = Int32.Parse(map_datas[1]);
                                        //TimerLaunch.Enabled = False
                                        string indice = map_datas[2];
                                        string clef = map_datas[3];
                                        //TabUtilisateur.ListPlayers.Items.Clear()
                                        //TabUtilisateur.ListMonster.Items.Clear()
                                        MapHandler.LoadMap(Globals.currentMapId, indice, clef);
                                        Globals.game.send("GI");
                                        break;
                                    case "F":
                                        string[] res_datas = Data.Split('|');
                                        if (res_datas[1].Split(';')[2] == "0")
                                        {
                                            if (Globals.actualResources.ContainsKey(Int32.Parse(res_datas[1].Split(';')[0]))) {
                                                Globals.actualResources.Remove(Int32.Parse(res_datas[1].Split(';')[0]));
                                            }
                                        }
                                        break;
                                }
                                break;
                            case "GM":
                                MoveHandler.handleMove(Data);
                                break;
                            case "Ow"://Infos sur les pods
                                Thread.Sleep(100);
                                string[] elems = Data.Substring(2).Split('|');
                                Player.pods_max = Int32.Parse(elems[1]);
                                Player.pods = Int32.Parse(elems[0]);

                                break;
                            case "As"://Infos détaillées sur le perso
                                Thread.Sleep(100);
                                string[] player_stats = Data.Substring(2).Split('|');
                                string[] xp_stats = player_stats[0].Split(',');
                                Player.xp_bas = Int32.Parse(xp_stats[1]);
                                Player.xp_max = Int32.Parse(xp_stats[2]);
                                Player.xp = Int32.Parse(xp_stats[0]);

                                Player.kamas = Int32.Parse(player_stats[1]);
                                string[] pdv_stats = player_stats[5].Split(',');
                                Player.pdv_max = Int32.Parse(pdv_stats[1]);
                                Player.pdv = Int32.Parse(pdv_stats[0]);

                                string[] en_stats = player_stats[6].Split(',');
                                Player.energie_max = Int32.Parse(en_stats[1]);
                                Player.energie = Int32.Parse(en_stats[0]);


                                break;
                            case "al"://?
                                Globals.game.send("GC1");
                                break;
                            case "fC"://Nombre de combats sur la map actuelle //A REMPLIR
                                Globals.game.send("BD");
                                break;
                            case "JS":
                                string[] metierArray = Data.Split('|');
                                Player.harvestables.Clear();
                                Player.metiers.Clear();
                                int index = 0;
                                foreach(string metier in metierArray)
                                {
                                    if( index != 0)
                                    {
                                        string[] hres = metier.Split(';')[1].Split(',');
                                        Player.metiers.Add(new Metier());
                                        Player.metiers[index - 1].id = Int32.Parse(metier.Split(';')[0]);
                                        //TODO : changer pour avoir le vrai nom de chaque metier
                                        Player.metiers[index - 1].nom = "Bucheron";
                                        foreach(string str in hres)
                                        {
                                            if (Globals.idResourcesTranslate.ContainsValue(Int32.Parse(str.Split('~')[0])))
                                            {
                                                Player.harvestables.Add(Int32.Parse(str.Split('~')[0]));
                                            }
                                        }
                                    }
                                    index++;
                                }
                                Globals.updateNomMetiers();
                                break;
                            case "JX":
                                string[] metierXPArray = Data.Split('|');
                                int index2 = 0;
                                foreach (string metier in metierXPArray)
                                {
                                    if (index2 != 0)
                                    {
                                        string[] xpstats = metier.Split(';');
                                        foreach (Metier m in Player.metiers)
                                        {
                                            if(m.id == Int32.Parse(xpstats[0]))
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
                                Globals.updateXPMetiers();
                                break;
                            case "GA"://Autorisation de se déplacer

                                if ((Data == "GA;0"))
                                {
                                    //Rien

                                }else if (Data.Substring(0, 5) == "GA;4;")
                                {
                                    //string pate = Data.Split(';')[3];
	                                //Globals.caseActuelle = Gettok(pate, ",", 1);

                                }
                                else if (Data.Substring(0, 6) == "GA;900")
                                {
	                                //.sock.Envoyer("GA902" + Gettok(packet, ";", 3));

                                }
                                else if (Data.Substring(0, 7) == "GA1;501")
                                {

                                    string pate = Data.Split(';')[3];
                                    int tempsDeFauche = Convert.ToInt32(pate.Split(',')[1]);

                                    HarvestHandler.WaitRecolte(tempsDeFauche);
                                }
                                else if (Data.Substring(0, 6) == "GA0;1;")
                                {

                                    if (!Globals.isFighting)
                                    {

                                        
                                            string cherche = Data.Split(';')[3];
                                            cherche = cherche.Substring(cherche.Length - 2);
                                            int trouve = -1;

                                            for (int j = 0; j <= 1024; j++)
                                            {
                                                if ((Globals.cases[j] == cherche))
                                                {
                                                    trouve = j;
                                                    j = 1025;
                                                }
                                            }

                                            if ((trouve != -1))
                                            {
				                                Globals.caseActuelle = trouve;
                                            }


                                        /*
                                        else if (isOnMonsterTab(index, Gettok(packet, ";", 3)))
                                        {
                                            ListViewItem item = onMonsterTab(index, Gettok(packet, ";", 3));

                                            string cherche = Gettok(packet, ";", 4);
                                            cherche = Strings.Mid(cherche, cherche.Length - 1);
                                            int trouve = -1;

                                            for (int i = 0; i <= 1024; i++)
                                            {
                                                if ((cases(i) == cherche))
                                                {
                                                    trouve = i;
                                                    i = 1025;
                                                }
                                            }

                                            if (trouve != -1)
                                                item.SubItems(1).Text = trouve.ToString;


                                        }
                                        else if ((.followChef))
                                        {

                                            if ((Gettok(packet, ";", 3) == .idChef))
                                            {
                                                string cherche = Gettok(packet, ";", 4);
                                                cherche = Strings.Mid(cherche, cherche.Length - 1);
                                                int trouve = -1;

                                                for (int i = 0; i <= 1024; i++)
                                                {
                                                    if ((cases(i) == cherche))
                                                    {
                                                        trouve = i;
                                                        i = 1025;
                                                    }
                                                }

                                                if ((trouve != -1))
                                                {
                                                    int caseFin = trouve;
					.SeDeplacer(caseFin);
                                                }

                                            }

                                        }*/

                                    }

                                }

                                break;
                            case "rp"://Pas trop sur, je crois que c'est un genre de ping régulier
                                if (Data.Substring(2, 3) == "ong")
                                {
                                    Globals.game.send("rpong");
                                }
                                break;
                            case "cM"://Message chat //A TERMINER AVEC L'INSERTION D'ITEMS
                                switch (Data.Substring(2, 1))
                                {
                                    case "K":
                                        string sub = Data.Substring(3, 1);
                                        string[] parts = Data.Split('|');
                                        switch (sub)
                                        {
                                            case ":":
                                                Globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] (Commerce) " + parts[2] + " : " + parts[3] + "\n", Color.SaddleBrown);
                                                break;
                                            case "?":
                                                Globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] (Recrutement) " + parts[2] + " : " + parts[3] + "\n", Color.LightGray);
                                                break;
                                            case "F":
                                                Globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] de " + parts[2] + " : " + parts[3] + "\n", Color.DeepSkyBlue);
                                                break;
                                            case "T":
                                                Globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] a " + parts[2] + " : " + parts[3] + "\n", Color.DeepSkyBlue);
                                                break;
                                            default:
                                                Globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] " + parts[2] + " : " + parts[3] + "\n", Color.Black);
                                                break;
                                        }
                                        break;
                                    case "E":
                                        if (Data.Substring(3, 1) == "f")
                                        {
                                            Globals.writeToMainBox("Le joueur n'existe pas ou n'est pas en ligne", Color.Firebrick);
                                        }
                                        break;
                                }

                                break;
                            default:
                                Globals.writeToDebugBox("Case inconnu\n", Color.Blue);
                                break;

                        }
                    }
                    else
                    {
                        Globals.writeToDebugBox("Aucune données reçues\n", Color.Firebrick);
                    }
                }
            }
        }
    }
}
