using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace Monopoly.Models
{


    public class PlayerBase
    {


        public string Name
        {
            get;
            set;
        }
        public int ID
        {
            get;
            set;
        }
        public int Credit
        {
            get;
            set;
        }

        public Uri FigurSource
        {
            get;
            set;
        }
        public int CurrentPosition_Colmun
        {
            get;
            set;
        }
        public int CurrentPosition_Row
        {
            get;
            set;
        }

        public List<int> OwnedProperty;

        


        



        public void MovePlayer(int nextPosition ,PlayerBase player )
        {
            int difference;
            nextPosition = nextPosition * 2;
            if (player.CurrentPosition_Row > 21)
            {
                difference = player.CurrentPosition_Colmun - nextPosition;
                if (difference >= 0)
                    player.CurrentPosition_Colmun = difference;
                else
                {
                    switch (player.CurrentPosition_Colmun % 2)
                    {
                        case 0:
                            player.CurrentPosition_Colmun = 0;

                            break;
                        case 1:
                            player.CurrentPosition_Colmun = 1;
                            break;
                    }
                    player.CurrentPosition_Row -= (nextPosition - player.CurrentPosition_Colmun - 1);
                }
            }
            else
            {
                difference = nextPosition + player.CurrentPosition_Colmun;
                if (difference <= 23)
                {
                    player.CurrentPosition_Colmun = difference;
                }
                else
                {
                    switch (player.CurrentPosition_Row % 2)
                    {
                        case 0:
                            player.CurrentPosition_Colmun = 23;

                            break;
                        case 1:
                            player.CurrentPosition_Colmun = 22;
                            break;
                    }
                    player.CurrentPosition_Row += (difference - player.CurrentPosition_Colmun );
                }

            }
        }





    }
}
