using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameMaster;
using MonopolyGameModel.Model;
using MonopolyGameModel.Responses;
using MonopolyGameModel.Commands;

namespace MonopolyTester
{
    class Program
    {
        static void Main(string[] args)
        {
            AResponse response = null;
            String gameID = null;
            APlayer playerA = new HumanPlayer();
            APlayer playerB = new HumanPlayer();


            Console.WriteLine("Game initializing");
            response = GameMaster.Perform(new Request(null, new CreateNewGameCmd()));
            if (!(response is GameCreateResponse))
            {
                throw new Exception();
            }

            gameID = (response as GameCreateResponse).GameID;
            Console.WriteLine("Game {0} initialized", gameID);


            Console.WriteLine("trying to join game");
            response = GameMaster.Perform(new Request(gameID, new JoinActiveGameCmd(playerA)));
            if (response is FailureResponse)
            {
                Console.WriteLine("could not join.");
            }
            else
            {
                Console.WriteLine("player joined game.");
            }


        }
    }
}
