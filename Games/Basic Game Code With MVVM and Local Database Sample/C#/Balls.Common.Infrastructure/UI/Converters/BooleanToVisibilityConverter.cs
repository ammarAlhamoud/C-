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
using System.Windows.Data;

namespace Balls.Common.Infrastructure.UI.Converters
{
    /// <summary>
    /// Convert Boolean value into System.Visibility, Visible if true, otherwise Collapsed
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = false;
            if (null == value || !Boolean.TryParse(value.ToString(), out result))
                result = false;

            if (result)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }


        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
