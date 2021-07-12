using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class PayCharityCmd : ACommand
    {
        public PayCharityCmd(APlayer player)
            : base(player)
        {

        }
    }
}
