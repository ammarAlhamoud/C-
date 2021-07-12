namespace SnowFighter.Controller.States
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Controller.States;
    using Controller.Utils;
    using View;

    public class GameOverState : State
    {
        public GameOverState(InputHandler inputHandler, UIFactory uiFactory, SoundManager soundManager, int loserIndex)
            : base(inputHandler, uiFactory, soundManager)
        {

            this.SpritesInState.Add(this.uiFactory.GameOverBackground);
            if (loserIndex == 0)
            {
                this.SpritesInState.Add(this.uiFactory.Player2WinsSprite);
                this.uiFactory.Player2WinsSprite.Position = new Vector2(300, 600);
            }
            else
            {
                this.SpritesInState.Add(this.uiFactory.Player1WinsSprite);
                this.uiFactory.Player1WinsSprite.Position = new Vector2(300, 600);
            }
        }

        public override void Update()
        {
            base.Update();

            if (!this.isDone)
            {
                foreach (KeyboardButtonState key in this.inputHandler.ActiveKeys)
                {
                    if ((key.Button == Keys.Enter || key.Button == Keys.Escape) && key.ButtonState == Utils.KeyState.Clicked)
                    {
                        this.isDone = true;
                        this.NextState = new MenuState(this.inputHandler, this.uiFactory, this.soundManager);
                    }
                }
            }
        }
    }
}
