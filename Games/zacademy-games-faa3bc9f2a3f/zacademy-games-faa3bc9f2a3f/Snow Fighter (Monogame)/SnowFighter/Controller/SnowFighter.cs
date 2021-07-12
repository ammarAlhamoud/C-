namespace SnowFighter.Controller
{
    using global::SnowFighter.Controller.States;
    using global::SnowFighter.Controller.Utils;
    using global::SnowFighter.View;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SnowFighter : Game
    { 
        private MonoGameRenderer renderer;

        private StateMachine stateMachine;

        private UIFactory uiFactory;
        private SoundManager soundManager;
        private InputHandler inputHandler;

        public SnowFighter()
        {
            Globals.Graphics = new GraphicsDeviceManager(this);
            Globals.Content = this.Content;
            Globals.Content.RootDirectory = "Content";

            this.IsMouseVisible = false;

            Window.Title = "Snow Fighter";

            Globals.Graphics.PreferredBackBufferWidth = 1280;
            Globals.Graphics.PreferredBackBufferHeight = 1024;
            
            MenuState.OnExitPressed += this.QuitGame;
        }

        protected override void Initialize()
        {
            this.uiFactory = new UIFactory();
            this.renderer = new MonoGameRenderer();
            this.soundManager = new SoundManager();
            this.inputHandler = new InputHandler();

            this.stateMachine = new StateMachine(this.inputHandler, this.uiFactory, this.soundManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            //this.soundManager.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.GameTime = gameTime;
            this.stateMachine.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Globals.Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.stateMachine.Draw(this.renderer);
        }

        private void QuitGame()
        {
            this.Exit();
        }
    }
}
