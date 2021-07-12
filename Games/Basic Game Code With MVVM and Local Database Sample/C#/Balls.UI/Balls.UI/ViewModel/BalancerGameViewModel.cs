using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Balls.Business.Components;
using Balls.Common.Models;
using Microsoft.Xna.Framework.Input.Touch;
using System.Windows.Threading;
using Balls.Common.Infrastructure;
using Microsoft.Xna.Framework.Media;
using Balls.Business.Services;
using Balls.Common.Interfaces;
using System.Windows;
using Balls.Common.Infrastructure.UI.Settings;
using Balls.Common.Infrastructure.UI.Base;

namespace Balls.UI.ViewModel
{
    public class BalancerGameViewModel : IBalancerGameViewModel
    {

        #region Game

        #region Variables
        private string _nativate = "";
        private double _consumedSeconds;
        private double _totalConsumedSeconds;
        private int _ballCount = 1;
        private double _maxRunSeconds = 90;
        private double _waitSeconds = 5;
        private double _maxBallCount = 5;
        private int _level = 1;
        private bool _isMusicOn;
        private int _attempt = 0;
        private bool _isGameOver;
        public bool _dbUpdated = false;
        private long _score = 0;

        private TouchCollection _touchCollections;
        private GameBackground _backgroundComponent;
        private StartClock _startClockComponent;
        private ButtonComponet _buttonComponent;
        private BalancingBall _ball;
        private ComponentModel _ballModel;
        private DispatcherTimer _timerSec;
        private ContentManager contentManager;
        private GameTimer timer;
        private SpriteBatch spriteBatch;
        private SPEED Speed { get; set; }
        private PlayerModel _playerModel;
        private List<Vector2> listOfVectors
        {
            get
            {
                return new List<Vector2>(){
                    new Vector2(550.0f, 150.0f),
                    new Vector2(450.0f, 250.0f),
                    new Vector2(250.0f, 350.0f),
                };
            }
        }
        private List<BarModel> listOfBarModels
        {
            get
            {
                return new List<BarModel>
                {
                    new BarModel{ AssetName = "Images/CloudBar", IsMovalble = false, Position =new Vector2(750f, 65f) },
                    new BarModel{ AssetName = "Images/CollectingBar", IsMovalble = false, Position =new Vector2(50f, 465f), IsAllwaysFall = true },
                    new BarModel{ AssetName = "Images/bar", IsMovalble = true, Position =new Vector2(550.0f, 150.0f) },
                    new BarModel{ AssetName = "Images/bar", IsMovalble = true, Position =new  Vector2(450.0f, 250.0f) },
                    new BarModel{ AssetName = "Images/bar", IsMovalble = true, Position =new  Vector2(250.0f, 350.0f)}
                };
            }
        }
        private List<BarModel> listOfBarModels1
        {
            get
            {
                return new List<BarModel>
                {
                    new BarModel{ AssetName = "startBar", IsMovalble = false, Position =new Vector2(750f, 65f) },
                    new BarModel{ AssetName = "startBar", IsMovalble = false, Position =new Vector2(50f, 465f), IsAllwaysFall = true },
                    new BarModel{ AssetName = "SmallBar", IsMovalble = true, Position =new Vector2(((float)(600-(150*1.5*0))), 150.0f) },
                    new BarModel{ AssetName = "SmallBar", IsMovalble = true, Position =new  Vector2(((float)(600-(150*1.5*1))), 250.0f) },
                    new BarModel{ AssetName = "SmallBar", IsMovalble = true, Position =new  Vector2(((float)(600-(150*1.5*2))), 350.0f)}
                };
            }
        }
        private List<TextModel> listOfTextModels
        {
            get
            {
                return new List<TextModel>
                {
                    new TextModel{ AssetName ="Fonts/Hud", Recno = 1, Position =new Vector2(50, 10), Color = Color.White},
                    new TextModel{ AssetName ="Fonts/Hud", Recno = 2, Position =new Vector2(50, 40), Color = Color.White},
                    new TextModel{ AssetName ="Fonts/Hud", Recno = 3, Position =new Vector2(50, 70), Color = Color.White},
                    new TextModel{ AssetName ="Fonts/Result", Recno = 4, Position =new Vector2(380, 210), Color = Color.Black},
                };

            }
        }
        private XnaComponentSystem.GameComponentCollection _barComponents = new XnaComponentSystem.GameComponentCollection();
        private XnaComponentSystem.GameComponentCollection _textComponents = new XnaComponentSystem.GameComponentCollection();
        private ScoreCardService ScoreCardService
        {
            get
            {
                return new ScoreCardService();
            }
        }
        #endregion

        #region Methods
        private void UpdateScore()
        {
            if (_dbUpdated)
                return;
            _dbUpdated = true;

            _playerModel = (PlayerModel)"".APPPageData().IsoGetData();

            var scoreCard = _playerModel.ScoreCards.Where(x => x.GameRecno == 2).FirstOrDefault();

            if (null != scoreCard && !scoreCard.IsCompleted)
            {
                _attempt = scoreCard.Attempt;
                scoreCard.Attempt += 1;
                scoreCard.Score += _score;
                scoreCard.IsNeedUpdateDB = true;

                if (_score > 99)
                {
                    _level = ((int)this.Speed) + 1;

                    scoreCard.Level = _level;
                }
                ScoreCardService.AddUpdatePlayerDetail(_playerModel);
            }
            else
            {
                _score = scoreCard.Score;
                _attempt = scoreCard.Attempt;
            }
        }

        private void BallReloadConent()
        {
            if (!_isGameOver)
            {
                LoadBall();
                _consumedSeconds = 4;
                _ballCount++;
            }

        }

        private void LoadBall()
        {
            #region Load Ball
            _ball = null;

            _ballModel = new ComponentModel
            {
                ContentManager = contentManager,
                SpriteBatch = spriteBatch,
                AssetName = "Images/Moon"
            };

            _ball = new BalancingBall(_ballModel, 750f, 65f) { AccMulFactor = 70, Start = false, IsMusicOn = _isMusicOn };

            #endregion
        }

        private void GameTimerAndContentManagerInitialization()
        {
            // Get the content manager from the application
            contentManager = (Application.Current as App).Content;

            // Create a timer for this page
            timer = new GameTimer();
            timer.Update += Update;
            timer.Draw += Draw;

        }

        private long GetSpeedTicks(SPEED sp)
        {
            long val = 333333;
            val = Convert.ToInt64(val / ((long)Speed));
            return val;
        }

        private void NavigatedTo(string uri)
        {
            "".RemoveBackEntry();

            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);
            int intSpeed = 1;

            if (uri.ToString().UriQueryParameterCount() == 1)
                intSpeed = uri.ToString().UriQueryParameters()[0].ToInt();

            if (intSpeed > 5)
            {
                intSpeed = 5;
                _maxRunSeconds = 1;
            }

            Speed = (SPEED)intSpeed;
            if (Speed == SPEED.Speed1)
                _maxRunSeconds = 120;

            timer.UpdateInterval = TimeSpan.FromTicks(GetSpeedTicks(Speed));

            // Start the timer
            timer.Start();

            LoadContent();
            InitalizeContent();
        }

        private void _buttonComponent_GameButtonClick(object sender)
        {
            _nativate = string.Format("{1}?BalancerGameView/{0}", _level, XAML.GameChooserView);
        }

        private void _timerSec_Tick(object sender, EventArgs e)
        {
            _consumedSeconds++;
            _totalConsumedSeconds++;
        }

        private string BarSound(SPEED speed)
        {
            string sound = "Sounds/Ball";
            switch (speed)
            {
                case SPEED.Speed1:
                case SPEED.Speed2:
                    sound = "Sounds/LowSpeedBar";
                    break;
                case SPEED.Speed3:
                    sound = "Sounds/MediumSpeedBar";
                    break;
                case SPEED.Speed4:
                case SPEED.Speed5:
                    sound = "Sounds/HighSpeedBar";
                    break;
            }
            return sound;
        }

        private List<string> GetTextMessages(GAMEMODE gameMode)
        {
            List<string> listOfTextMessages = new List<string>();

            switch (gameMode)
            {
                case GAMEMODE.Start:
                    listOfTextMessages.Add("");
                    listOfTextMessages.Add("");
                    listOfTextMessages.Add("");
                    listOfTextMessages.Add((_waitSeconds - _consumedSeconds).ToString());
                    break;
                case GAMEMODE.Runing:
                    listOfTextMessages.Add(string.Format("REMAINING SECONDS: {0}", (_maxRunSeconds - _totalConsumedSeconds).ToString()));
                    listOfTextMessages.Add(string.Format("REMAINING BALL: {0}", (_maxBallCount - _ballCount).ToString()));
                    listOfTextMessages.Add(string.Format("BALLS REACHED: {0}", (_score).ToString()));
                    listOfTextMessages.Add("");
                    break;
                case GAMEMODE.Stop:
                    listOfTextMessages.Add("");
                    listOfTextMessages.Add("");
                    listOfTextMessages.Add("");
                    listOfTextMessages.Add(GetScore().ToString());
                    break;
            }


            return listOfTextMessages;
        }

        private long GetScore()
        {
            if (_score < 6)
            {
                int score = (int)(100 * _score / _maxBallCount);

                if (_totalConsumedSeconds < _maxRunSeconds)
                    score += (int)(((_maxRunSeconds - _totalConsumedSeconds) * 100) / (_maxRunSeconds));

                _score = score;
            }

            return _score;
        }
        #endregion

        #endregion

        #region ctor
        public BalancerGameViewModel(IBalancerGameView view)
        {
            this.View = view;
            this.View.SetViewModel(this);
        }

        #endregion

        #region Variables & Properties

        public Common.Interfaces.Base.IBaseView View
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public void LoadData(string uri)
        {
            _isMusicOn = AppSettings.SettingsModel.IsAudioEnabled;
            SetMediaPlayerVolume();
            GameTimerAndContentManagerInitialization();
            NavigatedTo(uri);
        }

        private void SetMediaPlayerVolume()
        {
            if (AppSettings.SettingsModel.IsAudioEnabled)
            {
                MediaPlayer.Volume = (float)AppSettings.SettingsModel.Volume;
                Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume = (float)AppSettings.SettingsModel.Volume;
            }
        }

        public void LoadContent()
        {

            LoadBall();

            #region Load Bars
            _barComponents.Clear();

            foreach (var item in listOfBarModels)
            {
                _barComponents.Add(new Bar(new ComponentModel
                {
                    ContentManager = contentManager,
                    SpriteBatch = spriteBatch,
                    AssetName = item.AssetName,
                    Position = item.Position
                })
                {
                    IsMovable = item.IsMovalble,
                    IsAllwaysFall = item.IsAllwaysFall,
                    Speed = this.Speed
                });

            }
            #endregion

            #region Load Text
            _textComponents.Clear();

            foreach (var item in listOfTextModels)
            {
                _textComponents.Add(new Text(new ComponentModel
                {
                    ContentManager = contentManager,
                    SpriteBatch = spriteBatch,
                    AssetName = item.AssetName,
                    Position = item.Position
                })
                {
                    Recno = item.Recno,
                    Color = item.Color
                });
            }
            #endregion

            #region Game Background Image
            _backgroundComponent = new GameBackground(new ComponentModel
            {
                ContentManager = contentManager,
                SpriteBatch = spriteBatch,
                AssetName = "Images/BalancerBG",
                Position = new Vector2(0, 0)
            });
            #endregion

            #region Start Clock
            _startClockComponent = new StartClock(new ComponentModel
            {
                ContentManager = contentManager,
                SpriteBatch = spriteBatch,
                AssetName = "Images/Clock",
                Position = new Vector2(400, 240)
            });

            #endregion

            #region Button Component

            _buttonComponent = new ButtonComponet(new ComponentModel
            {
                ContentManager = contentManager,
                SpriteBatch = spriteBatch,
                AssetName = "Images/100x100",
                Position = new Vector2(400, 240)
            }) { TextAssetName = "Fonts/ScoreCard", Visible = false };

            _buttonComponent.GameButtonClick += new ButtonComponet.GameButtonClickHandler(_buttonComponent_GameButtonClick);

            #endregion

            #region Timer
            timer.Start();
            _timerSec = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1) };
            _timerSec.Tick += new EventHandler(_timerSec_Tick);
            _timerSec.Start();
            #endregion

            #region Background Music
            if (_isMusicOn)
            {

                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(contentManager.Load<Song>(BarSound(Speed)));
                MediaPlayer.Pause();
            }

            #endregion
        }

        public void InitalizeContent()
        {
            _ball.Initialize();
            _barComponents.Initialize();
            _textComponents.Initialize();
            _backgroundComponent.Initialize();
            _startClockComponent.Initialize();
            _buttonComponent.Initialize();

        }

        public void ReloadContent()
        {
            LoadContent();
        }

        public void Update(object sender, GameTimerEventArgs e)
        {

            _touchCollections = TouchPanel.GetState();
            Vector2 position = new Vector2();
            if (_touchCollections.Count > 0)
            {
                position = _touchCollections[_touchCollections.Count - 1].Position;
            }

            #region Ball

            foreach (var item in _barComponents.ToList())
            {

                if (!_ball.Start &&
                  _consumedSeconds >= _waitSeconds)
                {
                    ((Bar)item).Start = true;
                }

                if (!((Bar)item).IsAllwaysFall && _ball.Y == ((Bar)item).BouncePosition.Y)
                    if (_ball.X > ((Bar)item).BouncePosition.X + (((Bar)item).TextureWidth / 2f)
                        || _ball.X < ((Bar)item).BouncePosition.X - (((Bar)item).TextureWidth / 2f))
                    {
                        _ball.IsFall = true;
                        _ball.AccMulFactor = 10;
                    }
                    else
                    {
                        _ball.X = _ball.PostionX;
                        _ball.Y = ((Bar)item).BouncePosition.Y;
                        _ball.IsFall = false;
                    }
            }
            #endregion

            if (!_ball.Start &&
              _consumedSeconds >= _waitSeconds)
            {
                _ball.Start = true;

                _consumedSeconds = 0;
                if (_isMusicOn)
                    MediaPlayer.Resume();
            }

            List<string> listOfTextMessages = null;

            if (_isGameOver || (_maxRunSeconds - _totalConsumedSeconds) == 0 || (_maxBallCount - _ballCount) == -1)
            {
                _isGameOver = true;
                _barComponents.Clear();
                _ball.Start = false;
                _ball.Y = 550;
                _textComponents.Clear();
                if (_isMusicOn)
                    MediaPlayer.Stop();
                listOfTextMessages = GetTextMessages(GAMEMODE.Stop);
                if (!_dbUpdated)
                {
                    _score = GetScore();
                    UpdateScore();
                    _buttonComponent.ButtonText = string.Format("{0}\n\n\n {1}           {2}", Extension.ScoreCardSrting(_score), ((int)this.Speed), _attempt);
                }

                _buttonComponent.TouchPosition = position;
                _buttonComponent.Visible = true;
                _buttonComponent.Update(e);

            }
            _startClockComponent.Visible = false;
            if (!_isGameOver)
                if (_ball.Start || _ballCount > 1)
                {
                    listOfTextMessages = GetTextMessages(GAMEMODE.Runing);

                }
                else
                {
                    _startClockComponent.Visible = true;
                    _startClockComponent.Count = (int)_consumedSeconds;
                    listOfTextMessages = GetTextMessages(GAMEMODE.Start);
                    _totalConsumedSeconds = 0;
                }

            foreach (var item in _textComponents.ToList())
                ((Text)item).TextMessage = listOfTextMessages[((Text)item).Recno - 1];


            _ball.Update(e);
            _barComponents.Update(e);
            _textComponents.Update(e);
            _startClockComponent.Update(e);

        }

        public void Draw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.CornflowerBlue);


            if (_ball.PostionY > 480)
            {
                if (_ball.PostionX > 0 && _ball.PostionX < 100)
                    _score++;
                BallReloadConent();
            }

            _backgroundComponent.Draw(e);
            _ball.Draw(e);
            _barComponents.Draw(e);
            _startClockComponent.Draw(e);
            _textComponents.Draw(e);
            _buttonComponent.Draw(e);

            if (!String.IsNullOrEmpty(_nativate))
            {
                _nativate.Navigate();
                timer.Stop();
            }
        }

        public void UnloadContent()
        {
            // Stop the timer
            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            if (null != _ball)
                _ball.Dispose();
            _ball = null;
            if (_isMusicOn)
                MediaPlayer.Stop();
            if (null != _barComponents)
                _barComponents.Clear();
        }
        #endregion

    }
}
