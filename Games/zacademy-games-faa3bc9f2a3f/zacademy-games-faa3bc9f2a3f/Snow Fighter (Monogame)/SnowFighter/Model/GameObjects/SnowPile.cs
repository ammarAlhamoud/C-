namespace SnowFighter.Model.GameObjects
{
    using Microsoft.Xna.Framework;

    public class SnowPile : GameObject
    {
        private const int SNOWBALL_REFILL_TIME = 30;

        private int timeElapsed;

        public SnowPile(Vector2 position)
            :base(position)
        {
            this.timeElapsed = 0;
        }

        public bool IsCharged { get; set; }

        public override void ActOnCollision()
        {
            if (!this.IsCharged)
            {
                if (this.timeElapsed < SNOWBALL_REFILL_TIME)
                {
                    this.timeElapsed++;
                }
                else
                {
                    this.IsCharged = true;
                    this.timeElapsed = 0;
                }
            }
        }
    }
}
