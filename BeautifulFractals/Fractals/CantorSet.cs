using System;
using System.Collections.Generic;
using System.Text;

using TAlex.BeautifulFractals.Rendering;

namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// 
    /// </summary>
    public class CantorSet : GeometricFractal2D
    {
        #region Fields

        private const double DefaultLineHeight = 10.0;

        #endregion

        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("Cantor Set (Level: {0})", Iterations);
            }
        }

        public double LineHeight
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

        public CantorSet()
        {
            LineHeight = DefaultLineHeight;
        }

        public CantorSet(Color color)
            : this()
        {
            Color = color;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            Rectangle viewport = context.Viewport;

            double x = viewport.X;
            double y = viewport.Y + viewport.Height / 2 - Iterations * LineHeight + LineHeight / 2;
            double width = viewport.Width;

            Recursion(context, x, y, width, Iterations);
        }

        private void Recursion(IGraphics2DContext context, double x, double y, double width, int level)
        {
            if (level > 0)
            {
                double dx = width / 3;

                context.FillRectangle(x, y, width, LineHeight, Color);

                Recursion(context, x, y + LineHeight * 2, dx, level - 1);
                Recursion(context, x + 2 * dx, y + LineHeight * 2, dx, level - 1);
            }
        }

        public override string ToString()
        {
            return String.Format("Cantor Set");
        }

        #endregion
    }
}
