using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class GoToJailCmd : ACommand
    {
        public GoToJailCmd(APlayer player)
            : base(player)
        {

        }
    }
}
