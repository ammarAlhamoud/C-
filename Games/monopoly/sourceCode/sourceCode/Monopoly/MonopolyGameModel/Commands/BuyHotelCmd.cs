using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class BuyHotelCmd : ACommand
    {
        public BuyHotelCmd(APlayer player)
            : base(player)
        {

        }
    }
}
