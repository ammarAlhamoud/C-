﻿namespace SnowFighter.Controller.States
{
    using Controller.Utils;
    using Microsoft.Xna.Framework.Input;
    using View;

    public delegate void OnGameQuit();

    public class MenuState: State
    {
        public static event OnGameQuit OnExitPressed;

        private int menuId;

        public MenuState(InputHandler inputHandler, UIFactory uiFactory, SoundManager soundManager)
            :base(inputHandler, uiFactory, soundManager)
        {
            this.SpritesInState.Add(this.uiFactory.MenuBackground);
            this.SpritesInState.Add(this.uiFactory.StartButton.Sprite);
            this.SpritesInState.Add(this.uiFactory.OptionsButton.Sprite);
            this.SpritesInState.Add(this.uiFactory.ExitButton.Sprite);
            this.menuId = 1;
        }

        public override void Update()
        {
            base.Update();

            if (!this.isDone)
            {
                foreach (KeyboardButtonState key in this.inputHandler.ActiveKeys)
                {
                    if (key.Button == Keys.Down && key.ButtonState == Utils.KeyState.Clicked)
                    {
                        this.menuId++;

                        if (this.menuId > 3)
                        {
                            this.menuId = 1;
                        }
                    }
                    else if (key.Button == Keys.Up && key.ButtonState == Utils.KeyState.Clicked)
                    {
                        this.menuId--;

                        if (this.menuId < 1)
                        {
                            this.menuId = 3;
                        }
                    }

                    if (key.Button == Keys.Enter && key.ButtonState == Utils.KeyState.Clicked)
                    {
                        switch (this.menuId)
                        {
                            case 1:
                                this.PlayGame();
                                break;
                            case 2:
                                this.GoFullScreen();
                                break;
                            case 3:
                                this.ExitGame();
                                break;
                        }
                    }
                }
            }

            this.ChangeButtonsState();
        }

        private void ExitGame()
        {
            this.isDone = true;
            // Stop Sound
            MenuState.OnExitPressed.Invoke();
        }

        private void PlayGame()
        {
            this.isDone = true;
            // pause sound
            this.NextState = new UpdateState(this.inputHandler, this.uiFactory, this.soundManager);
        }

        private void GoFullScreen()
        {
            Globals.Graphics.ToggleFullScreen();
        }

        private void ChangeButtonsState()
        {
            this.uiFactory.StartButton.ChangeToNormalImage();
            this.uiFactory.OptionsButton.ChangeToNormalImage();
            this.uiFactory.ExitButton.ChangeToNormalImage();

            switch (this.menuId)
            {
                case 1:
                    this.uiFactory.StartButton.ChangeToHoverImage();
                    break;
                case 2:
                    this.uiFactory.OptionsButton.ChangeToHoverImage();
                    break;
                case 3:
                    this.uiFactory.ExitButton.ChangeToHoverImage();
                    break;
            }
        }
    }
}
