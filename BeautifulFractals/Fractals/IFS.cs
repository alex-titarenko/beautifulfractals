using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using TAlex.BeautifulFractals.Rendering;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Represents iterated function system (IFS) engine.
    /// </summary>
    public class IFS : Fractal2D
    {
        #region Fields

        private Random _rand = new Random();

        private Color _color;

        private bool _randColor = false;
        private Color[] _colors = null;

        #endregion

        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("{0} - IFS (Iterations: {1})", Name, Iterations);
            }
        }

        public string Name
        {
            get;
            set;
        }

        public int Iterations
        {
            get;
            set;
        }

        public List<IteratedFunction> System
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public IFS()
        {
            Iterations = 500000;
        }

        public IFS(List<IteratedFunction> system, string name)
            : this()
        {
            System = system;
            Name = name;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            UpdateColors();
            Rectangle area = RenderOrCalcSize(context, Point.Empty, 1, 100000, true);

            double w = context.Viewport.Width;
            double h = context.Viewport.Height;

            double scale_x = w / area.Width;
            double scale_y = h / area.Height;

            double step = (scale_x < scale_y) ? scale_x : scale_y;

            Point loc = new Point();
            loc.X = -area.X * step + (w - area.Width * step) / 2;
            loc.Y = -area.Y * step + (h - area.Height * step) / 2;

            RenderOrCalcSize(context, loc, step, Iterations, false);
        }

        private Rectangle RenderOrCalcSize(IGraphics2DContext context, Point loc, double scale, int iterations, bool dontRender)
        {
            double x = 0;
            double y = 0;

            double x_min = x;
            double y_min = y;
            double x_max = x;
            double y_max = y;

            double temp_x;
            Color color = _color;

            double h = context.Viewport.Height;

            for (int i = 0; i < iterations; i++)
            {
                double probability = _rand.NextDouble();
                temp_x = x;

                double sum = 0.0;

                for (int j = 0; j < System.Count; j++)
                {
                    IteratedFunction func = System[j];

                    sum += func.P;

                    if (probability <= sum)
                    {
                        x = func.A * temp_x + func.B * y + func.E;
                        y = func.C * temp_x + func.D * y + func.F;

                        if (_randColor) color = _colors[j];

                        if (x > x_max) x_max = x;
                        if (x < x_min) x_min = x;
                        if (y > y_max) y_max = y;
                        if (y < y_min) y_min = y;
                        break;
                    }
                }

                if (!dontRender)
                    context.PutPixel(loc.X + x * scale, h - (loc.Y + y * scale), color);
            }

            return new Rectangle(x_min, y_min, x_max - x_min, y_max - y_min);
        }

        public override string ToString()
        {
            return String.Format("{0} (IFS)", Name);
        }

        private void UpdateColors()
        {
            _randColor = true;
            _colors = new Color[System.Count];

            for (int i = 0; i < System.Count; i++)
            {
                _colors[i] = Color.Random();
            }
        }

        #endregion

        #region Nested Types

        public class IteratedFunction
        {
            [XmlAttribute]
            public double A { get; set; }

            [XmlAttribute]
            public double B { get; set; }

            [XmlAttribute]
            public double C { get; set; }

            [XmlAttribute]
            public double D { get; set; }


            [XmlAttribute]
            public double E { get; set; }

            [XmlAttribute]
            public double F { get; set; }


            [XmlAttribute]
            public double P { get; set; }


            public IteratedFunction()
            {

            }

            public IteratedFunction(double a, double b, double c, double d,
                double e, double f, double p)
            {
                A = a;
                B = b;
                C = c;
                D = d;

                E = e;
                F = f;

                P = p;
            }
        }

        #endregion
    }
}
