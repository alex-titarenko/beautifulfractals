using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TAlex.BeautifulFractals.Data
{
    public class TimeSpanToSecondsConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(double))
            {
                if (value != null)
                {
                    return ((TimeSpan)value).TotalSeconds;
                }
                return null;
            }

            throw new InvalidOperationException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(TimeSpan))
            {
                if (value != null)
                {
                    return TimeSpan.FromSeconds((double)value);
                }
                return null;
            }

            throw new InvalidOperationException();
        }

        #endregion
    }
}
