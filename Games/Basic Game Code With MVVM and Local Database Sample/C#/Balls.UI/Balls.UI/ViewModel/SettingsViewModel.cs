using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Balls.Common.Interfaces;
using Balls.Common.Models;
using Balls.Common.Interfaces.Base;
using Balls.Business.Services;
using Balls.Common.Infrastructure;
using Balls.Common.Infrastructure.UI.Base;
using Balls.Common.Infrastructure.UI.Settings;
using Balls.Common.Infrastructure.UI.Controls;
using Microsoft.Phone.Tasks;

namespace Balls.UI.ViewModel
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        #region Variables & Properties
        #region Priave Variables
        private SettingsModel _model;
        #endregion



        #region Delegate Commands
        private DelegateCommand _resetCommand;

        public DelegateCommand ResetCommand
        {
            get
            {
                return _resetCommand ??
                    (_resetCommand = new DelegateCommand(ResetClick, CanResetClick));
            }
        }
        #endregion

        public Int32 Recno
        {
            get
            {
                return _model.Recno;
            }
            set
            {
                if (value != _model.Recno)
                {
                    _model.Recno = value;
                    base.OnPropertyChanged(() => Recno);
                }
            }
        }

        public Boolean IsAudioEnabled
        {
            get
            {
                return _model.IsAudioEnabled;
            }
            set
            {
                if (value != _model.IsAudioEnabled)
                {
                    _model.IsAudioEnabled = value;
                    base.OnPropertyChanged(() => IsAudioEnabled);
                }
            }
        }

        public Double Volume
        {
            get
            {
                return _model.Volume;
            }
            set
            {
                if (value != _model.Volume)
                {
                    _model.Volume = value;
                    base.OnPropertyChanged(() => Volume);
                    base.OnPropertyChanged(() => VolumePercentage);
                }
            }
        }

        public int VolumePercentage
        {
            get
            {
                return Convert.ToInt16(Volume * 100);
            }
        }

        public IBaseView View
        {
            get;
            set;
        }

        public SettingsService SettingsService
        {
            get
            {
                return new SettingsService();
            }
        }
        #endregion

        #region ctor
        public SettingsViewModel(ISettingsView view)
        {
            _model = new SettingsModel();
            this.View = view;
            this.View.SetViewModel(this);
        }

        #endregion

        #region Methods

        public void LoadData(string uri)
        {
            Extension.IsExceptionOccurred = 0;
            _model = SettingsService.GetSettings();
            AppSettings.SettingsModel = _model;
            this.View.SetViewModel(this);

            RefreshSettings();
        }
        public void SaveSettings()
        {
            AppSettings.SettingsModel = _model;
            Deployment.Current.Dispatcher.BeginInvoke(() => SetSettings(_model));
        }

        private void SetSettings(SettingsModel settingsModel)
        {
            SettingsService.SetSettings(settingsModel);

        }
        private void RefreshSettings()
        {
            base.OnPropertyChanged(() => Recno);
            base.OnPropertyChanged(() => IsAudioEnabled);
            base.OnPropertyChanged(() => Volume);
            base.OnPropertyChanged(() => VolumePercentage);
        }
        private bool CanResetClick(object param)
        {
            return true;
        }
        private void ResetClick(object param)
        {
            SettingsService.ResetApplicationData();
            LoadData("");
        }
        #endregion
    }
}
