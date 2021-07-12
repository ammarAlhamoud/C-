using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Balls.Common.Models
{
    /// <summary>
    /// Bar Model
    /// </summary>
    public class BarModel
    { 
        /// <summary>
        /// Get or Set Bar Name
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// Set or Get for movable bar, bar component will accelerate when this value is set to true.
        /// </summary>
        public bool IsMovalble { get; set; }
        /// <summary>
        /// Get or Set Position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// By setting this value is true, bar compoent will not carry the bar over on this.
        /// </summary>
        public bool IsAllwaysFall { get; set; }
    }
}
