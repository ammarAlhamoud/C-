using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Responses;
using MonopolyGameModel.Commands;


namespace MonopolyGameModel.Model.States
{
    class GameReadyToStart:IState
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
                    isValid = true;
                }
            }

            if (command is StartGameCmd)
            {
                StartGameCmd cmd = command as StartGameCmd;
                if (gameState.PlayersList.Count>=2)
                {
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
                if (command is JoinActiveGameCmd)
                {
                    JoinActiveGameCmd cmd = command as JoinActiveGameCmd;
                    APlayer requestingPlayer = cmd.getPerformingPlayer();
                    gameState.PlayersList.Add(requestingPlayer);
                }

                if (command is StartGameCmd)
                {
                    gameState.State = new 
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
