using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Interfaces;
using Microsoft.Xna.Framework;
using Balls.Common.Models;

namespace Balls.Business.Components
{
    public class Text : XnaComponentSystem.DrawableGameComponent, IDrawableGameComponent
    {

        #region Variables
        private ComponentModel _componentModel;
        private string _text;
        private Color _color;

        public string TextMessage;

        public int Recno { get; set; }
        public Color Color = Color.White;
        #endregion

        #region ctor

        public Text(ComponentModel componentModel)
        {
            _componentModel = componentModel;
        }
        #endregion

        #region Methods

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTimerEventArgs e)
        {
            _text = TextMessage;
            _color = Color;
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

            _componentModel.SpriteBatch.DrawString(_componentModel.SpriteFont,
                        _text ?? "",
                        _componentModel.Position,
                        _color);

            _componentModel.SpriteBatch.End();
        }

        #endregion


    }
}
