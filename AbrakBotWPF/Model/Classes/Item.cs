using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Classes
{
    public class Item : ObservableObject
    {

        private int _id;
        public int id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                RaisePropertyChanged("id");
            }
        }

        private string _libelle;
        public string libelle
        {
            get { return _libelle; }
            set
            {
                if (_libelle == value) return;
                _libelle = value;
                RaisePropertyChanged("libelle");
            }
        }

        private int _quantite;
        public int quantite
        {
            get { return _quantite; }
            set
            {
                if (_quantite == value) return;
                _quantite = value;
                RaisePropertyChanged("quantite");
            }
        }

        public Item(int id, string libelle, int quantite)
        {
            this.id = id;
            this.libelle = libelle;
            this.quantite = quantite;
        }
    }
}
