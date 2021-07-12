using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly2019.Models
{
    class Player_Manager
    {
        public List<PlayerBase> Players = new List<PlayerBase>();
        public int CurrentPlayerID;

        public void AddPlayer(PlayerBase player)
        {

            this.Players.Add(player);
        }
    }
}
