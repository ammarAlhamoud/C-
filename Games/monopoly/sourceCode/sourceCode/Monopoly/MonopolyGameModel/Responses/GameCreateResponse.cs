using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyGameModel.Responses
{
    public class GameCreateResponse : AResponse
    {
        private readonly String r_GameID;

        public String GameID { get {return this.r_GameID;}}

        public GameCreateResponse(string GameID)
        {
            this.r_GameID = GameID;
        }
    }
}
