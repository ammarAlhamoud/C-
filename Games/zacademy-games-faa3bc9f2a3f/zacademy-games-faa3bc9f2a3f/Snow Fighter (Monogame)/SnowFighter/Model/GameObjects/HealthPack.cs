
namespace SnowFighter.Model.GameObjects
{
    using System;
    using Microsoft.Xna.Framework;
    using SnowFighter.Controller;

    public class HealthPack : GameObject
    {
        private int DEFAULT_HEAL = 50;

        private int timeToSpawn;

        public HealthPack(Vector2 position)
            : base(position)
        {
            this.timeToSpawn = Globals.Rng.Next(350, 700);
            this.IsActive = false;
            this.IsDrawn = false;
            this.HealthToGive = DEFAULT_HEAL;
        }

        public bool IsActive { get; set; }
        public bool IsDrawn { get; set; }
        public int HealthToGive { get; set; }

        public override void ActOnCollision()
        {
            this.IsActive = false;
        }

        public void UpdateTimer()
        {
            this.timeToSpawn--;

            if (this.timeToSpawn <= 0)
            {
                this.IsActive = true;
                this.timeToSpawn = Globals.Rng.Next(500, 1000);
            }
        }
    }
}
