namespace SnowFighter.Model.Level
{
    using SnowFighter.Model.GameObjects;
    using SnowFighter.View.UI;
    using System.Collections.Generic;

    public abstract class Level
    {
        public List<Block> Blocks { get; set;}
        // Snowballs
        public List<Snowball> ListOfSnowballs { get; set; }
        public List<SnowPile> ListOfSnowPiles { get; set; }
        public List<HealthPack> ListOfHealthPack { get; set; }
    }
}
