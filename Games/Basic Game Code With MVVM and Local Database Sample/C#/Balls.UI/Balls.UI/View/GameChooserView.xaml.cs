using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Balls.Common.Interfaces;
using Balls.UI.ViewModel;
using Balls.Common.Infrastructure;


namespace Balls.UI.View
{
    public partial class GameChooserView : PhoneApplicationPage, IGameChooserView
    {
        public GameChooserView()
        {
            InitializeComponent();
        }

        public void SetViewModel(Common.Interfaces.Base.IBaseViewModel viewModel)
        {
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.ToString().UriQueryParameterCount() == 2)
            {
                var queryString = e.Uri.ToString().UriQueryParameters();
                string.Format("/View/{0}.xaml?{1}", queryString[0], queryString[1]).Navigate();
            }
            else
            {
                (new GameChooserViewModel(this)).LoadData(e.Uri.ToString());
            }
            StoryBoardStart();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {

            StoryBoardStop();

            base.OnNavigatedFrom(e);
        }

        private void StoryBoardStart()
        {
            sbdBalancer.Begin();
            sbdCounter.Begin();
        }
        private void StoryBoardStop()
        {
            sbdBalancer.Stop();
            sbdCounter.Stop();
        }

    }
}