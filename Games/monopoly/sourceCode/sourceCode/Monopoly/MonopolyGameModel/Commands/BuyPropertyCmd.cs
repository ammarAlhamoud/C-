using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class BuyPropertyCmd : ACommand
    {
        public BuyPropertyCmd(APlayer player)
            : base(player)
        {

        }
    }
}
