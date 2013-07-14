using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using TAlex.BeautifulFractals.Helpers;


namespace TAlex.BeautifulFractals.Data
{
    public class BackgroundPreviewMultiConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Brush))
            {
                if (values.Length == 3 && values.All(x => x != null && x != DependencyProperty.UnsetValue))
                {
                    var primaryColor = (BeautifulFractals.Rendering.Color)values[0];
                    var secondaryColor = (BeautifulFractals.Rendering.Color)values[1];
                    var backGradType = (BeautifulFractals.ViewModels.BackgroundGradientType)values[2];

                    Point startPoint = new Point();
                    Point endPoint = new Point();

                    switch (backGradType)
                    {
                        case ViewModels.BackgroundGradientType.Vertical:
                            endPoint = new Point(0, 1);
                            break;

                        case ViewModels.BackgroundGradientType.Horizontal:
                            endPoint = new Point(1, 0);
                            break;

                        case ViewModels.BackgroundGradientType.ForwardDiagonal:
                            endPoint = new Point(1, 1);
                            break;

                        case ViewModels.BackgroundGradientType.BackwardDiagonal:
                            startPoint = new Point(1, 0);
                            endPoint = new Point(0, 1);
                            break;

                        default: throw new ArgumentException();

                    }

                    return new LinearGradientBrush
                    {
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop { Color = ColorHelper.ToWpfColor(primaryColor), Offset = 0 },
                            new GradientStop { Color = ColorHelper.ToWpfColor(secondaryColor), Offset = 1 }
                        },
                        StartPoint = startPoint,
                        EndPoint = endPoint
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new Exception();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
