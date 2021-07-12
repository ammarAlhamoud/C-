using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Balls.Common.Interfaces.Base
{
    /// <summary>
    /// Game View Model - Base Interface
    /// </summary>
    public interface IGameBaseViewModel : IBaseViewModel
    {
        void LoadContent();

        void InitalizeContent();

        void ReloadContent();

        void Update(object sender, GameTimerEventArgs e);

        void Draw(object sender, GameTimerEventArgs e);

        void UnloadContent();
    }
}
