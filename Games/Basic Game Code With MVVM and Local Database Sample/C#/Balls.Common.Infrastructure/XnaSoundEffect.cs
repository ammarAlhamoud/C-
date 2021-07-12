using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using System.Windows.Threading;

namespace Balls.Common.Infrastructure
{
    public class XnaSoundEffect
    {
        private SoundEffect _soundEffect;
        private bool _canRestart = true;
        private DispatcherTimer _dispatcherTimer;

        public bool IsMusicOn { get; set; }

        public XnaSoundEffect(SoundEffect soundEffect)
        {
            _soundEffect = soundEffect;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(_dispatcherTimer_Tick);
        }

        void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _dispatcherTimer.Stop();
            _canRestart = true;
        }

        public bool IsPlaying
        {
            get
            {
                return !_canRestart;
            }
        }

        public void Play()
        {
            if (!IsMusicOn)
                _soundEffect = null;

            if (null != _soundEffect && _canRestart)
            {
                _canRestart = false;
                _dispatcherTimer.Interval = _soundEffect.Duration;
                _soundEffect.Play();
                _dispatcherTimer.Start();
            }
        }
    }
}
