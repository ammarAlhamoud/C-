using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Interfaces;
using Balls.Common.Models;
using Microsoft.Xna.Framework;
using Balls.Common.Infrastructure;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Balls.Business.Components
{

    /// <summary>
    /// This component used in BALL counting game
    /// </summary>
    public class MovingBall : XnaComponentSystem.DrawableGameComponent, IDrawableGameComponent
    {
        #region Variables

        private ComponentModel _componentModel;
        private readonly Color _color = Color.White;
        private float _rotationOffset;
        private float _rotation = 0.0f;
        private static readonly Random random = new Random();
        private Vector2 _velocity;
        private XnaSoundEffect _ballSelectedSoundEffect;
        private XnaSoundEffect _ballUnselectedSoundEffect;

        public TimeSpan SelectedTime;
        public bool IsStopMusic = false;
        public bool IsMovable = true;
        public SPEED Speed { get; set; }
        private BALL _ball = BALL.Normal;
        public BALL Ball
        {
            get { return _ball; }
            set
            {
                if (value != _ball)
                    switch (value)
                    {
                        case BALL.Normal:
                            _ballUnselectedSoundEffect.Play();
                            break;

                        case BALL.Sealed:
                            _ballSelectedSoundEffect.Play();
                            break;

                    }
                _ball = value;
            }
        }
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
        private bool _isMusicOn;
        public bool IsMusicOn
        {
            get { return _isMusicOn; }
            set { _isMusicOn = value; }
        }

        #endregion

        #region ctor
        public MovingBall(ComponentModel componentModel)
        {
            _componentModel = componentModel;
            LoadContent();
        }
        #endregion

        #region Methods

        protected override void LoadContent()
        {
            _rotationOffset = (float)random.NextDouble() * MathHelper.Pi;

            _componentModel.Position = new Vector2(random.Next(100, 700), random.Next(100, 300));

            _velocity = new Vector2(
                random.Next(100, 200) * (random.Next() % 2 == 0 ? 1 : -1),
                random.Next(100, 200) * (random.Next() % 2 == 0 ? 1 : -1));
            _ballSelectedSoundEffect = new XnaSoundEffect(_componentModel.ContentManager.Load<SoundEffect>("Sounds/BallSelected")) { IsMusicOn = _isMusicOn };
            _ballUnselectedSoundEffect = new XnaSoundEffect(_componentModel.ContentManager.Load<SoundEffect>("Sounds/BallUnselected")) { IsMusicOn = _isMusicOn };
            base.LoadContent();
        }

        public override void Update(GameTimerEventArgs e)
        {
            string strBall = BALL.Normal == Ball ? "Images/Ball" : "Images/SelectedBall";
            _componentModel.AssetName = strBall;

            _componentModel.Position += _velocity * ((float)e.ElapsedTime.TotalSeconds * (int)Speed);

            if (_componentModel.Position.X > 700 && _velocity.X > 0)
            {
                _componentModel.Position = new Vector2(700, _componentModel.Position.Y);
                _velocity.X *= -1;
            }
            else if (_componentModel.Position.X < 100 && _velocity.X < 0)
            {
                _componentModel.Position = new Vector2(100, _componentModel.Position.Y);
                _velocity.X *= -1;
            }
            else if (_componentModel.Position.Y > 380 && _velocity.Y > 0)
            {
                _componentModel.Position = new Vector2(_componentModel.Position.X, 380);
                _velocity.Y *= -1;
            }
            else if (_componentModel.Position.Y < 100 && _velocity.Y < 0)
            {
                _componentModel.Position = new Vector2(_componentModel.Position.X, 100);
                _velocity.Y *= -1;
            }

            base.Update(e.ElapsedTime, e.TotalTime);
        }

        public override void Draw(GameTimerEventArgs e)
        {
            if (Ball == BALL.Selected)
                Ball = BALL.Sealed;

            _rotation = (float)Math.Sin(_rotationOffset + e.TotalTime.TotalSeconds * 5f) * MathHelper.Pi;

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
