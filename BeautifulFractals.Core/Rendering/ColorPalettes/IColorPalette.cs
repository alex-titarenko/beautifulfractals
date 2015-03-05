using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TAlex.BeautifulFractals.Rendering.ColorPalettes
{
    public interface IColorPalette
    {
        Color GetColor(double value);
    }
}
