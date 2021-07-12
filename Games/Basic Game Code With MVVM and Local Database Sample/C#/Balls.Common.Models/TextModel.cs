using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Balls.Common.Models
{
    public class TextModel
    {
        public string AssetName { get; set; }
        public Vector2 Position { get; set; }
        public int Recno { get; set; }
        public Color Color { get; set; }
    }
}
