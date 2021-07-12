namespace SnowFighter.Controller.States
{
    using View;
    using Controller.Utils;
    using System.Collections.Generic;

    public abstract class State
    {
        protected InputHandler inputHandler;
        protected UIFactory uiFactory;
        protected SoundManager soundManager;
        protected bool isDone;

        public State(InputHandler inputHandler, UIFactory uiFactory, SoundManager soundManager)
        {
            this.inputHandler = inputHandler;
            this.uiFactory = uiFactory;
            this.soundManager = soundManager;
            this.NextState = this;
            this.SpritesInState = new List<IRenderable>();
            this.isDone = false;
        }

        public State NextState { get; set; }

        public List<IRenderable> SpritesInState { get; set; }

        public virtual void Update()
        {
            if (!this.isDone)
            {
                this.inputHandler.Update();
            }
        }

        public virtual void Draw(MonoGameRenderer renderer)
        {
            renderer.DrawState(this.SpritesInState);
        }
    }
}
