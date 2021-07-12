using Monopoly2019.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Monopoly2019.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {

            ShellViewModel myShellViewModel = new ShellViewModel();
            InitializeComponent();

            this.DataContext = myShellViewModel;



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ShellViewModel();
        }

        private void PlayersNumber_Checked(object sender, RoutedEventArgs e)
        {
            if (MultiPlayer.IsChecked == true)
            {
                player2.IsEnabled = true;
                player2.Text = "Computer";
                player2.IsReadOnly = true;
                player3.IsEnabled = false;
                player3.Text = "";
                player4.IsEnabled = false;
                player4.Text = "";
            }
            else if(ThreePlayer.IsChecked == true)
            {
                player2.IsEnabled = true;
                player2.Text = "";
                player2.IsReadOnly = false;
                player3.IsEnabled = true;
                player3.Text = "";
                player4.IsEnabled = false;
                player4.Text = "";
            }
            else
            {
                player2.IsEnabled = true;
                player2.Text = "";
                player2.IsReadOnly = false;
                player3.IsEnabled = true;
                player3.IsEnabled = true;
                player3.Text = "";
                player4.IsEnabled = true;
                player4.Text = "";
            }

        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            int dice1 = new Random().Next(1, 7);
            int dice2 = new Random().Next(1, 7);
            string str_dice1 = "Images\\dice" + dice1 + ".png";
            string str_dice2 = "Images\\dice" + dice2+ ".png";
            Dice1.Source = new BitmapImage(new Uri(str_dice1, UriKind.Relative));
            Dice2.Source = new BitmapImage(new Uri(str_dice2, UriKind.Relative));


        }
    }
}
