using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balls.Common.Models
{
    /// <summary>
    /// Hold the player information and list of scorecard details.
    /// </summary>
    public class PlayerModel
    {
        public int Recno { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// it hold the game score, level and attempt information
        /// </summary>
        public List<ScoreCardModel> ScoreCards { get; set; }
    }
}
