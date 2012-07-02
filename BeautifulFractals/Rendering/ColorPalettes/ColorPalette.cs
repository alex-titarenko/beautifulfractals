using System;
using System.Collections.Generic;
using System.Text;

namespace TAlex.BeautifulFractals.Rendering.ColorPalettes
{
    public abstract class ColorPalette : IColorPalette
    {
        #region IColorPalette Members

        public abstract Color GetColor(double value);

        #endregion
    }
}
