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
using Balls.Common.Models;

namespace Balls.Common.Infrastructure.UI.Settings
{

    /// <summary>
    /// This class holds the Settings values throughout the application.
    /// </summary>
    public static class AppSettings
    { 
        private static SettingsModel _settingModel;
        public static SettingsModel SettingsModel
        {
            get
            {

                _settingModel = _settingModel ?? new SettingsModel
                {
                    Recno = 1,
                    IsAudioEnabled = true,
                    Volume = 1
                };

                if (Extension.IsExceptionOccurred == 1)
                    _settingModel.IsAudioEnabled = false;

                return _settingModel;
            }
            set
            {
                _settingModel = value;
            }
        }
    }
}
