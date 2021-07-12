using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class PayTaxCmd : ACommand
    {
        public PayTaxCmd(APlayer player)
            : base(player)
        {

        }
    }
}
