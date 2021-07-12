using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Interfaces;
using Balls.Common.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Balls.Common.Infrastructure;
using Microsoft.Xna.Framework.Media;

namespace Balls.Business.Components
{
    public class Bar : XnaComponentSystem.DrawableGameComponent, IDrawableGameComponent
    {
        #region Variables

        private ComponentModel _componentModel;
        private float _bounce;
        private readonly Color _color = Color.White;


        public bool IsMovable = true;
        public SPEED Speed { get; set; }
        private bool _start;
        public bool Start
        {
            get { return _start && IsMovable; }
            set
            {
                if (value)
                {
                    MediaPlayer.Resume();
                }
                _start = value;
            }
        }
        public Vector2 BouncePosition
        {
            get
            {
                return _componentModel.Position + new Vector2(_bounce, 0.0f);
            }
        }
        public float TextureWidth
        {
            get
            {
                return _componentModel.Texture2D.Width;
            }
        }
        public float TestureHeight
        {
            get
            {
                return _componentModel.Texture2D.Height;
            }
        }
        public bool IsAllwaysFall { get; set; }
        #endregion

        #region ctor

        public Bar(ComponentModel componentModel)
        {
            _componentModel = componentModel;
        }

        #endregion

        #region Methods

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTimerEventArgs e)
        {
            if (Start)
            {
                const float bounceWidth = 0.3f;
                float bounceRate = 0.8f;
                const float bounceSync = -0.35f;

                double t = e.TotalTime.TotalSeconds * bounceRate * (int)Speed
                            + BouncePosition.Y * bounceSync;
                _bounce = (float)Math.Sin(t)
                            * bounceWidth
                            * _componentModel.Texture2D.Width;
            }

            base.Update(e.ElapsedTime, e.TotalTime);
        }

        public override void Draw(GameTimerEventArgs e)
        {
            SpriteBatchDraw();
            base.Draw(e.ElapsedTime, e.TotalTime);
        }

        private void SpriteBatchDraw()
        {
            _componentModel.SpriteBatch.Begin();

            _componentModel.SpriteBatch.Draw(_componentModel.Texture2D,
                BouncePosition,
                null,
                _color,
                0.0f,
                _componentModel.Origin,
                1.0f,
                SpriteEffects.None,
                0.0f);

            _componentModel.SpriteBatch.End();
        }

        #endregion

    }
}
