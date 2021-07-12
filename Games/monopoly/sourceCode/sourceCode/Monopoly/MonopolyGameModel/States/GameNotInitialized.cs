using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Commands;
using MonopolyGameModel.Responses;

namespace MonopolyGameModel.Model.States
{
    class GameNotInitialized : IState
    {

        public bool isValidCommand(Logic.Game gameState, Commands.ACommand command)
        {
            bool isValid = false;
            if (command is JoinActiveGameCmd)
            {
                JoinActiveGameCmd cmd = command as JoinActiveGameCmd;
                APlayer requestingPlayer = cmd.getPerformingPlayer();
                if (!gameState.PlayersList.Contains(requestingPlayer))
                { 
                    //gameState.PlayersList.Append(requestingPlayer);
                    isValid = true;

                }
            }


            return isValid;

        }

        public AResponse Apply(Logic.Game gameState, Commands.ACommand command)
        {
            AResponse response = null;
            if (isValidCommand(gameState, command))
            {
                JoinActiveGameCmd cmd = command as JoinActiveGameCmd;
                APlayer requestingPlayer = cmd.getPerformingPlayer();
                gameState.PlayersList.Add(requestingPlayer);

                if (gameState.PlayersList.Count >= 2)
                {
                    gameState.State = new GameReadyToStart();
                }

                response = new SuccessResponse();
            }
            else
            {
                response = new FailureResponse(@"Cannot perform operation. Ilegal action.");
            }

            return response;
        }
    }
}
