using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Classes
{
    public class Ressource : ObservableObject
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

        private int _caseID;
        public int caseID
        {
            get { return _caseID; }
            set
            {
                if (_caseID == value) return;
                _caseID = value;
                RaisePropertyChanged("caseID");
            }
        }

        private bool _etat;
        public bool etat
        {
            get { return _etat; }
            set
            {
                if (_etat == value) return;
                _etat = value;
                RaisePropertyChanged("etat");
            }
        }

        public Ressource(int id, int caseID, string libelle, bool etat)
        {
            this.id = id;
            this.caseID = caseID;
            this.libelle = libelle;
            this.etat = etat;
        }
    }
}
