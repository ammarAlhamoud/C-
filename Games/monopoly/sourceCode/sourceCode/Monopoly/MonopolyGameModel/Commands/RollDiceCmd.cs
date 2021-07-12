using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class RollDiceCmd : ACommand
    {
        public RollDiceCmd(APlayer player)
            : base(player)
        {

        }
    }
}
