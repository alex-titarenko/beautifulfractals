using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;

namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// 
    /// </summary>
    public class ApollonianGasket : Fractal2D
    {
        #region Fields

        private Random rand = new Random();

        #endregion

        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("Apollonian Gasket  (Iterations: {0})", Iterations);
            }
        }

        public int Iterations
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public ApollonianGasket()
        {
            Iterations = 100000;
        }

        public ApollonianGasket(Color color)
            : this()
        {
            Color = color;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            double x = 0.2;
            double y = 0.3;
            double r = Math.Sqrt(3);

            double w = context.Viewport.Width;
            double mx = w / 2;
            double h = context.Viewport.Height;
            double my = h / 2;
            double scale = Math.Min(w, h) / 7.46;

            for (int n = 0; n < Iterations; n++)
            {
                double d = Math.Pow(1 + r - x, 2.0) + y * y;

                double a0 = 3 * (1 + r - x) / d - (1 + r) / (2 + r);
                double b0 = 3 * y / d;
                double denom = a0 * a0 + b0 * b0;
                double f1x = a0 / denom;
                double f1y = -b0 / denom;
                
                switch (rand.Next(0, short.MaxValue) % 3)
                {
		            case 0:
                        x = a0;
                        y = b0;
		                break;

                    case 1:
                        x = -f1x / 2 - f1y * r / 2;
                        y = f1x * r / 2 - f1y / 2;
                        break;

                    case 2:
                        x = -f1x / 2 + f1y * r / 2;
                        y = -f1x * r / 2 - f1y / 2;
                        break;
		        }

                context.PutPixel(x * scale + mx, y * scale + my, Color);
            }
        }

        public override string ToString()
        {
            return String.Format("Apollonian Gasket");
        }

        #endregion
    }
}
