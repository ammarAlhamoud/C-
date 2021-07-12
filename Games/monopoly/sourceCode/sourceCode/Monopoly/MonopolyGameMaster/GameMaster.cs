using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Logic;
using MonopolyGameModel.Model;
using MonopolyGameModel.Commands;
using MonopolyGameModel.Responses;

namespace MonopolyGameMaster
{
    public static class GameMaster
    {
        /// <summary>
        /// holds the games dictionary.
        /// each game has it's own ID.
        /// </summary>
        private static readonly Dictionary<String, Game> r_Games = new Dictionary<String,Game>();
        private static readonly Random rand = new Random((int)DateTime.Now.Ticks);


        public static AResponse Perform(Request req)
        {
            AResponse response = null;
            String gameId = req.GameID;
            
            if (gameId == null) // if the game id is null, then we need to handle the request within game master
            {
                if (req.Command is CreateNewGameCmd)
                {
                    gameId = CreateNewGame();
                    response = new GameCreateResponse(gameId);
                }
                else
                {
                    response = new FailureResponse("Invalid request.");
                }
            }
            else         //  the request is game specific, so we only pass it along. returning the outcome.
            {
                response = r_Games[gameId].Perform(req.Command);
            }


            if (response == null)
            {
                throw new NotImplementedException();
            }
            return response;
        }

        private static String  CreateNewGame(){
            String gameID = GetGameId();
            r_Games.Add(gameID, new Game());
            return gameID;
        }

        /// <summary>
        /// gets a new random identifier to a game
        /// </summary>
        /// <returns>random identifier for game</returns>
        private static String GetGameId()
        {
            String gameID = DateTime.Now.ToString() + "_" + rand.Next().ToString();
            return gameID;
        }

    }
}
