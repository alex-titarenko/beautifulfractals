using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;
using TAlex.MathCore;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Dynamical Fractals, Nonlinear transformation, Attractors
    /// </summary>
    public class NewtonBasins : AlgebraicFractal2D
    {
        #region Fields

        #endregion

        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("Newton Basins");
            }
        }

        public CPolynomial Polynomial
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public NewtonBasins()
            : this(new Rectangle(-2, -1.5, 4, 3))
        {
        }

        public NewtonBasins(Rectangle region)
        {
            Region = region;
            MaxIterations = 32;
            BailOut = 1E15;

            Polynomial = new CPolynomial(new double[] { -1, 0, 0, 0, 0, 1 });
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

            CPolynomial p = Polynomial;
            CPolynomial pd = Polynomial.FirstDerivative();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    Complex Z = new Complex(offset_x + x * step, offset_y + y * step);
                    Complex Delta = Z;
                    Complex Z1 = Z;

                    for (n = 0; (n < MaxIterations) && (Complex.Abs(Z) < BailOut) && Complex.Abs(Delta) > 1E-15; n++)
                    {
                        Z = Z - p.Evaluate(Z) / pd.Evaluate(Z);
                        Delta = Z1 - Z;
                        Z1 = Z;
                    }

                    context.PutPixel(x, y, GetColor(Z, n, MaxIterations));
                }
            }
        }

        private Color GetColor(Complex z, int k, int iterations)
        {
            double mag = ((double)(iterations - k)) / (double)iterations;
            double angle = z.IsZero ? 0 : Complex.Arg(z);

            int g = (int)(255 * mag * (Math.Sin(angle) / 2 + 0.5));
            int b = (int)(255 * mag * (Math.Sin(angle + 2 * Math.PI / 3) / 2 + 0.5));
            int r = (int)(255 * mag * (Math.Sin(angle + 4 * Math.PI / 3) / 2 + 0.5));

            return Color.FromArgb(r, g, b);
        }

        public override string ToString()
        {
            return String.Format("Newton Basins (P: {0})", Polynomial);
        }

        #endregion
    }
}
