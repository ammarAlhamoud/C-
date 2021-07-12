using Monopoly.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1;

namespace Monopoly.ViewModels
{
    public class GameManagerViewModel : BasicViewModel
    {
       
        public GameManagerViewModel()
        {
            
            PlayersNumberCommand = new RelayCommand(PLayersNumberCanExcute, PlayersNumberExcute);
        }
        //PlayersNumber Property
        private int _playersNumber;
        public int PlayersNumber
        {
            get { return _playersNumber; }
            set
            {
                _playersNumber = value;

                OnPropertyChange("PlayersNumber");
            }
        }

        private string _strPlayersNumber;
        public string StrPlayersNumber
        {
            get { return _strPlayersNumber; }
            set
            {
                _strPlayersNumber = value;
                if (_strPlayersNumber.Equals("Multiplayer"))
                    _playersNumber = 2;
                else if (_strPlayersNumber.Equals("3 Players"))
                    _playersNumber = 3;
                else if (_strPlayersNumber.Equals("4 Players"))
                    _playersNumber = 4;
                OnPropertyChange("StrPlayersNumber");
            }
        }
       

        private ObservableCollection<PlayerBase> _players;
        public ObservableCollection<PlayerBase> Players
        {
            get { return _players; }
            set
            {
                _players = value;

                OnPropertyChange("Players");
            }
        }
        public ICommand PlayersNumberCommand { get; set; }
        //PlayersNumber Methode
        private void PlayersNumberExcute(object parameter)
        {
            StrPlayersNumber = (String)parameter;
        }
        private bool PLayersNumberCanExcute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
     
    }
}
