using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class JoinActiveGameCmd : ACommand
    {
        public JoinActiveGameCmd(APlayer player)
            : base(player)
        {

        }

    }
}
