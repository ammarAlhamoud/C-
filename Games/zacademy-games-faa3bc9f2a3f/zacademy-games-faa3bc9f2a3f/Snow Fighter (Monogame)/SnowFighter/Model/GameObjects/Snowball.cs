
namespace SnowFighter.Model.GameObjects
{
    using Microsoft.Xna.Framework;

    public class Snowball : GameObject
    {

        private const int LEFT_BOUND = -40;
        private const int RIGHT_BOUND = 1240;
        private const int BOTTOM_BOUND = 900;

        private const int SPEED = 40;
        private const float FALL_VELOCITY = 0.8f;
        private const int DEFAULT_DAMAGE = 30;

        public Snowball(Vector2 position, bool isGoingRight)
            : base(position)
        {
            this.IsMelting = false;
            this.IsGoingRight = isGoingRight;
            this.Damage = DEFAULT_DAMAGE;
        }

        public bool IsGoingRight { get; set; }
        public bool IsMelting { get; set; }
        public int Damage { get; set; }

        public void Move()
        {
            float ballSpeed;

            if (this.IsGoingRight)
            {
                ballSpeed = SPEED;
            }
            else
            {
                ballSpeed = -SPEED;
            }

            if (this.Position.Y + FALL_VELOCITY < 900)
            {
                if (this.Bounds.Left + (this.Bounds.Width / 2) + ballSpeed < LEFT_BOUND)
                {
                    this.Position = new Vector2(RIGHT_BOUND - (this.Bounds.Width / 2), this.Position.Y);
                }
                else if (this.Bounds.Right - (this.Bounds.Width / 2) + ballSpeed > RIGHT_BOUND)
                {
                    this.Position = new Vector2(LEFT_BOUND - (this.Bounds.Width / 2), this.Position.Y);
                }
                else
                {
                    this.Position = new Vector2(this.Position.X + ballSpeed, this.Position.Y + FALL_VELOCITY);
                }
            }
            else
            {
                this.IsMelting = true;
            }

        }

        public override void ActOnCollision()
        {
            this.IsMelting = true;
        }
    }
}
