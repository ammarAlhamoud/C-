using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Infrastructure.UI.Base;
using Balls.Common.Interfaces;
using Balls.Common.Infrastructure.UI.Controls;
using Balls.Common.Models;
using Balls.Business.Services;
using Balls.Common.Infrastructure;

namespace Balls.UI.ViewModel
{
    public class GameChooserViewModel : ViewModelBase, IShellViewModel
    {

        #region Variables & Properties
        private PlayerModel _playerModel;

        public Common.Interfaces.Base.IBaseView View
        {
            get;
            set;
        }

        public string Name
        {
            get
            {
                return _playerModel.Name;
            }
            set
            {
                _playerModel.Name = value;
                this.OnPropertyChanged(() => Name);
                this.OnPropertyChanged(() => IsNameAvailable);
            }
        }

        public bool IsNameAvailable
        {
            get
            {
                if (null != _playerModel && !string.IsNullOrEmpty(_playerModel.Name))
                    return true;
                else
                    return false;
            }

        }

        public PlayerModel Player
        {
            get
            {
                return _playerModel;
            }
            set
            {
                _playerModel = value;
                this.OnPropertyChanged(() => Player);

            }
        }

        private DelegateCommand _navigateCommand;
        public DelegateCommand NavigateCommand
        {
            get
            {
                return _navigateCommand ??
                    (_navigateCommand = new DelegateCommand(NavigateClick, CanNavigateClick));
            }
        }
        #endregion

        #region ctor
        public GameChooserViewModel(IGameChooserView view)
        {
            _playerModel = new PlayerModel();

            this.View = view;
            this.View.SetViewModel(this);

        }
        #endregion

        #region Methods
        public void LoadData(string uri)
        {
            int i = 1;
            if (uri.UriQueryParameterCount() == 1)
                i = uri.UriQueryParameters()[0].ToInt();

        }

        public ScoreCardService ScoreCardService
        {
            get
            {
                return new ScoreCardService();
            }
        }

        private bool CanNavigateClick(object param)
        {
            return true;
        }

        private void AddPlayer()
        {
            _playerModel.ScoreCards = new List<ScoreCardModel>()
                {
                    new ScoreCardModel{
                            Level = 1,
                            Attempt = 1,
                            Score = 0,
                            GameRecno = 1,
                            IsNeedUpdateDB = true,
                            PlayerRecno = _playerModel.Recno                            
                    },
                     new ScoreCardModel{
                            Level = 1,
                            Attempt = 1,
                            Score = 0,
                            GameRecno = 2,
                            IsNeedUpdateDB = true,
                            PlayerRecno = _playerModel.Recno                            
                    }
                };

            _playerModel = ScoreCardService.AddUpdatePlayerDetail(_playerModel);
        }

        private void NavigateClick(object param)
        {
            int i = 1;
            int level = 1;
            string navigate = "";
            i = param.ToString().ToInt();

            AddPlayer();

            if (i == 1)
                navigate = string.Format("{0}?{1}", XAML.BallCounterGame, level);
            else
                navigate = string.Format("{0}?{1}", XAML.GamePage, level);

            "".APPPageData().IsoSetData(_playerModel);

            navigate.Navigate();

        }
        #endregion

    }
}
