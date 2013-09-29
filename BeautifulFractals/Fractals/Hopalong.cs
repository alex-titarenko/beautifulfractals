using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Represents the Hopalong fractal.
    /// </summary>
    public class Hopalong : Fractal2D
    {
        #region Properties

        public override string Caption
        {
            get
            {
                if (String.IsNullOrEmpty(Name))
                    return String.Format("Hopalong (A:{0} B:{1} C:{2})", A, B, C);
                else
                    return Name;
            }
        }

        public override bool FullyFillRendering
        {
            get
            {
                return false;
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

        public override void Render(IGraphics2DContext context)
        {
            Rectangle rect = RenderOrCalcMeasure(context, 1, false);
            double w = context.Viewport.Width;
            double h = context.Viewport.Height;

            double scale_x = w / rect.Width;
            double scale_y = h / rect.Height;

            double scale = (scale_x < scale_y) ? scale_x : scale_y;

            RenderOrCalcMeasure(context, scale, true);
        }

        private Rectangle RenderOrCalcMeasure(IGraphics2DContext context, double scale, bool render = true)
        {
            double x = 0.0;
            double y = 0.0;
            double max_x = 0;
            double max_y = 0;

            double mx = context.Viewport.Width / 2.0;
            double my = context.Viewport.Height / 2.0;

            for (int n = 0; n < Iterations; n++)
            {
                int signx = (x >= 0) ? 1 : -1;

                double t = x;
                x = y - signx * Math.Sqrt(Math.Abs(B * x - C));
                y = A - t;

                max_x = Math.Max(max_x, x);
                max_y = Math.Max(max_y, y);
                if (render)
                {
                    context.PutPixel(mx + x * scale, my - y * scale, Color);
                }
            }

            return new Rectangle(-max_x, -max_y, 2 * max_x, 2 * max_y);
        }

        #endregion
    }
}
