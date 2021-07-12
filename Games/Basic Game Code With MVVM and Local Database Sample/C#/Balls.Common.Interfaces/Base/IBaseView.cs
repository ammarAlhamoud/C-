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

namespace Balls.Common.Interfaces.Base
{
    /// <summary>
    /// View - Base Interace
    /// </summary>
    public interface IBaseView
    {
        void SetViewModel(IBaseViewModel viewModel);

    }
}
