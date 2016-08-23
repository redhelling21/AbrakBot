using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.Model.Network;
using AbrakBotWPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;

namespace AbrakBotWPF.Model.Services
{
    public class Globals
    {
        #region MACRO
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, Int32 nCmdShow);
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);


        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        #endregion

        public static string execPath;
        public Player player;

        #region booleens d'etat
        private bool _isFighting = false;
        private bool _isMoving = false;
        private bool _isHarvesting = false;
        private bool _isInExchange = false;
        public bool isFighting
        {
            get { return _isFighting; }
            set
            {
                _isFighting = value;
                var msg = new PlayerStateChangedMessage() { isFighting = _isFighting, isHarvesting = _isHarvesting, isInExchange = _isInExchange, isMoving = _isMoving };
                Messenger.Default.Send<PlayerStateChangedMessage>(msg);
            }
        }
        public bool isMoving
        {
            get { return _isMoving; }
            set
            {
                _isMoving = value;
                var msg = new PlayerStateChangedMessage() { isFighting = _isFighting, isHarvesting = _isHarvesting, isInExchange = _isInExchange, isMoving = _isMoving };
                Messenger.Default.Send<PlayerStateChangedMessage>(msg);
            }
        }
        public bool isHarvesting
        {
            get { return _isHarvesting; }
            set
            {
                _isHarvesting = value;
                var msg = new PlayerStateChangedMessage() { isFighting = _isFighting, isHarvesting = _isHarvesting, isInExchange = _isInExchange, isMoving = _isMoving };
                Messenger.Default.Send<PlayerStateChangedMessage>(msg);
            }
        }
        public bool isInExchange
        {
            get { return _isInExchange; }
            set
            {
                _isInExchange = value;
                var msg = new PlayerStateChangedMessage() { isFighting = _isFighting, isHarvesting = _isHarvesting, isInExchange = _isInExchange, isMoving = _isMoving };
                Messenger.Default.Send<PlayerStateChangedMessage>(msg);
            }
        }
        #endregion

        public bool isConnected = false;
        public bool isInGame = false;
        public bool isRunning = false;
        public bool needsBank = false;
        public bool mapLoaded = false;
        public bool isInDialog = false;
        public bool removingItem = false;
        public bool isDead = false;
        public bool isRegenerating = false;
        public bool isInQueue = false;

        public ClientAgent clientConnect;
        public ClientAgent clientGame;
        public ServerAgent serverConnect;
        public ServerAgent serverGame;
        //public TCPPacketHandler connect;
        //public TCPPacketHandler game;
        public MoveHandler moveHandler;
        public TrajetHandler trajetHandler;
        public FightHandler fightHandler;
        public MapHandler mapHandler;

        public int caseActuelle;
        public int currentMapId = 0;
        public int tpHaut, tpBas, tpDroite, tpGauche;
        public Cell[] mapDataActuelle;
        public string[] cases = new string[2500];
        public int bloqueGA = 0;

        public string idActionActuelle = "0";

        //Trajet
        public Dictionary<string, List<int>> listMovements = new Dictionary<string, List<int>>();
        public Dictionary<string, List<int>> listFight = new Dictionary<string, List<int>>();
        public Dictionary<string, List<int>> listHarvest = new Dictionary<string, List<int>>();
        public Dictionary<string, List<int>> listBanque = new Dictionary<string, List<int>>();

        //Liste des ressources sur la carte actuelle (case, idRessource)
        public Dictionary<Int32, Ressource> actualResources = new Dictionary<Int32, Ressource>();
        //Contient les équivalents entre l'id du sprite et l'id de la ressource (idSprite, idRessource)
        public Dictionary<Int32, Int32> idResourcesTranslate = new Dictionary<Int32, Int32>();
        //Contient les mapchangers de certaines cartes buggées (idMap, idCaseChangers)
        public Dictionary<Int32, Int32[]> mapchangers = new Dictionary<Int32, Int32[]>();
        //Contient tous les id des objets (id, nomObjet)
        public Dictionary<Int32, string> objects = new Dictionary<Int32, string>();
        //Contient tous les id des sprites des ressources (id, nomRessource)
        public Dictionary<Int32, string> ressources = new Dictionary<Int32, string>();
        //Contient tous les id des sorts (id, nomSort)
        public Dictionary<Int32, string> sorts = new Dictionary<Int32, string>();
        public int[] sortsMin = new int[1000];
        public int[] sortsMax = new int[1000];
        //Contient tous les id des maps (id, coordonnees)
        public Dictionary<Int32, string> maps = new Dictionary<Int32, string>();

        public List<MonsterGroup> monsterGroups = new List<MonsterGroup>();

        public int nbMinMonstres = 0;
        public int nbMaxMonstres = 8;
        public int lvlMinMonstres = 0;
        public int lvlMaxMonstres = 9999;
        public int percentRegen = 0;

        public int nombreDeCombat = 0;
        public int podsPercentLimit = 70;
        //Temps necessaire a recolter une ressource
        public int tempsRecolte;

        public Globals()
        {
            player = new Classes.Player();
            moveHandler = new MoveHandler(this, player);
            trajetHandler = new TrajetHandler(this, player);
            fightHandler = new FightHandler(this, player);
            mapHandler = new MapHandler(this);
            
        }

        //Recupere le chemin de l'executable
        public void setExecutionPath()
        {
            string uriString = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            Uri uri = new Uri(uriString);
            execPath = uri.LocalPath;
        }

        public void connect()
        {
            serverConnect = new ServerAgent(this, player);
            serverGame = new ServerAgent(this, player);
            Process.Start(@"C:\Program Files (x86)\Abrak\Dofus.exe");
            Thread.Sleep(3500);
            Process[] processes = Process.GetProcessesByName("dofus.dll");
            SetForegroundWindow(processes[0].MainWindowHandle);
            ClickOnPointTool.ClickOnPoint(processes[0].MainWindowHandle, new Point(700, 165));
            Thread.Sleep(2500);
            SendKeys.SendWait(Config.username);
            Thread.Sleep(200);
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(200);
            SendKeys.SendWait(Config.mdp);
            Thread.Sleep(200);
            SendKeys.SendWait("{ENTER}");
            SendKeys.Flush();

            clientConnect = new ClientAgent("127.0.0.1", 5547, this);
            clientConnect.toServ = serverConnect;
            serverConnect.toClient = clientConnect;
            serverConnect.Handle(Config.serverIp, Config.serverPort);
        }

        #region Actions to UI
        //Demande l'ecriture d'une ligne dans la box principale
        public void writeToMainBox(string text, string color)
        {
            var msg = new AddLineToBoxMessage() { text = text, color = color, boxType = "main" };
            Messenger.Default.Send<AddLineToBoxMessage>(msg);
        }

        //Demande l'ecriture d'une ligne dans la box de debug
        public void writeToDebugBox(string text, string color)
        {
            var msg = new AddLineToBoxMessage() { text = text, color = color, boxType = "debug" };
            Messenger.Default.Send<AddLineToBoxMessage>(msg);
        }

        //Demande la mise a jour des coordonnees affichees
        public void updateMapCoords(string coords)
        {
            var msg = new MapStatChangedMessage() { mapCoords = coords };
            Messenger.Default.Send<MapStatChangedMessage>(msg);
        }

        //Demande la mise a jour des stats sur les metiers
        public void updateMetiers()
        {
            var msg = new PlayerJobsChangedMessage() { metiers = player.metiers };
            Messenger.Default.Send<PlayerJobsChangedMessage>(msg);
        }

        #endregion
        /*
        //Demande l'envoi d'un message en jeu (commerce, recrutement, etc...)
        public void sendMessage(string message)
        {
            if(message.Substring(0, 1) == "/")
            {
                switch(message.Substring(1, 1))
                {
                    case "w":
                        string temp = message.Substring(3);
                        string temp2 = temp.Substring(temp.IndexOf(" "));
                        this.game.send("BM" + temp.Substring(0, temp.IndexOf(" ")) + "|" + temp2 + "|");
                        break;
                    case "b":
                        this.game.send("BM:" + message.Substring(3) + "|");
                        break;
                    case "r":
                        this.game.send("BM?" + message.Substring(3) + "|");
                        break;
                    default:
                        writeToMainBox("Type de message inconnu", "Firebrick");
                        break;
                }
            }else
            {
                this.game.send("BM*|" + message + "|");
            }
        }*/

        //Beeeen... Attends
        public void wait(long ms)
        {
            double endwait = 0;
            endwait = Environment.TickCount + ms;
            while (Environment.TickCount < endwait)
            {
                System.Threading.Thread.Sleep(1);
                //Application.DoEvents();
            }
        }

        //Fct de test (dispose d'un bouton a elle dans l'UI) pour lancer rapidement des choses
        public void doSomethingToTest()
        {
            moveHandler.SeDeplacerMap(this.tpHaut);
        }

        //Initialise une liste des cases d'une map (id en hexadecimal). Utile pour le pathfinding
        public void InitializeCells()
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
                    this.cases[Number] = hash2[i] + hash[j];
                    Number = Number + 1;

                }

            }

        }

        #region Trajet
        //Recupere la liste des trajets dispos
        public string[] getTrajetList()
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

        //Parsing du trajet choisi
        public void setActiveTrajet(string nom)
        {
            StreamReader reader = new StreamReader(execPath + "/Trajets/" + nom);
            listMovements.Clear();
            listFight.Clear();
            listHarvest.Clear();
            string line;
            bool movement = false;
            bool fight = false;
            bool harvest = false;
            bool bank = false;
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
                                bank = false;
                                break;
                            case "Combat":
                                movement = false;
                                fight = true;
                                harvest = false;
                                bank = false;
                                break;
                            case "Recolte":
                                movement = false;
                                fight = false;
                                harvest = true;
                                bank = false;
                                break;
                            case "Banque":
                                movement = false;
                                fight = false;
                                harvest = false;
                                bank = true;
                                break;
                        }
                        break;
                    case "[":
                        string coords = line.Substring(1).Split(']')[0];
                        string[] commandes = line.Split('>')[1].Split('|');
                        Dictionary<string, List<int>> list = listMovements;
                        if(fight)
                        {
                            list = listFight;
                        }else if (harvest)
                        {
                            list = listHarvest;
                        }else if (bank)
                        {
                            list = listBanque;
                        }
                        list.Add(coords, new List<int>());
                        foreach (string com in commandes)
                        {
                            switch (com)
                            {
                                case "haut":
                                    list[coords].Add(10001);
                                    break;
                                case "bas":
                                    list[coords].Add(10002);
                                    break;
                                case "gauche":
                                    list[coords].Add(10003);
                                    break;
                                case "droite":
                                    list[coords].Add(10004);
                                    break;
                                case "banque":
                                    list[coords].Add(9999);
                                    break;
                                default:
                                    int valOut = 0;
                                    if (Int32.TryParse(com, out valOut))
                                    {
                                        list[coords].Add(valOut);
                                    }
                                    break;
                            }
                        }
                        break;
                }
                
            }
            writeToMainBox("Trajet " + nom + " chargé\n", "Orange");
            reader.Close();
        }
        #endregion

        //Fait bouger le personnage (utilisé uniquement par la telecommande)
        public void makeAMove(int laCase, bool isMap)
        {
            if (!isMoving)
            {
                if (isMap)
                {
                    moveHandler.SeDeplacerMap(laCase);
                }
                else
                {
                    if (mapDataActuelle[laCase].movement > 2)
                    {
                        moveHandler.SeDeplacer(laCase);
                    }
                    else
                    {
                        writeToMainBox("La case est inatteignable, ou il s'agit d'un changeur de map\n", "Firebrick");
                    }
                    
                }
            }
            else
            {
                
                writeToMainBox("Le personnage est déjà en mouvement\n", "Firebrick");
            }
            
        }

        public void disconnect()
        {

            //Home.updateTSButtonText(mainForm.statusStrip, mainForm.connectButton, "Connexion");
        }

        //Recupere la liste des trajets dispos
        public string[] getConfigList()
        {
            List<string> confs = new List<string>();
            string[] array = Directory.GetFiles(execPath + "/Configs").Where(name => name.EndsWith(".json")).ToArray<string>();
            foreach (string str in array)
            {
                string[] split = str.Split('\\');
                confs.Add(split[split.Length - 1]);
            }
            return confs.ToArray();
        }

        public void updateResourceTable()
        {

        }
    }
    

}
