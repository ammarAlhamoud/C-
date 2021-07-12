using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class DrawChanceCmd : ACommand
    {
        public DrawChanceCmd(APlayer player)
            : base(player)
        {

        }
    }
}
