using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;

namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// 
    /// </summary>
    public class CantorDust : GeometricFractal2D
    {
        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("Cantor Dust (Level: {0})", Iterations);
            }
        }

        public Color Color
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public CantorDust()
        {
        }

        public CantorDust(Color color)
        {
            Color = color;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            Rectangle viewport = context.Viewport;
            double w = context.Viewport.Width;
            double h = context.Viewport.Height;

            double side = Math.Min(w, h);

            Rectangle rect = new Rectangle();
            rect.X = viewport.X + w / 2 - side / 2;
            rect.Y = viewport.Y + h / 2 - side / 2;
            rect.Width = side;
            rect.Height = side;

            Recursion(context, rect.X, rect.Y, rect.Width, rect.Height, Iterations);
        }

        private void Recursion(IGraphics2DContext context, double x, double y, double width, double height, int level)
        {
            if (level > 0)
            {
                double dx = width / 3;
                double dy = height / 3;

                Recursion(context, x, y, dx, dy, level - 1);
                Recursion(context, x + 2 * dx, y, dx, dy, level - 1);
                Recursion(context, x, y + 2 * dy, dx, dy, level - 1);
                Recursion(context, x + 2 * dx, y + 2 * dy, dx, dy, level - 1);
            }
            else
            {
                context.FillRectangle(x, y, width, height, Color);
            }
        }

        public override string ToString()
        {
            return String.Format("Cantor Dust");
        }

        #endregion
    }
}
