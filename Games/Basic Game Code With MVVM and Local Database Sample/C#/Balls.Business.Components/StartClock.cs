using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Interfaces;
using Balls.Common.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Balls.Business.Components
{
    public class StartClock : XnaComponentSystem.DrawableGameComponent, IDrawableGameComponent
    {
        #region Varibales
        private ComponentModel _componentModel;
        private readonly Color _color = Color.White;
        private float _rotation = 0.0f;

        public int Count;
        #endregion

        #region ctor

        public StartClock(ComponentModel componentModel)
        {
            _componentModel = componentModel;
            LoadContent();
        }

        #endregion

        #region Methods

        public override void Update(Microsoft.Xna.Framework.GameTimerEventArgs e)
        {
            _rotation = 1 * Count;
            //_rotation += 1;
            base.Update(e.ElapsedTime, e.TotalTime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTimerEventArgs e)
        {
            if (this.Visible)
                SpriteBatchDraw();

            base.Draw(e.ElapsedTime, e.TotalTime);
        }

        private void SpriteBatchDraw()
        {
            _componentModel.SpriteBatch.Begin();

            _componentModel.SpriteBatch.Draw(_componentModel.Texture2D,
                        _componentModel.Position,
                        null,
                        _color,
                        _rotation,
                        _componentModel.Origin,
                        1f, SpriteEffects.None,
                        0f);

            _componentModel.SpriteBatch.End();
        }
        #endregion
    }
}
