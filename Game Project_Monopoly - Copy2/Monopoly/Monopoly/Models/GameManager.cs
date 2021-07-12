using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models
{
    class GameManager
    {
        private static  int Dice1, Dice2;
        private bool PayToAnotherPlayer;
        private int MoneyAmount;
        public int PlayersNumber;

        public  static PlayerBase MyPlayerBase { get; set; }
        public static Player_Manager MyPlayerManager { get; set; }
        public GameManager(int playersNumber)
        {
            MyPlayerManager = new Player_Manager();

        }



        private static int ThrowDices()
        {
            Dice1 = new Random().Next(1, 7);
            Dice2 = new Random().Next(1, 7);
            return Dice1 + Dice2;
        }

        private bool CheckDices(int dice1, int dice2)
        {
            if (dice1 == dice2)
                return true;
            else
                return false;
        }
        



    }
}
