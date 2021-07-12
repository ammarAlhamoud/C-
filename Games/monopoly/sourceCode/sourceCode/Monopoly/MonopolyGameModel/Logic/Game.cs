using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;
using MonopolyGameModel.Model.States;
using MonopolyGameModel.Commands;
using MonopolyGameModel.Responses;

namespace MonopolyGameModel.Logic
{
    public class Game
    {
        private IState m_State = new Model.States.GameNotInitialized();
        private Board m_Board = new Board();
        private readonly List<APlayer> r_Players = new List<APlayer>();
        private TurnController m_turnController;

        internal List<APlayer> PlayersList
        {
            get { return r_Players; }
        }
        
        internal IState State
        {
            get { return m_State; }
            set { this.m_State = value; }
        }


        public AResponse Perform(ACommand cmd)
        {
            AResponse response = null;

            if (m_State.isValidCommand(this, cmd))
            {
                try
                {
                    response = m_State.Apply(this, cmd);
                }
                catch (Exception ex)
                {
                    response = new FailureResponse(@"Unkown error. Cannot perform operation.");
                }
            }
            else
            {
                response = new FailureResponse(@"Cannot perform operation. Ilegal action.");
            }

            return response;
        }

        public bool isValidAction(ACommand cmd)
        {
            return m_State.isValidCommand(this, cmd);
        }
    }
}



