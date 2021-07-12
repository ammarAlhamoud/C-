using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class UseGetOutOfJailCardCmd : ACommand
    {
        public UseGetOutOfJailCardCmd(APlayer player)
            : base(player)
        {

        }  
    }
}
