namespace SnowFighter.View
{
    using System.Collections.Generic;
    using SnowFighter.Controller;
    using SnowFighter.View.UI;
    using SnowFighter.View.UI.Models;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class UIFactory
    {
        public Sprite SplashScreen { get; set; }

        public Sprite PressAnyKey { get; set; }

        public Sprite MenuBackground { get; set; }

        public Sprite GameOverBackground { get; set; }

        public Sprite PausedBackground { get; set; }

        public Sprite CreditsSprite { get; set; }

        public Sprite Player1WinsSprite { get; set; }

        public Sprite Player2WinsSprite { get; set; }

        public Sprite Player1Name { get; set; }

        public Sprite Player2Name { get; set; }

        public Sprite HealthBarEmptyPlayer1 { get; set; }

        public Sprite HealthBarEmptyPlayer2 { get; set; }

        public Sprite HealthbarFullPlayer1 { get; set; }

        public Sprite HealthbarFullPlayer2 { get; set; }

        public List<Sprite> ListOfHealthbars { get; set; }

        public Sprite SnowballBarEmptyPlayer1 { get; set; }

        public Sprite SnowballBarEmptyPlayer2 { get; set; }

        public Sprite SnowballBarFullPlayer1 { get; set; }

        public Sprite SnowballBarFullPlayer2 { get; set; }

        public List<Sprite> ListOfSnowballBars { get; set; }

        public Button StartButton { get; set; }

        public Button ResumeButton { get; set; }

        public Button OptionsButton { get; set; }

        public Button CreditsButton { get; set; }

        public Button ExitButton { get; set; }

        public Button ExitToMenuButton { get; set; }

        public UIFactory()
        {
            this.SplashScreen = CreateSprite("SplashScreen");
            this.PressAnyKey = CreateSprite("PressAnyKey");
            this.PressAnyKey.Position = new Vector2(Globals.Graphics.GraphicsDevice.Viewport.Bounds.Width / 2 - PressAnyKey.Texture.Width / 2, Globals.Graphics.GraphicsDevice.Viewport.Bounds.Height * 0.75f);
            this.MenuBackground = CreateSprite("MenuBackground");
            this.GameOverBackground = CreateSprite("GameOverBackground");
            this.PausedBackground = CreateSprite("PausedBackground");
            this.CreditsSprite = CreateSprite("Credits");
            this.Player1WinsSprite = CreateSprite("Player1WINS");
            this.Player2WinsSprite = CreateSprite("Player2WINS");
            this.Player1Name = CreateSprite("Player1");
            this.Player2Name = CreateSprite("Player2");
            this.HealthBarEmptyPlayer1 = CreateSprite("HealthBarEmpty");
            this.HealthBarEmptyPlayer2 = CreateSprite("HealthBarEmpty");
            this.HealthbarFullPlayer1 = CreateSprite("HealthBarFull");
            this.HealthbarFullPlayer2 = CreateSprite("HealthBarFull");
            this.ListOfHealthbars = new List<Sprite> { HealthbarFullPlayer1, HealthbarFullPlayer2 };
            this.SnowballBarEmptyPlayer1 = CreateSprite("SnowballBarEmpty");
            this.SnowballBarEmptyPlayer2 = CreateSprite("SnowballBarEmpty");
            this.SnowballBarFullPlayer1 = CreateSprite("SnowballBarFull");
            this.SnowballBarFullPlayer2 = CreateSprite("SnowballBarFull");
            this.ListOfSnowballBars = new List<Sprite> { SnowballBarFullPlayer1, SnowballBarFullPlayer2 };
            this.StartButton = CreateButton("StartNormal", "StartHover", new Vector2((Globals.Graphics.PreferredBackBufferWidth - 300) / 2, 275));
            this.ResumeButton = CreateButton("ResumeNormal", "ResumeHover", new Vector2((Globals.Graphics.PreferredBackBufferWidth - 300) / 2, 275));
            this.OptionsButton = CreateButton("OptionsNormal", "OptionsHover", new Vector2((Globals.Graphics.PreferredBackBufferWidth - 300) / 2, 375));
            this.CreditsButton = CreateButton("CreditsNormal", "CreditsHover", new Vector2((Globals.Graphics.PreferredBackBufferWidth - 300) / 2, 475));
            this.ExitButton = CreateButton("ExitNormal", "ExitHover", new Vector2((Globals.Graphics.PreferredBackBufferWidth - 300) / 2, 775));
            this.ExitToMenuButton = CreateButton("ExitToMenuNormal", "ExitToMenuHover", new Vector2((Globals.Graphics.PreferredBackBufferWidth - 300) / 2, 775));
        }

        private Button CreateButton(string buttonNormal, string buttonHover, Vector2 position)
        {
            Texture2D startNormal = Globals.Content.Load<Texture2D>(buttonNormal);
            Texture2D startHover = Globals.Content.Load<Texture2D>(buttonHover);
            Sprite startSprite = new Sprite(startNormal, position);
            Button newButton = new Button(startSprite, startHover, startNormal);
            return newButton;
        }
        
        public static Sprite CreateSprite(string fileName)
        {
            var texture = Globals.Content.Load<Texture2D>(fileName);
            Sprite sprite = new Sprite(texture);
            return sprite;
        }
    }
}
