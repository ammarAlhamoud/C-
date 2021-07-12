using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Balls.Common.Infrastructure.UI.Converters
{
    /// <summary>
    /// Convert string name into image source, extension can be mention in the parameter section, default extension is 'png'
    /// </summary>
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string exten = "png";
            if (null == value) return null;
            if (null != parameter)
                exten = parameter.ToString().ToLower();

            return new Uri(string.Format("../Images/{0}.{1}", value.ToString(), exten), UriKind.Relative);
        }

        /// <summary>
        /// Not Implemented
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
