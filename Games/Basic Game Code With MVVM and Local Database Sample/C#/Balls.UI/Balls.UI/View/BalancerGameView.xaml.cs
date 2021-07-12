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
using System.Windows.Navigation;

namespace Balls.UI.View
{
    public partial class BalancerGameView : PhoneApplicationPage, IBalancerGameView
    {
        public BalancerGameView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            if (e.NavigationMode == NavigationMode.Back)
            {
                XAML.Shell.Navigate();
            }
            else
            {
                (new BalancerGameViewModel(this)).LoadData(e.Uri.ToString());
                base.OnNavigatedTo(e);
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            ((BalancerGameViewModel)this.DataContext).UnloadContent();

            base.OnNavigatedFrom(e);
        }

        public void SetViewModel(Common.Interfaces.Base.IBaseViewModel viewModel)
        {
            this.DataContext = viewModel;
        }
    }
}