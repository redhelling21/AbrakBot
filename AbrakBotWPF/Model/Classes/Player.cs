using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Classes
{
    public class Player : ObservableObject
    {
        private int _level;
        private int _kamas;
        private int _xp;
        private int _xp_bas;
        private int _xp_max;
        private int _pdv;
        private int _pdv_max;
        private int _energie;
        private int _energie_max;
        private int _pods;
        private int _pods_max;
        private string _pseudo;
        public List<Int32> harvestables = new List<Int32>();
        public List<Metier> metiers = new List<Metier>();
        public List<Item> inventaire = new List<Item>();
        public string id;

        //L'ensemble de ces proprietes envoie un message au ViewModel lorsqu'elles sont mises a jour

        public int level
        {
            get
            {
                return _level;
            }

            set
            {
                //#3
                _level = value;
                var msg = new PlayerStatChangedMessage() { stat = "level", value = value };
                Messenger.Default.Send<PlayerStatChangedMessage>(msg);
            }
        }
        public int kamas
        {
            get
            {
                return _kamas;
            }

            set
            {
                //#3
                _kamas = value;
                var msg = new PlayerStatChangedMessage() { stat = "kamas", value = value };
                Messenger.Default.Send<PlayerStatChangedMessage>(msg);
            }
        }
        public int xp
        {
            get
            {
                return _xp;
            }

            set
            {
                //#3
                _xp = value;
                var msg = new PlayerStatChangedMessage() { stat = "xp", value = (int)Math.Round(((float)(_xp - _xp_bas) / (_xp_max - _xp_bas)) * 100) };
                Messenger.Default.Send<PlayerStatChangedMessage>(msg);
            }
        }
        public int xp_bas
        {
            get
            {
                return _xp_bas;
            }

            set
            {
                _xp_bas = value;
            }
        }
        public int xp_max
        {
            get
            {
                return _xp_max;
            }
            set
            {
                _xp_max = value;
            }
        }
        public int pdv
        {
            get
            {
                return _pdv;
            }

            set
            {

                _pdv = value;
                var msg = new PlayerStatChangedMessage() { stat = "pdv", value = (int)Math.Round(((float)(_pdv) / (_pdv_max)) * 100) };
                Messenger.Default.Send<PlayerStatChangedMessage>(msg);
            }
        }
        public int pdv_max
        {
            get
            {
                return _pdv_max;
            }

            set
            {
                _pdv_max = value;
            }
        }
        public int energie
        {
            get
            {
                return _energie;
            }

            set
            {
                _energie = value;
                var msg = new PlayerStatChangedMessage() { stat = "energie", value = (int)Math.Round(((float)(_energie) / (_energie_max)) * 100) };
                Messenger.Default.Send<PlayerStatChangedMessage>(msg);
            }
        }
        public int energie_max
        {
            get
            {
                return _energie_max;
            }

            set
            {
                _energie_max = value;
            }
        }
        public int pods
        {
            get
            {
                return _pods;
            }

            set
            {
                _pods = value;
                var msg = new PlayerStatChangedMessage() { stat = "pods", value = (int)Math.Round(((float)(_pods) / _pods_max) * 100) };
                Messenger.Default.Send<PlayerStatChangedMessage>(msg);
            }
        }
        public int pods_max
        {
            get
            {
                return _pods_max;
            }

            set
            {
                _pods_max = value;
            }
        }
        public string pseudo
        {
            get
            {
                return _pseudo;
            }

            set
            {
                _pseudo = value;
                var msg = new PlayerStatChangedMessage() { stat = "pseudo", valString = value };
                Messenger.Default.Send<PlayerStatChangedMessage>(msg);
            }
        }

    }
}
