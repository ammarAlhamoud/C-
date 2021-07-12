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
using Balls.Common.Interfaces;
using Microsoft.Devices.Sensors;

namespace Balls.Common.Infrastructure
{

    /// <summary>
    /// Customized Accelerometer class, it make simple start and stop operation in core coding.
    /// </summary>
    public class DrawableGameAccelerometerComponent : XnaComponentSystem.DrawableGameComponent
    { 
        private Accelerometer _accelerometer;

        public bool IsAccelerometerInitiated
        {
            get
            {
                return null != _accelerometer;
            }
        }

        public AccelerometerReading AccelerometerValue
        {
            get
            {
                return _accelerometer.CurrentValue;
            }
        }

        public void LoadAccelerometer(TimeSpan _timeBetweenUpdates)
        {
            if (_accelerometer == null)
            {
                // Instantiate the Accelerometer.
                _accelerometer = new Accelerometer();
                _accelerometer.TimeBetweenUpdates = _timeBetweenUpdates;
            }

            StrartAcceleromter();
        }

        public void StrartAcceleromter()
        {
            if (null != _accelerometer)
                try
                {
                    _accelerometer.Start();
                }
                catch (InvalidOperationException)
                {
                    //TODO: Accelerometer not able to start
                }
        }

        public void StopAccelerometer()
        {
            if (null != _accelerometer)
            {
                _accelerometer.Stop();
            }
        }
       
        

    }
}
