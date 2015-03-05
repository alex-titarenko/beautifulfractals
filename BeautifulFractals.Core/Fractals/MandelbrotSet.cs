using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using TAlex.BeautifulFractals.Rendering;
using TAlex.BeautifulFractals.Rendering.ColorPalettes;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Represents the Mandelbrot Set fractal.
    /// </summary>
    public class MandelbrotSet : AlgebraicFractal2D
    {
        #region Properties

        public override string Caption
        {
            get
            {
                if (String.IsNullOrEmpty(Name))
                    return String.Format("Mandelbrot Set (L:{0:G4} R:{1:G4} T:{2:G4} B:{3:G4})", Region.Left, Region.Right, Region.Top, Region.Bottom);
                else
                    return Name;
            }
        }

        #endregion

        #region Constructors

        public MandelbrotSet()
        {
            Region = new Rectangle(-2, -1.25, 3.25, 2.5);
            BailOut = 20;
        }

        public MandelbrotSet(Rectangle region, int maxIterations, ColorPalette palette)
            : this()
        {
            Region = region;
            MaxIterations = maxIterations;
            ColorPalette = palette;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            int n;

            double w = context.Viewport.Width;
            double h = context.Viewport.Height;

            double logBailOut = Math.Log(BailOut);
            double bailOutSq = BailOut * BailOut;

            double step = Math.Max(Region.Width / w, Region.Height / h);
            double offset_x = Region.X - (w * step - Region.Width) / 2.0;
            double offset_y = Region.Y - (h * step - Region.Height) / 2.0;

            double x2 = 0, y2 = 0;
            double C_Re, C_Im;
            double Z_Re, Z_Im;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    C_Re = offset_x + x * step;
                    C_Im = offset_y + y * step;

                    Z_Re = C_Re;
                    Z_Im = C_Im;

                    for (n = 0; n < MaxIterations; n++)
                    {
                        x2 = Z_Re * Z_Re;
                        y2 = Z_Im * Z_Im;

                        if (x2 + y2 > bailOutSq)
                            break;

                        Z_Im = Z_Re * Z_Im * 2 + C_Im;
                        Z_Re = x2 - y2 + C_Re;
                    }

                    if (n < MaxIterations)
                    {
                        double mu = n - Math.Log(Math.Log(Math.Sqrt(x2 + y2)) / logBailOut, 2);
                        Color c = ColorPalette.GetColor(mu / MaxIterations);
                        context.PutPixel(x, y, c);
                    }
                    else
                    {
                        context.PutPixel(x, y, ColorPalette.GetColor(1));
                    }
                }
            }
        }

        #endregion
    }
}
