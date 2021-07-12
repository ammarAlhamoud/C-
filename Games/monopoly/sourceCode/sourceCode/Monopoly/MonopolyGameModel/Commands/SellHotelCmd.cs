using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class SellHotelCmd : ACommand
    {
        public SellHotelCmd(APlayer player)
            : base(player)
        {

        }
    }
}
