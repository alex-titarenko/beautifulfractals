using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;

namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Represents the Sierpinski Carpet fractal.
    /// </summary>
    public class SierpinskiCarpet : GeometricFractal2D
    {
        #region Fields

        private Color? _color;

        #endregion

        #region Properties

        public override string Caption
        {
            get
            {
                return String.IsNullOrEmpty(Name) ? "Sierpinski Carpet" : Name;
            }
        }

        #endregion

        #region Constructors

        public SierpinskiCarpet()
        {
        }

        public SierpinskiCarpet(Color? color)
            : this()
        {
            _color = color;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            Rectangle viewport = context.Viewport;
            double minSide = Math.Min(viewport.Width, viewport.Height);

            double x1 = viewport.X + viewport.Width / 2 - minSide / 2;
            double x2 = x1 + minSide;
            double y1 = viewport.Y + viewport.Height / 2 - minSide / 2;
            double y2 = y1 + minSide;

            context.DrawRectangle(x1, y1, x2 - x1, y2 - y1, Color.FromArgb(0, 0, 0));
            Recursion(context, x1, y1, x2, y2, Iterations);
        }

        private void Recursion(IGraphics2DContext context, double x1, double y1, double x2, double y2, int level)
        {
            if (level > 0)
            {
                double x1n = 2 * x1 / 3 + x2 / 3;
                double x2n = x1 / 3 + 2 * x2 / 3;
                double y1n = 2 * y1 / 3 + y2 / 3;
                double y2n = y1 / 3 + 2 * y2 / 3;

                Color color = _color ?? Color.Random();

                context.FillRectangle(x1n, y1n, x2n - x1n, y2n - y1n, color);

                Recursion(context, x1, y1, x1n, y1n, level - 1);
                Recursion(context, x1n, y1, x2n, y1n, level - 1);
                Recursion(context, x2n, y1, x2, y1n, level - 1);
                Recursion(context, x1, y1n, x1n, y2n, level - 1);
                Recursion(context, x2n, y1n, x2, y2n, level - 1);
                Recursion(context, x1, y2n, x1n, y2, level - 1);
                Recursion(context, x1n, y2n, x2n, y2, level - 1);
                Recursion(context, x2n, y2n, x2, y2, level - 1);
            }
        }

        #endregion
    }
}
