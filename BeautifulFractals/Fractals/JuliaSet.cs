using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using TAlex.BeautifulFractals.Rendering;
using TAlex.BeautifulFractals.Rendering.ColorPalettes;
using TAlex.MathCore;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Dynamical Fractals, Nonlinear transformation, Attractors
    /// </summary>
    public class JuliaSet : AlgebraicFractal2D
    {
        #region Fields

        private Complex _c;

        #endregion

        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("Julia Set (C= {0} + {1}i)", _c.Re, _c.Im);
            }
        }

        public Complex C
        {
            get
            {
                return _c;
            }

            set
            {
                _c = value;
            }
        }

        #endregion

        #region Constructors

        public JuliaSet()
        {
            MaxIterations = 250;
            BailOut = 2;
        }

        public JuliaSet(Complex C, ColorPalette palette)
            : this(new Rectangle(-1.5, -1.5, 3, 3), C, palette)
        {
        }

        public JuliaSet(Rectangle area, Complex C, ColorPalette palette)
        {
            MaxIterations = 250;
            BailOut = 2;
            Region = area;
            this._c = C;
            ColorPalette = palette;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            int n;

            double w = context.Viewport.Width;
            double h = context.Viewport.Height;

            double step = Math.Max(Region.Width / w, Region.Height / h);
            double offset_x = Region.X - (w * step - Region.Width) / 2.0;
            double offset_y = Region.Y - (h * step - Region.Height) / 2.0;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Complex Z = new Complex(offset_x + x * step, offset_y + y * step);
                    double smooth_iter = Math.Exp(-Complex.Abs(Z));

                    for (n = 0; (n < MaxIterations) && (Complex.Abs(Z) < BailOut); n++)
                    {
                        Z = Z * Z + _c;
                        smooth_iter += Math.Exp(-Complex.Abs(Z));
                    }

                    if (n < MaxIterations)
                    {
                        Color c = ColorPalette.GetColor(smooth_iter / MaxIterations);
                        context.PutPixel(x, y, c);
                    }
                    else
                    {
                        context.PutPixel(x, y, ColorPalette.GetColor(1));
                    }
                }
            }
        }

        public override string ToString()
        {
            return String.Format("Julia Set (C: {0})", C);
        }

        #endregion
    }
}
