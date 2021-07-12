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

namespace Balls.UI.View
{
    public partial class ReportView : PhoneApplicationPage, IReportView
    {
        public ReportView()
        {
            InitializeComponent();
        }

        public void SetViewModel(Common.Interfaces.Base.IBaseViewModel viewModel)
        {
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            (new ReportViewModel(this)).LoadData(e.Uri.ToString());


            base.OnNavigatedTo(e);
        }
    }
}