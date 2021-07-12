using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public abstract class ACommand
    {
        private readonly APlayer r_Player;

        public ACommand(APlayer PerformingPlayer)
        {
            this.r_Player = PerformingPlayer;
        }

        public APlayer getPerformingPlayer()
        {
            return this.r_Player;
        }
    }
}
