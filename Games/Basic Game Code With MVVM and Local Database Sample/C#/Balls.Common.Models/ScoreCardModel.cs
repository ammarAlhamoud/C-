using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balls.Common.Models
{
    /// <summary>
    /// This class hold the game score, level and attempt information
    /// </summary>
    public class ScoreCardModel
    {
        public int Recno { get; set; }
        public int Level { get; set; }
        public long Score { get; set; }
        public int Attempt { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int PlayerRecno { get; set; }
        public int GameRecno { get; set; }
        public bool IsCompleted
        {
            get
            {
                return Level > 5;
            }
        }
        public bool IsNeedUpdateDB { get; set; }
    }
}
