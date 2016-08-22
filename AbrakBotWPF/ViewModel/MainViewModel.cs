using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AbrakBotWPF.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        //COMMANDS
        public RelayCommand ConnectCommand { get; private set; }
        public RelayCommand TelecommandeCommand { get; private set; }
        public RelayCommand TestCommand { get; private set; }
        public RelayCommand LaunchCommand { get; private set; }
        public RelayCommand AddToRoutineCommand { get; private set; }
        public RelayCommand DeleteSpellFightCommand { get; private set; }
        public RelayCommand SaveConfigCommand { get; private set; }
        public RelayCommand ResetConfigCommand { get; private set; }
        public RelayCommand SaveConfigFileCommand { get; private set; }
        public Globals globals;
        public MainWindow window;
        
        public MainViewModel()
        {

            this.actualResources = new ObservableCollection<Ressource>();
            this.sorts = new ObservableCollection<Sort>();
            this.sortsCombat = new ObservableCollection<SortCombat>();
            this.sortsCombat.CollectionChanged += sortsCombatsChanged;
            this.inventaire = new ObservableCollection<Item>();
            Messenger.Default.Register<AddLineToBoxMessage>(this,
                 (addLineMessage) => ReceiveAddLineToBox(addLineMessage)
            );
            Messenger.Default.Register<PlayerStatChangedMessage>(this,
                 (statChangedMessage) => ReceivePlayerStatChanged(statChangedMessage)
            );
            Messenger.Default.Register<PlayerJobsChangedMessage>(this,
                 (jobsChangedMessage) => ReceivePlayerJobsChanged(jobsChangedMessage)
            );
            Messenger.Default.Register<MapStatChangedMessage>(this,
                 (mapStatChangedMessage) => ReceiveMapStatChanged(mapStatChangedMessage)
            );
            Messenger.Default.Register<MapResourcesChangedMessage>(this,
                 (mapResourcesChangedMessage) => ReceiveMapResourcesChanged(mapResourcesChangedMessage)
            );
            Messenger.Default.Register<InventoryChangedMessage>(this,
                 (inventoryChangedMessage) => ReceiveInventoryChanged(inventoryChangedMessage)
            );
            Messenger.Default.Register<PlayerStateChangedMessage>(this,
                 (playerStateChangedMessage) => ReceivePlayerStateChanged(playerStateChangedMessage)
            );
            Messenger.Default.Register<HarvestedResourceMessage>(this,
                 (harvestedResourceMessage) => ReceiveHarvestedResource(harvestedResourceMessage)
            );
            Messenger.Default.Register<SpellsChangedMessage>(this,
                 (spellsChangedMessage) => ReceiveSpellsChanged(spellsChangedMessage)
            );
            Messenger.Default.Register<EndedFightMessage>(this,
                 (endedFightMessage) => ReceiveEndedFight(endedFightMessage)
            );
            ConnectCommand = new RelayCommand(connect);
            TelecommandeCommand = new RelayCommand(telecommande);
            TestCommand = new RelayCommand(test);
            LaunchCommand = new RelayCommand(launch);
            AddToRoutineCommand = new RelayCommand(addSpellToFightingRoutine);
            DeleteSpellFightCommand = new RelayCommand(deleteSpellFight);
            SaveConfigCommand = new RelayCommand(saveConfig);
            ResetConfigCommand = new RelayCommand(resetConfig);
            SaveConfigFileCommand = new RelayCommand(saveConfigFile);
        }

        public void initializeGlobals()
        {
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


        private ObservableCollection<Ressource> _actualResources;
        public ObservableCollection<Ressource> actualResources
        {
            get { return _actualResources; }
            set
            {
                if (_actualResources == value) return;
                _actualResources = value;
                RaisePropertyChanged("actualResources");
            }
        }

        private ObservableCollection<Item> _inventaire;
        public ObservableCollection<Item> inventaire
        {
            get { return _inventaire; }
            set
            {
                if (_inventaire == value) return;
                _inventaire = value;
                RaisePropertyChanged("inventaire");
            }
        }
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

        //PERSO A CONNECTER
        private string _selectedCharacter;
        public string selectedCharacter
        {
            get { return _selectedCharacter; }
            set
            {
                if (_selectedCharacter == value) return;
                _selectedCharacter = value;
                RaisePropertyChanged("selectedCharacter");
                Config.load(selectedCharacter);
                applyConfig();
                globals.writeToMainBox("Configuration chargée !\n", "Orange");
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

        #region CONFIGTAB

        private int _nbMinMonstres = 0;
        public int nbMinMonstres
        {
            get { return _nbMinMonstres; }
            set
            {
                _nbMinMonstres = value;
                RaisePropertyChanged("nbMinMonstres");
            }
        }

        private int _nbMaxMonstres = 8;
        public int nbMaxMonstres
        {
            get { return _nbMaxMonstres; }
            set
            {
                _nbMaxMonstres = value;
                RaisePropertyChanged("nbMaxMonstres");
            }
        }

        private int _lvlMinMonstres = 0;
        public int lvlMinMonstres
        {
            get { return _lvlMinMonstres; }
            set
            {
                _lvlMinMonstres = value;
                RaisePropertyChanged("lvlMinMonstres");
            }
        }

        private int _lvlMaxMonstres = 9999;
        public int lvlMaxMonstres
        {
            get { return _lvlMaxMonstres; }
            set
            {
                _lvlMaxMonstres = value;
                RaisePropertyChanged("lvlMaxMonstres");
            }
        }

        private int _percentBank = 70;
        public int percentBank
        {
            get { return _percentBank; }
            set
            {
                _percentBank = value;
                RaisePropertyChanged("percentBank");
            }
        }

        private int _percentRegen = 0;
        public int percentRegen
        {
            get { return _percentRegen; }
            set
            {
                _percentRegen = value;
                RaisePropertyChanged("percentRegen");
            }
        }

        #endregion

        #region FIGHTTAB
        private ObservableCollection<SortCombat> _sortsCombat;
        public ObservableCollection<SortCombat> sortsCombat
        {
            get { return _sortsCombat; }
            set
            {
                if (_sortsCombat == value) return;
                _sortsCombat = value;
                RaisePropertyChanged("sortsCombat");
            }
        }

        private SortCombat _selectedSpellFight;
        public SortCombat selectedSpellFight
        {
            get { return _selectedSpellFight; }
            set
            {
                if (_selectedSpellFight == value) return;
                _selectedSpellFight = value;
                RaisePropertyChanged("selectedSpellFight");
            }
        }
        #endregion

        #region REPORTTAB

        #region RECOLTE
        private int _resourceCount = 0;
        public int resourceCount
        {
            get { return _resourceCount; }
            set
            {
                _resourceCount = value;
                RaisePropertyChanged("resourceCount");
            }
        }

        private int _nodeCount = 0;
        public int nodeCount
        {
            get { return _nodeCount; }
            set
            {
                _nodeCount = value;
                RaisePropertyChanged("nodeCount");
            }
        }
        #endregion

        #region COMBAT

        private int _objectCount = 0;
        public int objectCount
        {
            get { return _objectCount; }
            set
            {
                _objectCount = value;
                RaisePropertyChanged("objectCount");
            }
        }

        private int _kamasCount = 0;
        public int kamasCount
        {
            get { return _kamasCount; }
            set
            {
                _kamasCount = value;
                RaisePropertyChanged("kamasCount");
            }
        }

        private int _fightCount = 0;
        public int fightCount
        {
            get { return _fightCount; }
            set
            {
                _fightCount = value;
                RaisePropertyChanged("fightCount");
            }
        }

        private int _xpCount = 0;
        public int xpCount
        {
            get { return _xpCount; }
            set
            {
                _xpCount = value;
                RaisePropertyChanged("xpCount");
            }
        }

        private float _dureeCount = 0;
        public float dureeCount
        {
            get { return _dureeCount/_fightCount; }
            set
            {
                _dureeCount = value;
                RaisePropertyChanged("dureeCount");
            }
        }
        #endregion

        #endregion

        #region PERSOTAB

        private ObservableCollection<Sort> _sorts;
        public ObservableCollection<Sort> sorts
        {
            get { return _sorts; }
            set
            {
                if (_sorts == value) return;
                _sorts = value;
                RaisePropertyChanged("sorts");
            }
        }

        private Sort _selectedSpell;
        public Sort selectedSpell
        {
            get { return _selectedSpell; }
            set
            {
                if (_selectedSpell == value) return;
                _selectedSpell = value;
                RaisePropertyChanged("selectedSpell");
            }
        }

        private string _PA = "0";
        public string PA
        {
            get { return _PA; }
            set
            {
                _PA = value;
                RaisePropertyChanged("PA");
            }
        }

        private string _PM = "0";
        public string PM
        {
            get { return _PM; }
            set
            {
                _PM = value;
                RaisePropertyChanged("PM");
            }
        }

        private string _initiative = "0";
        public string initiative
        {
            get { return _initiative; }
            set
            {
                _initiative = value;
                RaisePropertyChanged("initiative");
            }
        }

        private string _prospection = "0";
        public string prospection
        {
            get { return _prospection; }
            set
            {
                _prospection = value;
                RaisePropertyChanged("prospection");
            }
        }

        private string _vie = "0";
        public string vie
        {
            get { return _vie; }
            set
            {
                _vie = value;
                RaisePropertyChanged("vie");
            }
        }

        private string _force = "0";
        public string force
        {
            get { return _force; }
            set
            {
                _force = value;
                RaisePropertyChanged("force");
            }
        }

        private string _sagesse = "0";
        public string sagesse
        {
            get { return _sagesse; }
            set
            {
                _sagesse = value;
                RaisePropertyChanged("sagesse");
            }
        }

        private string _intelligence = "0";
        public string intelligence
        {
            get { return _intelligence; }
            set
            {
                _intelligence = value;
                RaisePropertyChanged("intelligence");
            }
        }

        private string _chance = "0";
        public string chance
        {
            get { return _chance; }
            set
            {
                _chance = value;
                RaisePropertyChanged("chance");
            }
        }

        private string _agilite = "0";
        public string agilite
        {
            get { return _agilite; }
            set
            {
                _agilite = value;
                RaisePropertyChanged("agilite");
            }
        }
        #endregion

        #region STATUSBAR
        //ETAT
        private string _stateText = "Inactif";
        public string stateText
        {
            get { return _stateText; }
            set
            {
                _stateText = value;
                RaisePropertyChanged("stateText");
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
                    window.mainBox.ScrollToEnd();
                }));
            }
            else if (action.boxType == "debug")
            {
                window.mainBox.Dispatcher.Invoke((Action)(() =>
                {
                    window.debugBox.AppendText(action.text, action.color);
                    window.debugBox.ScrollToEnd();
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
                case "PA":
                    PA = action.value.ToString();
                    break;
                case "PM":
                    PM = action.value.ToString();
                    break;
                case "initiative":
                    initiative = action.value.ToString();
                    break;
                case "prospection":
                    prospection = action.value.ToString();
                    break;
                case "vie":
                    vie = action.value.ToString();
                    break;
                case "sagesse":
                    sagesse = action.value.ToString();
                    break;
                case "force":
                    force = action.value.ToString();
                    break;
                case "intelligence":
                    intelligence  = action.value.ToString();
                    break;
                case "chance":
                    chance = action.value.ToString();
                    break;
                case "agilite":
                    agilite = action.value.ToString();
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

        private void ReceiveMapStatChanged(MapStatChangedMessage action)
        {
            mapCoords = action.mapCoords;
        }

        private void ReceiveMapResourcesChanged(MapResourcesChangedMessage action)
        {

            this.window.Dispatcher.Invoke(() =>
            {
                this.actualResources.Clear();
                foreach (Ressource res in action.ressources)
                {
                    this.actualResources.Add(res);
                }
            });


        }

        private void ReceiveInventoryChanged(InventoryChangedMessage action)
        {

            this.window.Dispatcher.Invoke(() =>
            {
                this.inventaire.Clear();
                foreach (Item item in action.inventory)
                {
                    this.inventaire.Add(item);
                }
            });

        }

        private void ReceiveSpellsChanged(SpellsChangedMessage action)
        {

            this.window.Dispatcher.Invoke(() =>
            {
                this.sorts.Clear();
                foreach (Sort sort in action.spells)
                {
                    this.sorts.Add(sort);
                }
            });

        }

        private void ReceivePlayerStateChanged(PlayerStateChangedMessage action)
        {
            if (action.isInExchange)
            {
                stateText = "Echange";
            }
            else if (action.isFighting)
            {
                stateText = "Combat";
            }else if (action.isMoving)
            {
                stateText = "Mouvement";
            }else if (action.isHarvesting)
            {
                stateText = "Recolte";
            }
            else
            {
                stateText = "Inactif";
            }
        }

        private void ReceiveHarvestedResource(HarvestedResourceMessage action)
        {

            resourceCount = resourceCount + action.qte;
            nodeCount++;

        }

        private void ReceiveEndedFight(EndedFightMessage action)
        {
            this.kamasCount += action.kamas;
            this.dureeCount += action.duree;
            this.xpCount += action.xp;
            this.fightCount += 1;
            this.objectCount += action.objects;
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

        private void deleteSpellFight()
        {
            if(_selectedSpellFight != null)
            {
                sortsCombat.Remove(_selectedSpellFight);
                //RaisePropertyChanged("sortsCombat");
            }
        }

        private void addSpellToFightingRoutine()
        {
            if (selectedSpell != null)
            {
                this.sortsCombat.Add(new SortCombat(selectedSpell.id, selectedSpell.libelle, selectedSpell.niveau, 1000, "Ennemi"));
            }
        }

        private void telecommande()
        {
            var win = new Telecommande();
            TelecommandeViewModel model = ServiceLocator.Current.GetInstance<TelecommandeViewModel>();
            model.globals = globals;
            win.Show();
        }

        private void test()
        {
            globals.doSomethingToTest();
        }

        private void saveConfig()
        {
            globals.podsPercentLimit = percentBank;
            globals.percentRegen = percentRegen;
            globals.nbMaxMonstres = nbMaxMonstres;
            globals.nbMinMonstres = nbMinMonstres;
            globals.lvlMaxMonstres = lvlMaxMonstres;
            globals.lvlMinMonstres = lvlMinMonstres;
        }

        private void resetConfig()
        {
            percentBank = Config.podsPercentLimit;
            percentRegen = Config.percentRegen;
            nbMaxMonstres = Config.nbMaxMonstres;
            nbMinMonstres = Config.nbMinMonstres;
            lvlMaxMonstres = Config.lvlMaxMonstres;
            lvlMinMonstres = Config.lvlMinMonstres;
        }

        private void applyConfig()
        {
            percentBank = Config.podsPercentLimit;
            percentRegen = Config.percentRegen;
            nbMaxMonstres = Config.nbMaxMonstres;
            nbMinMonstres = Config.nbMinMonstres;
            lvlMaxMonstres = Config.lvlMaxMonstres;
            lvlMinMonstres = Config.lvlMinMonstres;
            foreach(SortCombat sort in Config.sortsCombat)
            {
                this.sortsCombat.Add(sort);
            }
        }

        private void saveConfigFile()
        {
            Config.save(selectedCharacter);
        }

        private void updateRoutine(object sender, PropertyChangedEventArgs e)
        {
            globals.player.sortsCombat = new List<SortCombat>(_sortsCombat);
        }

        private void sortsCombatsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            globals.player.sortsCombat = new List<SortCombat>(_sortsCombat);
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (SortCombat sort in e.OldItems)
                {
                    sort.PropertyChanged -= updateRoutine;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (SortCombat sort in e.NewItems)
                {
                    sort.PropertyChanged += updateRoutine;
                }
            }
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