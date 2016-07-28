using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;

namespace AbrakBotWPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        //COMMANDS
        public RelayCommand ConnectCommand { get; private set; }
        public RelayCommand TelecommandeCommand { get; private set; }
        public RelayCommand TestCommand { get; private set; }
        public RelayCommand LaunchCommand { get; private set; }
        public Globals globals;
        public MainWindow window;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

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
    }
}