using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Commands;

namespace MonopolyGameMaster
{
    public class Request
    {
        private readonly String r_GameId;
        private readonly ACommand r_Command;

        public String GameID { get { return r_GameId; } }
        public ACommand Command { get { return r_Command; } }

        public Request(String GameID, ACommand Command)
        {
            this.r_Command = Command;
            this.r_GameId = GameID;
        }

    }
}
