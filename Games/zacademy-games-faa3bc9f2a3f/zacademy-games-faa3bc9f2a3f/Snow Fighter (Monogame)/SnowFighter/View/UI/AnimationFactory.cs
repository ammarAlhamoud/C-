namespace SnowFighter.View.UI
{
    using System.Collections.Generic;
    using SnowFighter.Controller;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SnowFighter.Model.Player;

    public static class AnimationFactory
    {
        public static Animation CreatePlayerAnimation(Color tint)
        {
            Animation currentAnimation = new Animation(new Vector2(71, 120), Globals.Content.Load<Texture2D>("PlayerSpriteSheet"), 60);
            currentAnimation.AnimationStates = new List<AnimationState>();
            currentAnimation.AnimationStates.Add(new AnimationState(PlayerStates.WALKING.ToString(), new Vector2(71, 120), 9, 0));
            currentAnimation.AnimationStates.Add(new AnimationState(PlayerStates.RUNNING.ToString(), new Vector2(71, 120), 11, 1));
            currentAnimation.AnimationStates.Add(new AnimationState(PlayerStates.IDLE.ToString(), new Vector2(71, 120), 4, 2));

            currentAnimation.Tint = tint;
            currentAnimation.ChangeAnimation("IDLE");
            return currentAnimation;
        }

        public static Animation CreateHealthPack()
        {
            Animation currentAnimation = new Animation(new Vector2(61, 80), Globals.Content.Load<Texture2D>("HealthPack"), 300);
            currentAnimation.AnimationStates = new List<AnimationState>();
            currentAnimation.AnimationStates.Add(new AnimationState("Normal", new Vector2(61, 80), 4, 0));

            currentAnimation.Tint = Color.White;
            currentAnimation.ChangeAnimation("Normal");

            return currentAnimation;
        }
    }
}
