using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AbrakBotWPF.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        //COMMANDS
        public RelayCommand ConnectCommand { get; private set; }
        public RelayCommand TelecommandeCommand { get; private set; }
        public RelayCommand TestCommand { get; private set; }
        public RelayCommand LaunchCommand { get; private set; }
        public Globals globals;
        public MainWindow window;
        
        public MainViewModel()
        {
            

            Messenger.Default.Register<AddLineToBoxMessage>
            (
                 this,
                 (addLineMessage) => ReceiveAddLineToBox(addLineMessage)
            );
            Messenger.Default.Register<PlayerStatChangedMessage>
            (
                 this,
                 (statChangedMessage) => ReceivePlayerStatChanged(statChangedMessage)
            );
            Messenger.Default.Register<PlayerJobsChangedMessage>
            (
                 this,
                 (jobsChangedMessage) => ReceivePlayerJobsChanged(jobsChangedMessage)
            );
            ConnectCommand = new RelayCommand(connect);
            TelecommandeCommand = new RelayCommand(telecommande);
            TestCommand = new RelayCommand(test);
            LaunchCommand = new RelayCommand(launch);
        }

        public void initializeGlobals()
        {
            Config.load();
            globals.setExecutionPath();
            ResourceLoader.load(globals);
            globals.InitializeCells();
        }

        #region RESSOURCES TAB

        #region METIER 1
        private string _metierBlockText1 = "?";
        public string metierBlockText1
        {
            get { return _metierBlockText1; }
            set
            {
                if (_metierBlockText1 == value) return;
                _metierBlockText1 = value;
                RaisePropertyChanged("metierBlockText1");
            }
        }

        private string _metierBar1;
        public string metierBar1
        {
            get { return _metierBar1; }
            set
            {
                if (_metierBar1 == value) return;
                _metierBar1 = value;
                RaisePropertyChanged("metierBar1");
            }
        }

        private string _metierLvl1 = "Lvl. 0";
        public string metierLvl1
        {
            get { return _metierLvl1; }
            set
            {
                if (_metierLvl1 == value) return;
                _metierLvl1 = value;
                RaisePropertyChanged("metierLvl1");
            }
        }
        #endregion

        #region METIER 2
        private string _metierBlockText2 = "?";
        public string metierBlockText2
        {
            get { return _metierBlockText2; }
            set
            {
                if (_metierBlockText2 == value) return;
                _metierBlockText2 = value;
                RaisePropertyChanged("metierBlockText2");
            }
        }

        private string _metierBar2;
        public string metierBar2
        {
            get { return _metierBar2; }
            set
            {
                if (_metierBar2 == value) return;
                _metierBar2 = value;
                RaisePropertyChanged("metierBar2");
            }
        }

        private string _metierLvl2 = "Lvl. 0";
        public string metierLvl2
        {
            get { return _metierLvl2; }
            set
            {
                if (_metierLvl2 == value) return;
                _metierLvl2 = value;
                RaisePropertyChanged("metierLvl2");
            }
        }
        #endregion

        #region METIER 3
        private string _metierBlockText3 = "?";
        public string metierBlockText3
        {
            get { return _metierBlockText3; }
            set
            {
                if (_metierBlockText3 == value) return;
                _metierBlockText3 = value;
                RaisePropertyChanged("metierBlockText3");
            }
        }

        private string _metierBar3;
        public string metierBar3
        {
            get { return _metierBar3; }
            set
            {
                if (_metierBar3 == value) return;
                _metierBar3 = value;
                RaisePropertyChanged("metierBar3");
            }
        }

        private string _metierLvl3 = "Lvl. 0";
        public string metierLvl3
        {
            get { return _metierLvl3; }
            set
            {
                if (_metierLvl3 == value) return;
                _metierLvl3 = value;
                RaisePropertyChanged("metierLvl3");
            }
        }
        #endregion

        #endregion

        //TRAJETS
        private string _selectedTrajet;
        public string selectedTrajet
        {
            get { return _selectedTrajet; }
            set
            {
                if (_selectedTrajet == value) return;
                _selectedTrajet = value;
                RaisePropertyChanged("selectedTrajet");
                globals.setActiveTrajet(value);
            }
        }

        //RESSOURCES RECOLTABLES
        private List<string> _harvestables;
        public List<string> harvestables
        {
            get { return _harvestables; }
            set
            {
                if (_harvestables == value) return;
                _harvestables = value;
                RaisePropertyChanged("harvestables");
            }
        }

        #region STATUSBAR
        //XP
        private int _barXP = 0;
        public int barXP
        {
            get { return _barXP; }
            set
            {
                _barXP = value;
                RaisePropertyChanged("barXP");
            }
        }

        private string _percentXP = "0%";
        public string percentXP
        {
            get { return _percentXP; }
            set
            {
                _percentXP = value;
                RaisePropertyChanged("percentXP");
            }
        }

        //PODS
        private int _barPods = 0;
        public int barPods
        {
            get { return _barPods; }
            set
            {
                _barPods = value;
                RaisePropertyChanged("barPods");
            }
        }

        private string _percentPods = "0%";
        public string percentPods
        {
            get { return _percentPods; }
            set
            {
                _percentPods = value;
                RaisePropertyChanged("percentPods");
            }
        }

        //ENERGIE
        private int _barEner = 0;
        public int barEner
        {
            get { return _barEner; }
            set
            {
                _barEner = value;
                RaisePropertyChanged("barEner");
            }
        }

        private string _percentEner = "0%";
        public string percentEner
        {
            get { return _percentEner; }
            set
            {
                _percentEner = value;
                RaisePropertyChanged("percentEner");
            }
        }

        //PDV
        private int _barPDV = 0;
        public int barPDV
        {
            get { return _barPDV; }
            set
            {
                _barPDV = value;
                RaisePropertyChanged("barPDV");
            }
        }

        private string _percentPDV = "0%";
        public string percentPDV
        {
            get { return _percentPDV; }
            set
            {
                _percentPDV = value;
                RaisePropertyChanged("percentPDV");
            }
        }

        //LVL
        private string _lvl = "Lvl. 0";
        public string lvl
        {
            get { return _lvl; }
            set
            {
                _lvl = value;
                RaisePropertyChanged("lvl");
            }
        }

        //MAP COORDS
        private string _mapCoords = "[0, 0]";
        public string mapCoords
        {
            get { return _mapCoords; }
            set
            {
                _mapCoords = value;
                RaisePropertyChanged("mapCoords");
            }
        }

        //PSEUDO
        private string _pseudo = "?";
        public string pseudo
        {
            get { return _pseudo; }
            set
            {
                _pseudo = value;
                RaisePropertyChanged("pseudo");
            }
        }

        //KAMAS
        private string _kamas = "0";
        public string kamas
        {
            get { return _kamas; }
            set
            {
                _kamas = value;
                RaisePropertyChanged("kamas");
            }
        }

        #endregion

        #region TEXTBUTTONS
        //TEXT CONNECT BUTTON
        private string _connectButtonText = "Connexion";
        public string connectButtonText
        {
            get { return _connectButtonText; }
            set
            {
                _connectButtonText = value;
                RaisePropertyChanged("connectButtonText");
            }
        }

        //TEXT START BUTTON
        private string _startButtonText = "Lancer";
        public string startButtonText
        {
            get { return _startButtonText; }
            set
            {
                _startButtonText = value;
                RaisePropertyChanged("startButtonText");
            }
        }
        #endregion

        #region RECEIVEMESSAGE FUNCTIONS

        private void ReceiveAddLineToBox(AddLineToBoxMessage action)
        {
            if (action.boxType == "main")
            {
                window.mainBox.Dispatcher.Invoke((Action)(() =>
                {
                    window.mainBox.AppendText(action.text, action.color);
                }));
            }
            else if (action.boxType == "debug")
            {
                window.mainBox.Dispatcher.Invoke((Action)(() =>
                {
                    window.debugBox.AppendText(action.text, action.color);
                }));
            }
        }

        private void ReceivePlayerStatChanged(PlayerStatChangedMessage action)
        {
            switch (action.stat)
            {
                case "xp":
                    barXP = action.value;
                    percentXP = action.value.ToString() + "%";
                    break;
                case "kamas":
                    kamas = action.value.ToString();
                    break;
                case "level":
                    lvl = action.value.ToString();
                    break;
                case "pods":
                    barPods = action.value;
                    percentPods = action.value.ToString() + "%";
                    break;
                case "energie":
                    barEner = action.value;
                    percentEner = action.value.ToString() + "%";
                    break;
                case "pdv":
                    barPDV = action.value;
                    percentPDV = action.value.ToString() + "%";
                    break;
                case "pseudo":
                    pseudo = action.valString;
                    break;
            }
        }

        private void ReceivePlayerJobsChanged(PlayerJobsChangedMessage action)
        {
            if(action.metiers.Count >= 1)
            {
                metierBlockText1 = (action.metiers[0].nom != "") ? action.metiers[0].nom : "Metier inconnu"; //Temporaire
                metierBar1 = ((int)Math.Round(((float)(action.metiers[0].xp - action.metiers[0].xp_min) / (action.metiers[0].xp_max - action.metiers[0].xp_min)) * 100)).ToString();
                metierLvl1 = "Lvl. " + action.metiers[0].level.ToString();
            }
            if (action.metiers.Count >= 2)
            {
                metierBlockText2 = (action.metiers[1].nom != "") ? action.metiers[1].nom : "Metier inconnu"; //Temporaire
                metierBar2 = ((int)Math.Round(((float)(action.metiers[1].xp - action.metiers[1].xp_min) / (action.metiers[0].xp_max - action.metiers[1].xp_min)) * 100)).ToString();
                metierLvl2 = "Lvl. " + action.metiers[1].level.ToString();
            }
            if (action.metiers.Count >= 3)
            {
                metierBlockText3 = (action.metiers[2].nom != "") ? action.metiers[2].nom : "Metier inconnu"; //Temporaire
                metierBar3 = ((int)Math.Round(((float)(action.metiers[2].xp - action.metiers[2].xp_min) / (action.metiers[0].xp_max - action.metiers[2].xp_min)) * 100)).ToString();
                metierLvl3 = "Lvl. " + action.metiers[2].level.ToString();
            }
        }

        #endregion

        #region COMMANDS FUNCTIONS
        private void connect()
        {
            if (connectButtonText == "Connexion")
            {
                connectButtonText = "Déconnexion";
                globals.connect = new TCPPacketHandler(globals, globals.player);
                globals.connect.Handle(Config.serverIp, Config.serverPort);
            }
            else
            {
                connectButtonText = "Connexion";
                globals.connect.close();
                globals.connect.shouldStop = true;
            }
        }

        private void telecommande()
        {

        }

        private void test()
        {
            barXP = 50;
            barPDV = 75;
        }

        private void launch()
        {
            if (startButtonText == "Lancer")
            {
                startButtonText = "Arrêter";
                globals.isRunning = true;
                globals.trajetHandler.handleTrajet();
            }
            else
            {
                startButtonText = "Lancer";
                globals.isRunning = false;
            }
        }
        #endregion
    }
}