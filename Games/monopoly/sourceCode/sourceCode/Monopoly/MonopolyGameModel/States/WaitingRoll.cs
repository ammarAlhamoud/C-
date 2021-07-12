using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model.States;

namespace MonopolyGameModel.States
{
    class WaitingRoll:IState
    {

        public bool isValidCommand(Logic.Game gameState, Commands.ACommand command)
        {
            throw new NotImplementedException();
        }

        public Responses.AResponse Apply(Logic.Game gameState, Commands.ACommand command)
        {
            throw new NotImplementedException();
        }
    }
}
