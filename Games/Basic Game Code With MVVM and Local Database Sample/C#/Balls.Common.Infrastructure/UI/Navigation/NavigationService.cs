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
using Balls.Common.Interfaces.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Linq;


namespace Balls.Common.Infrastructure.UI.Navigation
{
    /// <summary>
    /// This class handles Navigate uri and removable of backentry
    /// </summary>
    public class NavigationService : INavigationService
    {

        #region Variables
        private PhoneApplicationFrame _mainFrame;

        public event NavigatingCancelEventHandler Navigating;

        Uri _pageUri;
        #endregion

        #region Methods
        /// <summary>
        /// Navigates to the content specified by the uniform resource identifier (URI).
        /// </summary>
        /// <param name="strUri"></param>
        public void Navigate(string strUri)
        {
            _pageUri = new Uri(strUri, UriKind.Relative);

            if (EnsureMainFrame())
            {
                _mainFrame.Navigate(_pageUri);
            }
        }

        /// <summary>
        /// Navigates to the most recent entry in the back navigation history.
        /// </summary>
        public void GoBack()
        {
            if (EnsureMainFrame()
                && _mainFrame.CanGoBack)
            {
                _mainFrame.GoBack();
            }
        }

        /// <summary>
        /// This method is used to remove the most recent entry from the back stack.
        /// </summary>
        public void RemoveBackEntry()
        {
            if (EnsureMainFrame()
                && _mainFrame.CanGoBack)
            {
                _mainFrame.RemoveBackEntry();
            }
        }

        /// <summary>
        /// This method is used to remove all entry from the back stack
        /// </summary>
        public void RemoveAllBackEntry()
        {
            while (EnsureMainFrame() && _mainFrame.BackStack.Any())
            {
                _mainFrame.RemoveBackEntry();
            }
        }

        private bool EnsureMainFrame()
        {
            if (_mainFrame != null)
            {
                return true;
            }

            _mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (_mainFrame != null)
            {
                // Could be null if the app runs inside a design tool
                _mainFrame.Navigating += (s, e) =>
                {
                    if (Navigating != null)
                    {
                        Navigating(s, e);
                    }
                };

                return true;
            }

            return false;
        }
        #endregion

    }
}
