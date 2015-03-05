using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using TAlex.BeautifulFractals.Rendering;
using TAlex.BeautifulFractals.Rendering.ColorPalettes;


namespace TAlex.BeautifulFractals.Fractals
{
    public abstract class AlgebraicFractal2D : Fractal2D
    {
        public int MaxIterations { get; set; }

        public double BailOut
        {
            get;
            set;
        }

        public Rectangle Region { get; set; }

        [XmlElement(typeof(TransitionColorPalette))]
        [XmlElement(typeof(MulticoloredColorPalette))]
        [XmlElement(typeof(MonochromeColorPalette))]
        [XmlElement(typeof(RainbowColorPalette))]
        [XmlElement(typeof(OrangeBlueColorPalette))]
        [XmlElement(typeof(FireColorPalette))]
        public ColorPalette ColorPalette
        {
            get;
            set;
        }

        public override bool FullyFillRendering
        {
            get
            {
                return true;
            }
        }
    }
}
