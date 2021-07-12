using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Infrastructure.UI.Base;
using Balls.Common.Interfaces;
using Balls.Common.Models;
using Balls.Common.Interfaces.Base;
using Balls.Common.Infrastructure.UI.Controls;
using Microsoft.Phone.Tasks;
using Balls.Common.Infrastructure;

namespace Balls.UI.ViewModel
{
    public class AboutViewModel : ViewModelBase, IAboutViewModel
    {
        #region  Properties & Varibles

        private AboutModel _model;

        public string Descriptions
        {
            get
            {
                return _model.Descriptions;
            }
        }

        public string Version
        {
            get
            {
                return _model.Version;
            }
        }

        public IBaseView View
        {
            get;
            set;
        }

        private DelegateCommand _emailCommand;
        public DelegateCommand EMailCommand
        {
            get
            {
                return _emailCommand ??
                    (_emailCommand = new DelegateCommand(EmailClick, CanEmailClick));
            }
        }

        private DelegateCommand _rateCommand;
        public DelegateCommand RateCommand
        {
            get
            {
                return _rateCommand ??
                    (_rateCommand = new DelegateCommand(RateClick, CanRateClick));
            }
        }

        private DelegateCommand _otherAppsCommand;
        public DelegateCommand OtherAppsCommand
        {
            get
            {
                return _otherAppsCommand ??
                    (_otherAppsCommand = new DelegateCommand(OtherAppsClick, CanOtherAppsClick));
            }
        }
        #endregion

        #region ctor
        public AboutViewModel(IAboutView view)
        {
            _model = new AboutModel();
            this.View = view;
            this.View.SetViewModel(this);
        }
        #endregion

        #region Methods

        public void LoadData(string uri)
        {
            String version = "1.0.0.0";
            try { version = System.Reflection.Assembly.GetExecutingAssembly().FullName.Split('=')[1].Split(',')[0]; }
            catch (Exception) { }
            _model = new AboutModel
            {
                Version = version,
                Descriptions = APPConstants.AboutDescription
            };
            RefreshPage();
        }

        private void RefreshPage()
        {
            base.OnPropertyChanged(() => Descriptions);
            base.OnPropertyChanged(() => Version);
        }

        private bool CanEmailClick(object param)
        {
            return true;
        }

        private void EmailClick(object param)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = APPConstants.ApplicationName;
            emailComposeTask.To = APPConstants.AppOwnerEmailID;
            emailComposeTask.Body = string.Format("Comments on {0} (V{2}):{1}", APPConstants.ApplicationName, Environment.NewLine, this.Version);

            emailComposeTask.Show();
        }

        private bool CanRateClick(object param)
        {
            return true;
        }

        private void RateClick(object param)
        {
            (new MarketplaceReviewTask()).Show();
        }

        private bool CanOtherAppsClick(object param)
        {
            return true;
        }

        private void OtherAppsClick(object param)
        {
            (new MarketplaceSearchTask() { ContentType = MarketplaceContentType.Applications, SearchTerms = APPConstants.AppOwner }).Show();
        }
        #endregion
    }
}
