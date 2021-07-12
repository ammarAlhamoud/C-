using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Logic;
using MonopolyGameModel.Commands;
using MonopolyGameModel.Responses;

namespace MonopolyGameModel.Model.States
{
    public interface IState
    {
        bool isValidCommand(Game gameState, ACommand command);
        AResponse Apply(Game gameState, ACommand command);
    }

}
