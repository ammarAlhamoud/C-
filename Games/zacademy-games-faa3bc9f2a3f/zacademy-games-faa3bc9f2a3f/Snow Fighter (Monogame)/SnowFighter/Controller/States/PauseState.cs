namespace SnowFighter.Controller.States
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework.Input;
    using Model.Player;
    using View;
    using Controller.Utils;

    public class PausedState : State
    {
        private List<Player> players;

        public PausedState(InputHandler inputHandler, UIFactory uiFactory, SoundManager soundManager, List<Player> playersData)
            : base(inputHandler, uiFactory, soundManager)
        {
            this.players = playersData;

            this.SpritesInState.Add(this.uiFactory.PausedBackground);
            this.SpritesInState.Add(this.uiFactory.ResumeButton.Sprite);
            this.SpritesInState.Add(this.uiFactory.OptionsButton.Sprite);
            this.SpritesInState.Add(this.uiFactory.ExitToMenuButton.Sprite);

            this.MenuId = 1;
        }

        public int MenuId { get; private set; }

        public override void Update()
        {
            base.Update();

            if (!this.isDone)
            {

                //this.soundManager.Resume("MenuSound");

                foreach (KeyboardButtonState key in this.inputHandler.ActiveKeys)
                {
                    if (key.Button == Keys.Down && key.ButtonState == Utils.KeyState.Clicked)
                    {
                        this.MenuId++;
                        //this.soundManager.Play("MenuMove", 1.0f);

                        if (this.MenuId > 3)
                        {
                            this.MenuId = 1;
                        }
                    }

                    if (key.Button == Keys.Up && key.ButtonState == Utils.KeyState.Clicked)
                    {
                        this.MenuId--;
                        //this.soundManager.Play("MenuMove", 1.0f);

                        if (this.MenuId < 1)
                        {
                            this.MenuId = 3;
                        }
                    }

                    if (key.Button == Keys.Enter && key.ButtonState == Utils.KeyState.Clicked)
                    {
                        switch (this.MenuId)
                        {
                            case 1:
                                this.ResumeGame();
                                break;

                            case 2:
                                Globals.Graphics.ToggleFullScreen();
                                break;

                            case 3:
                                this.ExitToMenu();
                                break;
                        }
                    }
                }

                this.ChangeButtonsState();
            }
        }

        private void ExitToMenu()
        {
            this.isDone = true;
            this.NextState = new MenuState(this.inputHandler, this.uiFactory, this.soundManager);
        }

        private void ResumeGame()
        {
            //this.soundManager.Pause("MenuSound");
            this.isDone = true;
            this.NextState = new UpdateState(this.inputHandler, this.uiFactory, this.soundManager, this.players);
        }

        private void ChangeButtonsState()
        {
            this.uiFactory.ResumeButton.ChangeToNormalImage();
            this.uiFactory.OptionsButton.ChangeToNormalImage();
            this.uiFactory.ExitToMenuButton.ChangeToNormalImage();

            switch (this.MenuId)
            {
                case 1:
                    this.uiFactory.ResumeButton.ChangeToHoverImage();
                    break;

                case 2:
                    this.uiFactory.OptionsButton.ChangeToHoverImage();
                    break;

                case 3:
                    this.uiFactory.ExitToMenuButton.ChangeToHoverImage();
                    break;
            }
        }
    }
}
