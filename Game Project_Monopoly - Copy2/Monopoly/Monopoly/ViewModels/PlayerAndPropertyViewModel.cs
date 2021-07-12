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
    public class PlayerAndPropertyViewModel : GameManagerViewModel
    {

        //Players Property
        private int _currentPlayerID;

        public int CurrentPlayerID
        {
            get { return _currentPlayerID; }
            set
            {
                _currentPlayerID = value;
                OnPropertyChange("CurrentPlayerID");
            }
        }
        private string _str_currentPlayerID;

        public string StrCurrentPlayerID
        {
            get
            {   
                return _str_currentPlayerID;
            }
            set
            {
                if (_currentPlayerID == 1)
                {
                    _str_currentPlayerID = _player1Name;
                }
                else if (_currentPlayerID == 2)
                {
                    _str_currentPlayerID = _player2Name;
                }
                else if (_currentPlayerID == 3)
                {
                    _str_currentPlayerID = _player3Name;
                }
                else
                {
                    _str_currentPlayerID = _player4Name;
                }
                _str_currentPlayerID = value;
                OnPropertyChange("CurrentPlayerID");
            }
        }

        public int _nexttPlayerID;
        public int NextPlayerID
        {
            get { return _nexttPlayerID; }
            set
            {
                _nexttPlayerID = CurrentPlayerID + 1;
               
                _nexttPlayerID = value;
                OnPropertyChange("NextPlayer");
            }
        }

        //Player 1
        public string _player1Name;
        public string Player1Name
        {
            get { return _player1Name; }
            set
            {

                _player1Name = value;
                OnPropertyChange("Player1Name");
            }
        }
        //Player 2
        public string _player2Name;
        public string Player2Name
        {
            get { return _player2Name; }
            set
            {
                _player2Name = value;
                OnPropertyChange("Player2Name");
            }
        }
        //Player 3
        public string _player3Name;
        public string Player3Name
        {
            get { return _player3Name; }
            set
            {
                _player3Name = value;
                OnPropertyChange("Player3Name");
            }
        }
        //Player 4
        public string _player4Name;
        public string Player4Name
        {
            get { return _player4Name; }
            set
            {
                _player4Name = value;
                OnPropertyChange("Player4Name");
            }
        }


        //player 1 postition
        public int _player1Position_Column = 23;
        public int Player1Position_Column
        {
            get { return _player1Position_Column; }
            set
            {
                _player1Position_Column = value;
                OnPropertyChange("Player1Position_Column");
            }
        }
        public int _player1Position_Row = 22;
        public int Player1Position_Row
        {
            get { return _player1Position_Row; }
            set
            {
                _player1Position_Row = value;
                OnPropertyChange("Player1Position_Row");
            }
        }
        //player 2 postition
        public int _player2Position_Column = 22;
        public int Player2Position_Column
        {
            get { return _player2Position_Column; }
            set
            {
                _player2Position_Column = value;
                OnPropertyChange("Player2Position_Column");
            }
        }
        public int _player2Position_Row = 22;
        public int Player2Position_Row
        {
            get { return _player2Position_Row; }
            set
            {
                _player2Position_Row = value;
                OnPropertyChange("Player2Position_Row");
            }
        }
        //player 3 postition
        public int _player3Position_Column = 23;
        public int Player3Position_Column
        {
            get { return _player3Position_Column; }
            set
            {
                _player3Position_Column = value;
                OnPropertyChange("Player3Position_Column");
            }
        }
        public int _player3Position_Row = 23;
        public int Player3Position_Row
        {
            get { return _player3Position_Row; }
            set
            {
                _player3Position_Row = value;
                OnPropertyChange("Player3Position_Row");
            }
        }
        //player 4 postition
        public int _player4Position_Column = 22;
        public int Player4Position_Column
        {
            get { return _player4Position_Column; }
            set
            {
                _player4Position_Column = value;
                OnPropertyChange("Player4Position_Column");
            }
        }
        public int _player4Position_Row = 23;
        public int Player4Position_Row
        {
            get { return _player4Position_Row; }
            set
            {
                _player4Position_Row = value;
                OnPropertyChange("Player4Position_Row");
            }
        }

        //startgame command
        private ICommand _StartNewGameCommand;
        public ICommand StartNewGameCommand
        {
            get
            {
                return _StartNewGameCommand ?? (_StartNewGameCommand = new RelayCommand(p => CanStartGame(), p => StartGame(PlayersNumber)));
            }

        }

        // StartGame Methode
        public void StartGame(int playersNumber)
        {
            GameManager MyGameManager = new GameManager(playersNumber);

            LoadPlayers();
            CurrentPlayerID = 1;
            StrCurrentPlayerID = _player1Name;
        }

        private ICommand _setNextPlayerCommand;
        public ICommand SetNextPlayerCommand
        {
            get
            {
                return _setNextPlayerCommand ?? (_setNextPlayerCommand = new RelayCommand(p => true, p => SetnextPlayer()));
            }

        }

        public void SetnextPlayer()
        {
            _currentPlayerID++ ;
            if (CurrentPlayerID > PlayersNumber)
                _currentPlayerID = 0;
        }

        public bool CanStartGame()
        {
            return PlayersNumber >= 2;
        }


        public void LoadPlayers()
        {
            Players = new ObservableCollection<PlayerBase>();
            if (PlayersNumber == 2)
            {
                Players.Add(new PlayerBase { Name = _player1Name, ID = 1, Credit = 1500, CurrentPosition_Colmun = _player1Position_Column, CurrentPosition_Row = _player1Position_Row, FigurSource = new Uri(@"images\figur red.png", UriKind.Relative) });
                Players.Add(new PlayerBase { Name = _player2Name, ID = 2, Credit = 1500, CurrentPosition_Colmun = _player2Position_Column, CurrentPosition_Row = _player2Position_Row, FigurSource = new Uri(@"images\figurGreen.png", UriKind.Relative) });
            }
            else if (PlayersNumber == 3)
            {
                Players.Add(new PlayerBase { Name = _player1Name, ID = 1, Credit = 1500, CurrentPosition_Colmun = _player1Position_Column, CurrentPosition_Row = _player1Position_Row, FigurSource = new Uri(@"images\figur red.png", UriKind.Relative) });
                Players.Add(new PlayerBase { Name = _player2Name, ID = 2, Credit = 1500, CurrentPosition_Colmun = _player2Position_Column, CurrentPosition_Row = _player2Position_Row, FigurSource = new Uri(@"images\figurGreen.png", UriKind.Relative) });
                Players.Add(new PlayerBase { Name = _player3Name, ID = 3, Credit = 1500, CurrentPosition_Colmun = _player3Position_Column, CurrentPosition_Row = _player3Position_Row, FigurSource = new Uri(@"images\figur Blue.png", UriKind.Relative) });
            }
            else if (PlayersNumber == 4)
            {
                Players.Add(new PlayerBase { Name = _player1Name, ID = 1, Credit = 1500, CurrentPosition_Colmun = _player1Position_Column, CurrentPosition_Row = _player1Position_Row, FigurSource = new Uri(@"images\figur red.png", UriKind.Relative) });
                Players.Add(new PlayerBase { Name = _player2Name, ID = 2, Credit = 1500, CurrentPosition_Colmun = _player2Position_Column, CurrentPosition_Row = _player2Position_Row, FigurSource = new Uri(@"images\figurGreen.png", UriKind.Relative) });
                Players.Add(new PlayerBase { Name = _player3Name, ID = 3, Credit = 1500, CurrentPosition_Colmun = _player3Position_Column, CurrentPosition_Row = _player3Position_Row, FigurSource = new Uri(@"images\figur Blue.png", UriKind.Relative) });
                Players.Add(new PlayerBase { Name = _player4Name, ID = 4, Credit = 1500, CurrentPosition_Colmun = _player4Position_Column, CurrentPosition_Row = _player4Position_Row, FigurSource = new Uri(@"images\figur yello.png", UriKind.Relative) });
            }

        }
    }
}