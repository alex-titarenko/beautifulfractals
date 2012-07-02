using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Data;


namespace TAlex.BeautifulFractals.Data
{
    public class DisplayToBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Brush))
            {
                if (value != null)
                {
                    bool display = (bool)value;

                    if (display)
                        return new SolidColorBrush(Colors.DarkBlue);
                    else
                        return new SolidColorBrush(Colors.Silver);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
