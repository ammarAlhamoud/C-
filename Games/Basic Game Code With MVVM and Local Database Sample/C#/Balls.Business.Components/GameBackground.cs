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
    public class GameBackground : XnaComponentSystem.DrawableGameComponent, IDrawableGameComponent
    {
        #region Variables
        private ComponentModel _componentModel;
        private readonly Color _color = Color.White;
        #endregion

        #region ctor

        public GameBackground(ComponentModel componentModel)
        {
            _componentModel = componentModel;
            LoadContent();
        }

        #endregion

        #region Methods

        public override void Update(Microsoft.Xna.Framework.GameTimerEventArgs e)
        {
            
        }

        public override void Draw(Microsoft.Xna.Framework.GameTimerEventArgs e)
        {
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
                        0,
                       new Vector2(0, 0),
                        1f, SpriteEffects.None,
                        0f);

            _componentModel.SpriteBatch.End();
        }
        #endregion
    }
}
