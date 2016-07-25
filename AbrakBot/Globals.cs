using AbrakBot.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbrakBot
{
    static class Globals
    {
        public static string execPath;

        public static bool isConnected = false;
        public static bool isFighting = false;
        public static bool isInGame = false;
        public static bool isMoving = false;
        public static bool isHarvesting = false;
        public static bool isRunning = false;

        public static int caseActuelle;
        public static int currentMapId = 0;
        public static int tpHaut, tpBas, tpDroite, tpGauche;
        public static Cell[] mapDataActuelle;
        public static string[] cases = new string[2500];

        public static Dictionary<string, bool[]> listMovements = new Dictionary<string, bool[]>();
        public static Dictionary<string, bool[]> listFight = new Dictionary<string, bool[]>();
        public static Dictionary<string, bool[]> listHarvest = new Dictionary<string, bool[]>();

        public static Dictionary<Int32, string> objects = new Dictionary<Int32, string>();
        public static Dictionary<Int32, string> ressources = new Dictionary<Int32, string>();
        public static Dictionary<Int32, string> sorts = new Dictionary<Int32, string>();
        public static Dictionary<Int32, string> maps = new Dictionary<Int32, string>();
        public static Home mainForm;

        public static int nombreDeCombat = 0;
        public static int lastChangementMap = 0;

        public static void setExecutionPath()
        {
            string uriString = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            Uri uri = new Uri(uriString);
            execPath = uri.LocalPath;
        }

        public static void writeToMainBox(string text, Color color)
        {
            Home.appendBox(mainForm.mainBox, text, color);
        }

        public static void writeToDebugBox(string text, Color color)
        {
            Home.appendBox(mainForm.debugBox, text, color);
        }

        public static void updateBars(int pdv, int xp, int pods, int energie)
        {
            if(pdv >= 0)
            {
                Home.updateBar(mainForm.statusStrip, mainForm.pdvBar, mainForm.pdvLabel, pdv);
            }
            if (xp >= 0)
            {
                Home.updateBar(mainForm.statusStrip, mainForm.xpBar, mainForm.xpLabel, xp);
            }
            if (pods >= 0)
            {
                Home.updateBar(mainForm.statusStrip, mainForm.podsBar, mainForm.podsLabel, pods);
            }
            if (energie >= 0)
            {
                Home.updateBar(mainForm.statusStrip, mainForm.enerBar, mainForm.enerLabel, energie);
            }
        }

        public static void updateCharName(string charName)
        {
            Home.updateTSLabel(mainForm.statusStrip, mainForm.charNameLabel, charName);
        }

        public static void updateLevel(string lvl)
        {
            Home.updateTSLabel(mainForm.statusStrip, mainForm.lvlLabel, lvl);
        }

        public static void updateKamas(string kamas)
        {
            Home.updateTSLabel(mainForm.statusStrip, mainForm.kamasLabel, kamas);
        }

        public static void updateMapCoords(string coords)
        {
            Home.updateTSLabel(mainForm.statusStrip, mainForm.mapCoordLabel, coords);
        }

        public static void sendMessage(string message)
        {
            if(message.Substring(0, 1) == "/")
            {
                switch(message.Substring(1, 1))
                {
                    case "w":
                        string temp = message.Substring(3);
                        string temp2 = temp.Substring(temp.IndexOf(" "));
                        TCPPacketHandler.send("BM" + temp.Substring(0, temp.IndexOf(" ")) + "|" + temp2 + "|");
                        break;
                    case "b":
                        TCPPacketHandler.send("BM:" + message.Substring(3) + "|");
                        break;
                    case "r":
                        TCPPacketHandler.send("BM?" + message.Substring(3) + "|");
                        break;
                    default:
                        writeToMainBox("Type de message inconnu", Color.Firebrick);
                        break;
                }
            }else
            {
                TCPPacketHandler.send("BM*|" + message + "|");
            }
        }

        public static void wait(long ms)
        {
            double endwait = 0;
            endwait = Environment.TickCount + ms;
            while (Environment.TickCount < endwait)
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        //Fct de test
        public static void doSomethingToTest()
        {
            MoveHandler.SeDeplacerMap(Globals.tpHaut);
        }

        public static void InitializeCells()
        {
            int Number = 0;

            string[] hash = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","0","1","2","3","4","5","6","7","8","9","-","_"};
            string[] hash2 = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

            int i = 0;

            for (i = 0; i <= hash2.Length - 1; i++)
            {
                int j = 0;

                for (j = 0; j <= hash.Length - 1; j++)
                {
                    Globals.cases[Number] = hash2[i] + hash[j];
                    Number = Number + 1;

                }

            }

        }

        public static string[] getTrajetList()
        {
            List<string> trajets = new List<string>();
            string[] array = Directory.GetFiles(execPath + "/Trajets").Where(name => name.EndsWith(".txt")).ToArray<string>();
            foreach(string str in array)
            {
                string[] split = str.Split('\\');
                trajets.Add(split[split.Length -1]);
            }
            return trajets.ToArray();
        }

        public static void setActiveTrajet(string nom)
        {
            StreamReader reader = new StreamReader(execPath + "/Trajets/" + nom);
            listMovements.Clear();
            listFight.Clear();
            listHarvest.Clear();
            string line;
            bool movement = false;
            bool fight = false;
            bool harvest = false;
            while ((line = reader.ReadLine()) != null)
            {
                switch(line.Substring(0, 1))
                {
                    case "#":
                        break;
                    case "%":
                        switch (line.Split('%')[1])
                        {
                            case "Mouvement":
                                movement = true;
                                fight = false;
                                harvest = false;
                                break;
                            case "Combat":
                                movement = false;
                                fight = true;
                                harvest = false;
                                break;
                            case "Recolte":
                                movement = false;
                                fight = false;
                                harvest = true;
                                break;
                        }
                        break;
                    case "[":
                        string coords = line.Substring(1).Split(']')[0];
                        string[] commandes = line.Split('>')[1].Split('|');
                        Dictionary<string, bool[]> list = listMovements;
                        if(fight)
                        {
                            list = listFight;
                        }else if (harvest)
                        {
                            list = listHarvest;
                        }
                        list.Add(coords, new bool[4]);
                        foreach (string com in commandes)
                        {
                            switch (com)
                            {
                                case "haut":
                                    list[coords][0] = true;
                                    break;
                                case "bas":
                                    list[coords][1] = true;
                                    break;
                                case "gauche":
                                    list[coords][2] = true;
                                    break;
                                case "droite":
                                    list[coords][3] = true;
                                    break;
                            }
                        }
                        break;
                }
                
            }
            writeToMainBox("Trajet " + nom + " chargé\n", Color.Orange);
            reader.Close();
        }

        public static void makeAMove(int laCase, bool isMap)
        {
            if (!isMoving)
            {
                if (isMap)
                {
                    MoveHandler.SeDeplacerMap(laCase);
                }
                else
                {
                    if (mapDataActuelle[laCase].movement > 2)
                    {
                        MoveHandler.SeDeplacer(laCase);
                    }
                    else
                    {
                        writeToMainBox("La case est inatteignable, ou il s'agit d'un changeur de map\n", Color.Firebrick);
                    }
                    
                }
            }
            else
            {
                
                writeToMainBox("Le personnage est déjà en mouvement\n", Color.Firebrick);
            }
            
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
