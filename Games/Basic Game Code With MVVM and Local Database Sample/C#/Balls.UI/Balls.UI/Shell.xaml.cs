﻿using System;
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

namespace Balls.UI
{
    public partial class Shell : PhoneApplicationPage, IShellView
    {
        public Shell()
        {
            InitializeComponent();
        }

        public void SetViewModel(Common.Interfaces.Base.IBaseViewModel viewModel)
        {
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            "".RemoveAllBackEntry();

            (new ShellViewModel(this)).LoadData("");

            //myStoryboard.Begin();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            //myStoryboard.Stop();
            base.OnNavigatedFrom(e);
        }

    }
}