using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balls.Common.Interfaces.Base
{
    /// <summary>
    /// View Model - Base Interface
    /// </summary>
    public interface IBaseViewModel
    { 
        IBaseView View { get; set; }
        void LoadData(string uri);
    }
}
