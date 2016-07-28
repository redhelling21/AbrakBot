using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace AbrakBotWPF.Model.Services
{
    public class Globals
    {
        public string execPath;
        public Player player;
        public bool isConnected = false;
        public bool isFighting = false;
        public bool isInGame = false;
        public bool isMoving = false;
        public bool isHarvesting = false;
        public bool isRunning = false;

        public TCPPacketHandler connect;
        public TCPPacketHandler game;
        public MoveHandler moveHandler;
        public TrajetHandler trajetHandler;
        public MapHandler mapHandler;

        public int caseActuelle;
        public int currentMapId = 0;
        public int tpHaut, tpBas, tpDroite, tpGauche;
        public Cell[] mapDataActuelle;
        public string[] cases = new string[2500];
        public int bloqueGA = 0;

        //Trajet
        public Dictionary<string, bool[]> listMovements = new Dictionary<string, bool[]>();
        public Dictionary<string, bool[]> listFight = new Dictionary<string, bool[]>();
        public Dictionary<string, bool[]> listHarvest = new Dictionary<string, bool[]>();

        //Liste des ressources sur la carte actuelle (case, idRessource)
        public Dictionary<Int32, Int32> actualResources = new Dictionary<Int32, Int32>();
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
        //Contient tous les id des maps (id, coordonnees)
        public Dictionary<Int32, string> maps = new Dictionary<Int32, string>();

        public int nombreDeCombat = 0;

        //Temps necessaire a recolter une ressource
        public int tempsRecolte;

        public Globals()
        {
            player = new Classes.Player();
            moveHandler = new MoveHandler(this);
            trajetHandler = new TrajetHandler(this, player);
            mapHandler = new MapHandler(this);
        }

        //Recupere le chemin de l'executable
        public void setExecutionPath()
        {
            string uriString = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            Uri uri = new Uri(uriString);
            execPath = uri.LocalPath;
        }

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
        }

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
            writeToMainBox("Trajet " + nom + " chargé\n", "Orange");
            reader.Close();
        }

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

        public void updateResourceTable()
        {
            
        }
    }
    

}
