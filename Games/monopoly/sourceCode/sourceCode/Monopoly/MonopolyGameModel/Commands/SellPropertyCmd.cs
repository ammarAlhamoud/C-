using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class SellPropertyCmd : ACommand
    {
        public SellPropertyCmd(APlayer player)
            : base(player)
        {

        }
    }
}
