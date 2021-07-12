using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class StartGameCmd : ACommand
    {
        public StartGameCmd(APlayer player)
            : base(player)
        {

        }
    }
}
