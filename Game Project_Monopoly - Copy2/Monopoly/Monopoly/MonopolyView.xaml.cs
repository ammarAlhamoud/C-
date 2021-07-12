using Monopoly.Models;
using Monopoly.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monopoly
{
    /// <summary>
    /// Interaction logic for MonopolyView.xaml
    /// </summary>
    public partial class MonopolyView : Window
    {
        PlayerAndPropertyViewModel myViewModel;

        public MonopolyView()
        {

            InitializeComponent();
            myViewModel = new PlayerAndPropertyViewModel();
            this.DataContext = myViewModel;
        }

        private void PlayersNumber_Checked(object sender, RoutedEventArgs e)
        {

            if (rb_Multiplayer.IsChecked == true)
            {
                AiPlayer.IsEnabled = true;
                player1.IsEnabled = true;
                //player1.Text = "Player 1";
                player2.IsEnabled = true;
                //player2.Text = "Player 2";
                player3.IsEnabled = false;
                player4.IsEnabled = false;
                player1FigurPosition.Visibility = Visibility.Visible;
                player2FigurPosition.Visibility = Visibility.Visible;
                player3FigurPosition.Visibility = Visibility.Hidden;
                player4FigurPosition.Visibility = Visibility.Hidden;



            }
            else if (rb_ThreePlayer.IsChecked == true)
            {
                AiPlayer.IsEnabled = false;
                AiPlayer.IsChecked = false;
                player1.IsEnabled = true;
                //player1.Text = "Player 1";
                player2.IsEnabled = true;
                player2.IsReadOnly = false;
                //player2.Text = "Player 2";
                player3.IsEnabled = true;
                //player3.Text = "Player 3";
                player4.IsEnabled = false;

                player1FigurPosition.Visibility = Visibility.Visible;
                player2FigurPosition.Visibility = Visibility.Visible;
                player3FigurPosition.Visibility = Visibility.Visible;
                player4FigurPosition.Visibility = Visibility.Hidden;



            }
            else if (rb_FourPlayer.IsChecked == true)
            {
                AiPlayer.IsEnabled = false;
                AiPlayer.IsChecked = false;
                player1.IsEnabled = true;
                //player1.Text = "Player 1";
                player2.IsEnabled = true;
                player2.IsReadOnly = false;
                //player2.Text = "Player 2";
                player3.IsEnabled = true;
                //player3.Text = "Player 3";
                player4.IsEnabled = true;
                // player4.Text = "Player 4";
                player1FigurPosition.Visibility = Visibility.Visible;
                player2FigurPosition.Visibility = Visibility.Visible;
                player3FigurPosition.Visibility = Visibility.Visible;
                player4FigurPosition.Visibility = Visibility.Visible;



            }
            else
            {
                btn_play.IsEnabled = false;



            }
        }

        private void AiPlayer_Checked(object sender, RoutedEventArgs e)
        {

            player2.Text = "AI Player";
            player2.IsReadOnly = true;


        }

        private void AiPlayer_Unchecked(object sender, RoutedEventArgs e)
        {
            player2.Text = "";
            player2.IsReadOnly = false;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
           
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
            player1.IsReadOnly = true;
            player1.Background = Brushes.Wheat;
            player2.IsReadOnly = true;
            player2.Background = Brushes.Wheat;
            player3.IsReadOnly = true;
            player3.Background = Brushes.Wheat;
            player4.IsReadOnly = true;
            player4.Background = Brushes.Wheat;

            rb_Multiplayer.IsEnabled = false;
            rb_ThreePlayer.IsEnabled = false;
            rb_FourPlayer.IsEnabled = false;
        }

        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            var btn2 = sender as Button;
            btn2.Command.Execute(btn2.CommandParameter);

        }
    }
}
