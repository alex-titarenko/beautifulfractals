using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;

namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Represents the Sierpinski Triangle fractal.
    /// </summary>
    public class SierpinskiTriangle : GeometricFractal2D
    {
        #region Fields

        private Color? _color;

        #endregion

        #region Properties

        public override string Caption
        {
            get
            {
                return String.IsNullOrEmpty(Name) ? "Sierpinski Triangle" : Name;
            }
        }

        #endregion

        #region Constructors

        public SierpinskiTriangle()
        {
        }

        public SierpinskiTriangle(Color? color)
            : this()
        {
            _color = color;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            Rectangle viewport = context.Viewport;
            double side, h;

            if (viewport.Width > viewport.Height)
            {
                h = viewport.Height;
                side = viewport.Height / (Math.Sqrt(3) / 2.0);
            }
            else
            {
                side = viewport.Width;
                h = Math.Sqrt(3) / 2.0 * side;
            }

            double mx = viewport.X + viewport.Width / 2;
            double my = viewport.Y + viewport.Height / 2;

            Point p1 = new Point(mx, my - h / 2);
            Point p2 = new Point(mx + side / 2, my + h / 2);
            Point p3 = new Point(mx - side / 2, my + h / 2);

            Recursion(context, p1, p2, p3, Iterations);
        }

        private void Recursion(IGraphics2DContext context, Point p1, Point p2, Point p3, int level)
        {
            if (level > 0)
            {
                Point p1n = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                Point p2n = new Point((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2);
                Point p3n = new Point((p3.X + p1.X) / 2, (p3.Y + p1.Y) / 2);

                Color color = _color ?? Color.Random();

                context.DrawLine(p1, p2, color);
                context.DrawLine(p2, p3, color);
                context.DrawLine(p3, p1, color);

                Recursion(context, p1, p1n, p3n, level - 1);
                Recursion(context, p2, p1n, p2n, level - 1);
                Recursion(context, p3, p2n, p3n, level - 1);
            }
        }

        #endregion
    }
}
