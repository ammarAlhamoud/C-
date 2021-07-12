using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class BuyHouseCmd : ACommand
    {
        public BuyHouseCmd(APlayer player)
            : base(player)
        {

        }   
    }
}
