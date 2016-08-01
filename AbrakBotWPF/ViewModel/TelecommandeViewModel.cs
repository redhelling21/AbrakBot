using AbrakBotWPF.Model.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.ViewModel
{
    public class TelecommandeViewModel : ViewModelBase
    {
        public Globals globals;
        public RelayCommand UpCommand { get; private set; }
        public RelayCommand DownCommand { get; private set; }
        public RelayCommand LeftCommand { get; private set; }
        public RelayCommand RightCommand { get; private set; }
        public RelayCommand CaseCommand { get; private set; }

        public TelecommandeViewModel()
        {
            UpCommand = new RelayCommand(up);
            DownCommand = new RelayCommand(down);
            LeftCommand = new RelayCommand(left);
            RightCommand = new RelayCommand(right);
            CaseCommand = new RelayCommand(toCase);
        }

        private string _caseGo;
        public string caseGo
        {
            get { return _caseGo; }
            set
            {
                if (_caseGo == value) return;
                _caseGo = value;
                RaisePropertyChanged("caseGo");
            }
        }

        private bool? _isMapChanger = false;
        public bool? isMapChanger
        {
            get { return (_isMapChanger != null) ? _isMapChanger : false; }
            set
            {
                _isMapChanger = value;
                RaisePropertyChanged("isMapChanger");
            }
        }

        private void up()
        {
            globals.makeAMove(globals.tpHaut, true);
        }

        private void down()
        {
            globals.makeAMove(globals.tpBas, true);
        }

        private void left()
        {
            globals.makeAMove(globals.tpGauche, true);
        }

        private void right()
        {
            globals.makeAMove(globals.tpDroite, true);
        }

        private void toCase()
        {
            globals.makeAMove(Int32.Parse(_caseGo), _isMapChanger.HasValue ? _isMapChanger.Value : false);
        }
    }
}
