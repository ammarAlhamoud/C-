using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Infrastructure;
using Balls.Common.Interfaces;
using Balls.Common.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Threading;

namespace Balls.Business.Components
{
    public class ButtonComponet : DrawableGameAccelerometerComponent, IDrawableGameComponent
    {
        #region Variables
        private ComponentModel _componentModel;
        private Color _color = Color.White;
        private Vector2 _maxPosition;
        private Vector2 _minPosition;
        private Vector2 _cuttentTouchPosition;
        private string _text;

        public Vector2 TouchPosition { get; set; }
        public string TextAssetName { get; set; }
        public string ButtonText { get; set; }

        public delegate void GameButtonClickHandler(object sender);
        public event GameButtonClickHandler GameButtonClick;

        private DispatcherTimer _timerSec;
        private bool _enableClick = false;

        #endregion

        #region ctor
        public ButtonComponet(ComponentModel componentModel)
        {
            _componentModel = componentModel;
            base.VisibleChanged += new EventHandler<EventArgs>(ButtonComponet_VisibleChanged);

            _timerSec = new DispatcherTimer();
            _timerSec.Interval = new TimeSpan(0, 0, 2);
            _timerSec.Tick += new EventHandler(_timerSec_Tick);
        }

        #endregion

        #region Methods

        protected override void LoadContent()
        {
            base.LoadContent();

            _minPosition = _componentModel.Position - _componentModel.Origin;
            _maxPosition = _componentModel.Position + _componentModel.Origin;

        }

        public override void Update(Microsoft.Xna.Framework.GameTimerEventArgs e)
        {
            base.Update(e.ElapsedTime, e.TotalTime);
            _cuttentTouchPosition = TouchPosition;
            _text = ButtonText;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTimerEventArgs e)
        {
            if (base.Visible)
            {
                SpriteBatchDraw();

                if (_cuttentTouchPosition.IsWithinLimits(_minPosition, _maxPosition) && _enableClick)
                    GameButtonClick(this);

            }

            base.Draw(e.ElapsedTime, e.TotalTime);

        }

        private void SpriteBatchDraw()
        {
            _componentModel.SpriteBatch.Begin();

            _componentModel.SpriteBatch.Draw(_componentModel.Texture2D,
                _componentModel.Position,
                null,
                _color,
                0.0f,
                _componentModel.Origin,
                1.0f,
                SpriteEffects.None,
                0.0f);

            if (string.IsNullOrEmpty(_text))
                _text = "  ";


            var spriteFont = _componentModel.ContentManager.Load<SpriteFont>(TextAssetName);

            _componentModel.SpriteBatch.DrawString(spriteFont,
                      _text,
                      _componentModel.Position - ((spriteFont.MeasureString(_text)) / 2),
                      _color);

            _componentModel.SpriteBatch.End();
        }

        private void _timerSec_Tick(object sender, EventArgs e)
        {
            _enableClick = true;
            _timerSec.Stop();
        }

        private void ButtonComponet_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
                _timerSec.Start();
            else
                _enableClick = false;
        }

        #endregion
    }
}
