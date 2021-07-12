using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework.Content;
using Balls.Common.Interfaces;
using Balls.Common.Infrastructure;
using Balls.Common.Models;
using Microsoft.Xna.Framework.Audio;

namespace Balls.Business.Components
{
    /// <summary>
    /// Balancing Ball class is producting the ball component which is used in Balancer Game
    /// </summary>
    public class BalancingBall : DrawableGameAccelerometerComponent, IDrawableGameComponent
    {
        #region Variables

        private float _rotationOffset;
        private float _rotation = 0.0f;
        private Random _random = new Random();
        private ComponentModel _componentModel;
        private XnaSoundEffect _ballFallingSoundEffect;
        private float _yPositionConstant
        {
            get
            {
                return (_componentModel.Texture2D.Width / 1.3f);
            }
        }

        public float X;
        public float Y;
        public bool Start;
        public int AccMulFactor = 10;
        public bool IsFall = false;

        private float _accX;
        public float AccX
        {
            get
            {
                if (Start && IsAccelerometerInitiated)
                {
                    float diffX = (_accX - AccelerometerValue.Acceleration.Y);
                    if (diffX < 0)
                        diffX *= -1;
                    _rotation -= (1.0f / 10f) * diffX * 300;
                    if (360.0f == _rotation)
                        _rotation = 0.0f;
                    _accX = -AccelerometerValue.Acceleration.Y * AccMulFactor;
                }

                return _accX;
            }
        }

        private bool _isMusicOn;

        public bool IsMusicOn
        {
            get { return _isMusicOn; }
            set
            {
                _isMusicOn = value;
                if (null != _ballFallingSoundEffect)
                    _ballFallingSoundEffect.IsMusicOn = value;
            }
        }
        #endregion

        #region ctor
        public BalancingBall(ComponentModel componentModel, float x, float y)
        {
            X = x;
            Y = y;
            _componentModel = componentModel;
            LoadContent();
        }
        #endregion

        #region Methods


        protected override void LoadContent()
        {
            _rotationOffset = (float)_random.NextDouble() * MathHelper.Pi;

            _componentModel.Position = new Vector2(X + (AccX), Y - (_componentModel.Texture2D.Width / 1.3f));
            LoadAccelerometer(TimeSpan.FromMilliseconds(20));

            _ballFallingSoundEffect = new XnaSoundEffect(_componentModel.ContentManager.Load<SoundEffect>("Sounds/BallUnselected")) { IsMusicOn = _isMusicOn };

            base.LoadContent();
        }

        protected override void UnloadContent()
        {

            StopAccelerometer();
            base.UnloadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            StopAccelerometer();
            base.Dispose(disposing);
        }

        public override void Update(GameTimerEventArgs e)
        {
            if (Start)
                if (!IsFall)
                    _componentModel.Position = new Vector2(X + (AccX), Y - _yPositionConstant);
                else
                {
                    Y = _componentModel.Position.Y + 1 * 1.0f + _yPositionConstant;
                    X = _componentModel.Position.X + AccX / 20f;
                    _componentModel.Position = new Vector2(X, Y - _yPositionConstant);
                }
            else
            {
                _componentModel.Position = new Vector2(X, Y - _yPositionConstant);
            }

            base.Update(e.ElapsedTime, e.TotalTime);
        }

        public override void Draw(GameTimerEventArgs e)
        {
            _rotation = (float)Math.Sin(_rotationOffset + e.TotalTime.TotalSeconds * 5f) * MathHelper.Pi / 10f;

            if (IsFall)
                _ballFallingSoundEffect.Play();

            SpriteBatchDraw();

            base.Draw(e.ElapsedTime, e.TotalTime);
        }

        public float PostionX
        {
            get
            {
                return _componentModel.Position.X;
            }
        }

        public float PostionY
        {
            get
            {
                return _componentModel.Position.Y;
            }
        }

        private void SpriteBatchDraw()
        {
            _componentModel.SpriteBatch.Begin();

            _componentModel.SpriteBatch.Draw(_componentModel.Texture2D,
                        _componentModel.Position,
                        null,
                        Microsoft.Xna.Framework.Color.White,
                        _rotation,
                        _componentModel.Origin,
                        1f, SpriteEffects.None,
                        0f);

            _componentModel.SpriteBatch.End();
        }
        #endregion
    }
}
