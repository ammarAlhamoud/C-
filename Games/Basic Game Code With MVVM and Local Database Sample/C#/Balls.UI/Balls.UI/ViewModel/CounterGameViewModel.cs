using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Balls.Common.Infrastructure;
using Microsoft.Xna.Framework.Input.Touch;
using System.Windows.Threading;
using Balls.Business.Components;
using Balls.Common.Models;
using Microsoft.Xna.Framework.Media;
using Balls.Business.Services;
using Balls.Common.Interfaces;
using System.Windows;
using Balls.Common.Infrastructure.UI.Settings;

namespace Balls.UI.ViewModel
{
    public class CounterGameViewModel : ICounterGameViewModel
    {
        #region Game

        #region Variables

        private string _nativate = "";
        private ContentManager contentManager;
        private GameTimer timer;
        private SpriteBatch spriteBatch;
        public SPEED Speed { get; set; }
        private TouchCollection touchCollections;
        private DispatcherTimer _timerSec;
        private int _level = 1;
        private double _consumedSeconds;
        private double _totalConsumedSeconds;
        private double _maxRunSeconds = 90;
        private double _waitSeconds = 5;
        private double _maxBallCount = 10;
        private long _score = -1;
        private int _selectedBallCount;
        private bool _isGameOver;
        private bool _isGameStarted;
        private bool _isMusicOn = true;
        private XnaComponentSystem.GameComponentCollection _ballTwoComponents = new XnaComponentSystem.GameComponentCollection();
        private XnaComponentSystem.GameComponentCollection _textComponents = new XnaComponentSystem.GameComponentCollection();
        private GameBackground _backgroundComponent;
        private ButtonComponet _buttonComponent;
        public List<TextModel> listOfTextModels
        {
            get
            {
                return new List<TextModel>
                {
                    new TextModel{ AssetName ="Fonts/Hud", Recno = 1, Position =new Vector2(40, 5)},
                    new TextModel{ AssetName ="Fonts/Hud", Recno = 2, Position =new Vector2(410, 5)},
                    new TextModel{ AssetName ="Fonts/Hud", Recno = 3, Position =new Vector2(50, 90)},
                    new TextModel{ AssetName ="Fonts/Result", Recno = 4, Position =new Vector2(380, 210)},
                };

            }
        }
        private bool _dbUpdated = false;
        private PlayerModel _playerModel;
        private ScoreCardService ScoreCardService
        {
            get
            {
                return new ScoreCardService();
            }
        }
        private int _attempt = 0;
        #endregion

        #region Methods
        private void _buttonComponent_GameButtonClick(object sender)
        {
            _nativate = string.Format("{1}?CounterGameView/{0}", _level, XAML.GameChooserView);
        }

        private void _timerSec_Tick(object sender, EventArgs e)
        {
            _consumedSeconds++;
            _totalConsumedSeconds++;
        }

        private string BallSound(SPEED speed)
        {
            string sound = "Sounds/Ball";
            switch (speed)
            {
                case SPEED.Speed1:
                case SPEED.Speed2:
                    sound = "Sounds/LowSpeedBall";
                    break;
                case SPEED.Speed3:
                    sound = "Sounds/MediumSpeedBall";
                    break;
                case SPEED.Speed4:
                case SPEED.Speed5:
                    sound = "Sounds/HighSpeedBall";
                    break;
            }
            return sound;
        }

        private int GetScore()
        {
            int score = (int)(100 * _selectedBallCount / _maxBallCount);

            if (_consumedSeconds != _maxRunSeconds)
                score += (int)(((_maxRunSeconds - _consumedSeconds) * 100) / (_maxRunSeconds));

            return score;
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
                    listOfTextMessages.Add(string.Format("TOTAL SELECTED: {0}/{1}", _selectedBallCount, _maxBallCount));
                    listOfTextMessages.Add(string.Empty);
                    listOfTextMessages.Add("");
                    break;

                case GAMEMODE.Stop:

                    listOfTextMessages.Add("");
                    listOfTextMessages.Add("");
                    listOfTextMessages.Add("");
                    listOfTextMessages.Add((_score).ToString());
                    break;
            }


            return listOfTextMessages;
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

        private void NavigatedTo(string uri)
        {
            _isMusicOn = AppSettings.SettingsModel.IsAudioEnabled;
            "".RemoveBackEntry();
            // Set the sharing mode of the graphics device to turn on XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(true);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(SharedGraphicsDeviceManager.Current.GraphicsDevice);


            if (uri.ToString().UriQueryParameterCount() == 1)
                _level = uri.ToString().UriQueryParameters()[0].ToInt();

            if (_level > 5)
            {
                _level = 5;
                _maxRunSeconds = 1;
            }

            Speed = (SPEED)_level;

            timer.UpdateInterval = TimeSpan.FromTicks(GetSpeedTicks(Speed));

            // Start the timer
            timer.Start();

            LoadContent();
            InitalizeContent();

        }

        private long GetSpeedTicks(SPEED sp)
        {
            long val = 333333;
            val = Convert.ToInt64(val / ((long)Speed));
            return val;
        }

        #region Database Operation


        private void UpdateScore()
        {
            if (_dbUpdated)
                return;
            _dbUpdated = true;
            //if check 
            _playerModel = (PlayerModel)"".APPPageData().IsoGetData();

            var scoreCard = _playerModel.ScoreCards.Where(x => x.GameRecno == 1).FirstOrDefault();

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

        #endregion

        #endregion

        #endregion

        #region ctor
        public CounterGameViewModel(ICounterGameView view)
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
            #region Load Balls
            _ballTwoComponents.Clear();
            for (int i = 0; i < _maxBallCount; i++)
            {
                _ballTwoComponents.Add(new MovingBall(new ComponentModel
                {
                    ContentManager = contentManager,
                    SpriteBatch = spriteBatch,
                    AssetName = "Images/Ball"
                }) { Speed = this.Speed, IsMusicOn = _isMusicOn });
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
                    Recno = item.Recno
                });
            }
            #endregion

            #region Game Background Image
            _backgroundComponent = new GameBackground(new ComponentModel
            {
                ContentManager = contentManager,
                SpriteBatch = spriteBatch,
                AssetName = "Images/CounterBG",
                Position = new Vector2(0, 0)
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

        }
        public void InitalizeContent()
        {
            _ballTwoComponents.Initialize();
            _textComponents.Initialize();
            _backgroundComponent.Initialize();
            _buttonComponent.Initialize();
        }
        public void ReloadContent()
        {
            LoadContent();
        }
        public void Update(object sender, GameTimerEventArgs e)
        {
            touchCollections = TouchPanel.GetState();
            Vector2 position = new Vector2();
            if (touchCollections.Count > 0)
            {
                position = touchCollections[touchCollections.Count - 1].Position;
            }


            _selectedBallCount = 0;
            if (_isGameStarted)
                foreach (MovingBall item in _ballTwoComponents.ToList())
                {
                    if (Convert.ToInt16(item.PostionX / 50) == Convert.ToInt16(position.X / 50)
                       && Convert.ToInt16(item.PostionY / 50) == Convert.ToInt16(position.Y / 50))
                    {

                        if (((e.TotalTime.TotalMilliseconds - item.SelectedTime.TotalMilliseconds) > 200))
                        {
                            if (item.Ball == BALL.Sealed)
                            {
                                item.Ball = BALL.Normal;
                            }
                            else
                            {
                                item.Ball = BALL.Selected;
                            }

                            item.SelectedTime = e.TotalTime;
                        }
                    }
                    if (item.Ball == BALL.Selected || item.Ball == BALL.Sealed)
                        _selectedBallCount += 1;
                }


            List<string> listOfTextMessages = null;

            if (_isGameOver || (_maxRunSeconds - _totalConsumedSeconds) == 0 || (_maxBallCount - _selectedBallCount) == 0)
            {
                _isGameOver = true;
                if (_isMusicOn)
                    MediaPlayer.Stop();
                _ballTwoComponents.Clear();
                listOfTextMessages = GetTextMessages(GAMEMODE.Stop);
                _textComponents.Clear();
                if (-1 == _score)
                {
                    _score = GetScore();
                    UpdateScore();
                    _buttonComponent.ButtonText = string.Format("{0}\n\n\n {1}           {2}", Extension.ScoreCardSrting(_score), ((int)this.Speed), _attempt);
                }

                _buttonComponent.TouchPosition = position;
                _buttonComponent.Visible = true;
                _buttonComponent.Update(e);

            }

            if (!_isGameOver)
                if (_isGameStarted || (_waitSeconds - _consumedSeconds) == 0)
                {
                    listOfTextMessages = GetTextMessages(GAMEMODE.Runing);
                    if (!_isGameStarted)
                    {
                        if (_isMusicOn)
                        {
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Play(contentManager.Load<Song>(BallSound(Speed)));

                        }
                        _consumedSeconds = 0;

                    }

                    _isGameStarted = true;

                }
                else
                {


                    listOfTextMessages = GetTextMessages(GAMEMODE.Start);
                }


            foreach (var item in _textComponents.ToList())
                ((Text)item).TextMessage = listOfTextMessages[((Text)item).Recno - 1];


            _ballTwoComponents.Update(e);
            _textComponents.Update(e);


        }
        public void Draw(object sender, GameTimerEventArgs e)
        {
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.CornflowerBlue);

            _backgroundComponent.Draw(e);
            if (_isGameStarted)
                _ballTwoComponents.Draw(e);
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

            timer.Stop();

            // Set the sharing mode of the graphics device to turn off XNA rendering
            SharedGraphicsDeviceManager.Current.GraphicsDevice.SetSharingMode(false);

            if (_isMusicOn)
                MediaPlayer.Stop();
            timer.Stop();
            _timerSec.Stop();
        }
        #endregion

    }
}
