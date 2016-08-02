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

        private string _uniqueID; //ID unique généré lors du stockage dans l'inventaire
        public string uniqueID
        {
            get { return _uniqueID; }
            set
            {
                if (_uniqueID == value) return;
                _uniqueID = value;
                RaisePropertyChanged("uniqueID");
            }
        }

        private int _id; //ID de l(item
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

        private string _libelle; //Nom de l'item
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

        private int _quantite; //Quantite de l'item
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

        private bool _isEquipped; //Quantite de l'item
        public bool isEquipped
        {
            get { return _isEquipped; }
            set
            {
                if (_isEquipped == value) return;
                _isEquipped = value;
                RaisePropertyChanged("isEquipped");
            }
        }

        public Item(string uniqueID, int id, string libelle, int quantite, bool isEquipped)
        {
            this.uniqueID = uniqueID;
            this.id = id;
            this.libelle = libelle;
            this.quantite = quantite;
            this.isEquipped = isEquipped;
        }
    }
}
