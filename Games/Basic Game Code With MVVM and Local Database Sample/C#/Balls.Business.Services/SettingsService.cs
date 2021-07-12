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
using Balls.Data;
using Balls.Data.BGMDB.Designer;
using System.Collections.Generic;
using System.Linq;

namespace Balls.Business.Services
{
    /// <summary>
    /// Class helps to Setting CURD operations and reset the database
    /// </summary>
    public class SettingsService
    {
        #region Get
        public SettingsModel GetSettings()
        {
            SettingsModel settingsModel = null;

            if (BGMDBDataContextHelper.Instance().IsExists())
            {
                using (BGMDBDataContext context = new BGMDBDataContext(BGMDBDataContextHelper.ConnectionString))
                {

                    settingsModel = (from result in context.settings
                                     select new SettingsModel
                                     {
                                         Recno = result.Recno,
                                         IsAudioEnabled = result.IsAudioEnabled,
                                         Volume = result.Volume,
                                         IsFromDatabase = true
                                     }).FirstOrDefault();
                }
            }
            else

                settingsModel = GetDefaultSettings();


            return settingsModel;
        }

        public SettingsModel GetDefaultSettings()
        {
            return new SettingsModel
            {
                Recno = 1,
                IsAudioEnabled = true,
                Volume = 1,
                IsFromDatabase = false
            };
        }


        #endregion

        #region Set
        public bool SetSettings(SettingsModel settingsModel)
        {
            using (BGMDBDataContext context = new BGMDBDataContext(BGMDBDataContextHelper.ConnectionString))
            {
                var dbSetting = (from result in context.settings
                                 where result.Recno == settingsModel.Recno
                                 select result).FirstOrDefault();

                if (null != dbSetting)
                {
                    dbSetting.IsAudioEnabled = settingsModel.IsAudioEnabled;
                    dbSetting.Volume = settingsModel.Volume;
                }


                context.SubmitChanges();
            }

            return true;
        }
        #endregion

        #region Reset Database
        public bool ResetApplicationData()
        {

            BGMDBDataContextHelper.Instance().ResetDatabase();

            return true;
        }
        #endregion
    }
}
