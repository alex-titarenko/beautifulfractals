using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.BeautifulFractals.Rendering.ColorPalettes
{
    public abstract class PredefinedColorPalette : ColorPalette
    {
        protected static readonly TransitionColorPalette Palette = new TransitionColorPalette();

        #region IColorPalette Members

        public override Color GetColor(double value)
        {
            return Palette.GetColor(value);
        }

        #endregion
    }
}
