using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class MovePawnCmd : ACommand
    {
        public MovePawnCmd(APlayer player)
            : base(player)
        {

        }
    }
}
