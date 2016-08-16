using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Classes
{
    public class SortCombat : Sort
    {
        private int _priorite;
        public int priorite
        {
            get { return _priorite; }
            set
            {
                if (_priorite == value) return;
                _priorite = value;
                RaisePropertyChanged("priorite");
            }
        }

        private string _target;
        public string target
        {
            get { return _target; }
            set
            {
                if (_target == value) return;
                _target = value;
                RaisePropertyChanged("target");
            }
        }

        public SortCombat(int id, string libelle, int niveau, int priorite, string target) : base(id, libelle, niveau)
        {
            this.priorite = priorite;
            this.target = target;
        }
    }
}
