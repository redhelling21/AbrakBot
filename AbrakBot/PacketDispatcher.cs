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
        public static void ReceiveData(Queue<string> pck_queue)
        {
            if (pck_queue.Count != 0)
            {
                for (int i = 0; i < pck_queue.Count; i++)
                {
                    string Data = pck_queue.Dequeue();
                    string donnee = "";
                    if (Data != "")
                    {
                        Globals.writeToDebugBox("rcv : ", Color.Red);
                        Globals.writeToDebugBox(Data + "\n", Color.Black);
                        try
                        {
                            donnee = Data.Substring(0, 2);
                        }
                        catch
                        {
                            Globals.writeToDebugBox("erreur parse case\n", Color.Red);
                        }
                        //Console.WriteLine("Donnee " + donnee);
                        switch (donnee)
                        {
                            case "GD": //Debut de la connexion
                                Thread.Sleep(100);
                                string subcat = Data.Substring(2, 3);
                                switch (subcat)
                                {
                                    case "M":
                                        if (!Globals.isInGame)
                                        {
                                            Globals.isInGame = true;
                                            Globals.writeToMainBox("En jeu.\n", Color.Green);
                                        }
                                        string[] map_datas = Data.Split('|');
                                        Globals.currentMapId = Int32.Parse(map_datas[1]);
                                        //TODO recup cells
                                        TCPPacketHandler.send("GI");
                                        break;
                                }
                                break;
                            case "Ow":
                                Thread.Sleep(100);
                                string[] elems = Data.Substring(2).Split('|');
                                Player.pods = Int32.Parse(elems[0]);
                                Player.pods_max = Int32.Parse(elems[1]);
                                break;
                            case "As":
                                Thread.Sleep(100);
                                string[] player_stats = Data.Substring(2).Split('|');
                                string[] xp_stats = player_stats[0].Split(',');
                                Player.xp = Int32.Parse(xp_stats[0]);
                                Player.xp_bas = Int32.Parse(xp_stats[1]);
                                Player.xp_max = Int32.Parse(xp_stats[2]);
                                Player.kamas = Int32.Parse(player_stats[1]);
                                string[] pdv_stats = player_stats[5].Split(',');
                                Player.pdv = Int32.Parse(pdv_stats[0]);
                                Player.pdv_max = Int32.Parse(pdv_stats[1]);
                                string[] en_stats = player_stats[6].Split(',');
                                Player.energie = Int32.Parse(en_stats[0]);
                                Player.energie_max = Int32.Parse(en_stats[1]);
                                
                                break;
                            case "al":
                                TCPPacketHandler.send("GC1");
                                break;
                            case "fC":
                                TCPPacketHandler.send("BD");
                                break;
                            case "GA":
                                Globals.writeToDebugBox(Data.Substring(2, 4) + "\n", Color.Orange);
                                if (Data.Substring(2, 4) == "0;")
                                {
                                    TCPPacketHandler.send("GKK0");
                                }
                                break;
                            default:
                                Globals.writeToDebugBox("Case inconnu\n", Color.Blue);
                                break;

                        }
                    }
                }
            }
        }
    }
}
