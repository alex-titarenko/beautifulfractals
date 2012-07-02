using System;
using System.Windows.Media;


namespace TAlex.BeautifulFractals.Helpers
{
    public static class ColorHelper
    {
        public static Rendering.Color FromWpfColor(Color color)
        {
            return Rendering.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static Color ToWpfColor(Rendering.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
