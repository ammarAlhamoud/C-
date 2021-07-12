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

namespace Balls.UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        // Simple button Click event handler to take us to the second page BallCounterGame.xaml
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/GamePage.xaml?{0}", ((Button)sender).CommandParameter.ToString()), UriKind.Relative));
        }

        private void GameTwo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/BallCounterGame.xaml?{0}", ((Button)sender).CommandParameter.ToString()), UriKind.Relative));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).CommandParameter.ToString() == "1") NavigationService.Navigate(new Uri("/View/SettingsView.xaml", UriKind.Relative));
            else if (((Button)sender).CommandParameter.ToString() == "2") NavigationService.Navigate(new Uri("/View/AboutView.xaml", UriKind.Relative));
            else if (((Button)sender).CommandParameter.ToString() == "3") NavigationService.Navigate(new Uri("/Shell.xaml", UriKind.Relative));
            else if (((Button)sender).CommandParameter.ToString() == "4") NavigationService.Navigate(new Uri("/View/ReportView.xaml", UriKind.Relative));
        }
    }
}