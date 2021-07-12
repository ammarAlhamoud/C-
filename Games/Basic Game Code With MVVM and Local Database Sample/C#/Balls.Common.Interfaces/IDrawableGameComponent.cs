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
using Microsoft.Xna.Framework;

namespace Balls.Common.Interfaces
{
    /// <summary>
    /// This interface is used to create Game Component like Ball, Moving bar
    /// </summary>
    public interface IDrawableGameComponent
    {
        void Update(GameTimerEventArgs e);

        void Draw(GameTimerEventArgs e);
    }
}
