using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBot
{
    class Player
    {
        private static int _level;
        private static int _kamas;
        private static int _xp;
        private static int _xp_bas;
        private static int _xp_max;
        private static int _pdv;
        private static int _pdv_max;
        private static int _energie;
        private static int _energie_max;
        private static int _pods;
        private static int _pods_max;
        private static string _pseudo;
        public static List<Item> inventaire = new List<Item>();

        public static int level
        {
            get
            {
                return _level;
            }

            set
            {
                //#3
                _level = value;
                Globals.updateLevel(_level.ToString());
            }
        }
        public static int kamas
        {
            get
            {
                return _kamas;
            }

            set
            {
                //#3
                _kamas = value;
                Globals.updateKamas(_kamas.ToString());
            }
        }
        public static int xp
        {
            get
            {
                return _xp;
            }

            set
            {
                //#3
                _xp = value;
                Globals.updateBars(-1, (int)Math.Round(((float)(_xp - _xp_bas) / (_xp_max - _xp_bas)) * 100), -1, -1);
            }
        }
        public static int xp_bas
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
        public static int xp_max
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
        public static int pdv
        {
            get
            {
                return _pdv;
            }

            set
            {
                
                _pdv = value;
                Globals.updateBars((int)Math.Round(((float)(_pdv) / (_pdv_max)) * 100), -1, -1, -1);
            }
        }
        public static int pdv_max
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
        public static int energie
        {
            get
            {
                return _energie;
            }

            set
            {
                _energie = value;
                Globals.updateBars(-1, -1, -1, (int)Math.Round(((float)(_energie) / (_energie_max)) * 100));
            }
        }
        public static int energie_max
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
        public static int pods
        {
            get
            {
                return _pods;
            }

            set
            {
                _pods = value;
                Globals.updateBars(-1, -1, (int)Math.Round(((float)(_pods) / _pods_max) * 100), -1);
            }
        }
        public static int pods_max
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
        public static string pseudo
        {
            get
            {
                return _pseudo;
            }

            set
            {
                _pseudo = value;
                Globals.updateCharName(_pseudo);
            }
        }

    }
}
