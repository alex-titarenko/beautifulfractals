using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// 
    /// </summary>
    public class Hopalong : Fractal2D
    {
        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("Hopalong (Iterations: {0})", Iterations);
            }
        }

        public int Iterations
        {
            get;
            set;
        }

        public double A
        {
            get;
            set;
        }

        public double B
        {
            get;
            set;
        }

        public double C
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

        public Hopalong()
        {
            Iterations = 100000;
        }

        public Hopalong(double a, double b, double c, Color color)
            : this()
        {
            Color = color;

            A = a;
            B = b;
            C = c;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Caption;
        }

        public override void Render(IGraphics2DContext context)
        {
            double x = 0.0;
            double y = 0.0;

            double t;

            int signx = 0;

            double mx = context.Viewport.Width / 2.0;
            double my = context.Viewport.Height / 2.0;

            for (int n = 0; n < Iterations; n++)
            {
                signx = (x >= 0) ? 1 : -1;

                t = x;
                x = y - signx * Math.Sqrt(Math.Abs(B * x - C));
                y = A - t;

                context.PutPixel(mx + x, my - y, Color);
            }
        }

        #endregion
    }
}
