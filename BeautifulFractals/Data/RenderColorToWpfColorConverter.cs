using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TAlex.BeautifulFractals.Helpers;

namespace TAlex.BeautifulFractals.Data
{
    public class RenderColorToWpfColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(System.Windows.Media.Color))
            {
                if (value != null)
                {
                    return ColorHelper.ToWpfColor((BeautifulFractals.Rendering.Color)value);
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
            if (targetType == typeof(BeautifulFractals.Rendering.Color))
            {
                if (value != null)
                {
                    return ColorHelper.FromWpfColor((System.Windows.Media.Color)value);
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

        #endregion
    }
}
