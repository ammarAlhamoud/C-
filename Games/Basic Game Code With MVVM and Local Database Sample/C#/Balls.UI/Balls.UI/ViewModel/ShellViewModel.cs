using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Infrastructure.UI.Base;
using Balls.Common.Interfaces;
using Balls.Common.Infrastructure.UI.Controls;
using Balls.Business.Services;
using Balls.Common.Infrastructure;
using Balls.Common.Infrastructure.UI.Settings;
using System.Windows;

namespace Balls.UI.ViewModel
{
    public class ShellViewModel : ViewModelBase, IGameChooserViewModel
    {
        #region Variables & Properties
        private bool _canContinueGame = false;

        public Common.Interfaces.Base.IBaseView View
        {
            get;
            set;
        }

        private string _soundImage;
        public string SoundImage
        {
            get { return _soundImage; }
            set
            {
                _soundImage = value;
                this.OnPropertyChanged(() => SoundImage);

            }
        }

        public ScoreCardService ScoreCardService
        {
            get
            {
                return new ScoreCardService();
            }
        }

        private DelegateCommand _newGameCommand;
        public DelegateCommand NewGameCommand
        {
            get
            {
                return _newGameCommand ??
                (_newGameCommand = new DelegateCommand(NewGameClick, CanNewGameClick));

            }
        }

        private DelegateCommand _continueGameCommand;
        public DelegateCommand ContinueGameCommand
        {
            get
            {
                return _continueGameCommand ??
                (_continueGameCommand = new DelegateCommand(ContinueGameClick, CanContinueGameClick));

            }
        }

        private DelegateCommand _soundCommand;
        public DelegateCommand SoundCommand
        {
            get
            {
                return _soundCommand ??
                (_soundCommand = new DelegateCommand(SoundClick, CanSoundClick));

            }
        }


        private DelegateCommand _helpAboutCommand;
        public DelegateCommand HelpAboutCommand
        {
            get
            {
                return _helpAboutCommand ??
                (_helpAboutCommand = new DelegateCommand(HelpAboutClick, CanHelpAboutClick));

            }
        }



        #endregion

        #region Methods
        public ShellViewModel(IShellView view)
        {
            //If database is not created, Get ALL Players service will not execute,
            //so this if condition checks whether is databse is created, 
            //if YES, it will check the count of Players list count for enable and disable the Continue Game /Report button.
            if (AppSettings.SettingsModel.IsFromDatabase)
                _canContinueGame = ScoreCardService.GetAllPlayers().Count > 0;

            this.View = view;
            this.View.SetViewModel(this);

        }
        public void LoadData(string uri)
        {

            if (AppSettings.SettingsModel.IsAudioEnabled
              && !Microsoft.Xna.Framework.Media.MediaPlayer.GameHasControl)
            {
                AppSettings.SettingsModel.IsAudioEnabled = false;
                if (MessageBox.Show("Currently media is playing some other music on the phone. Do you want to stop?",
                    "Stop Player", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    Microsoft.Xna.Framework.Media.MediaPlayer.Stop();
                    AppSettings.SettingsModel.IsAudioEnabled = true;
                }
            }


            SetSoundImage();
        }

        private bool CanNewGameClick(object param)
        {
            return true;

        }

        private void NewGameClick(object param)
        {
            string.Format("{0}?1", XAML.GameChooserView).Navigate();
        }

        private bool CanContinueGameClick(object param)
        {
            return _canContinueGame;
        }

        private void ContinueGameClick(object param)
        {
            XAML.ReportView.Navigate();
        }

        private bool CanSoundClick(object param)
        {
            return true;
        }

        private void SoundClick(object param)
        {
            //Toggle the audion settings
            AppSettings.SettingsModel.IsAudioEnabled = !AppSettings.SettingsModel.IsAudioEnabled;
            (new SettingsService()).SetSettings(AppSettings.SettingsModel);

            SetSoundImage();
        }

        public void SetSoundImage()
        {
            if (AppSettings.SettingsModel.IsAudioEnabled)
                SoundImage = "Sound";
            else
                SoundImage = "Sound1";
        }



        private bool CanHelpAboutClick(object param)
        {
            return true;
        }

        private void HelpAboutClick(object param)
        {
            string navigate = XAML.HelpView;

            switch (param.ToString().ToInt())
            {
                case 1:
                    navigate = XAML.HelpView;
                    break;
                case 2:
                    navigate = XAML.AboutView;
                    break;
                case 3:
                    navigate = XAML.SettingsView;
                    break;
                default:
                    break;
            }
            navigate.Navigate();
        }

        #endregion

    }
}
