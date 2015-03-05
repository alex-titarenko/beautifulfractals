using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using TAlex.BeautifulFractals.Rendering;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Represents iterated function system (IFS) fractal generation engine.
    /// </summary>
    public class IFS : Fractal2D
    {
        #region Fields

        private int? _colorSeed;
        private Color[] _colors = null;

        #endregion

        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("{0} (IFS)", Name);
            }
        }

        public int Iterations
        {
            get;
            set;
        }

        public Color? Color
        {
            get;
            set;
        }

        public List<IteratedFunction> System
        {
            get;
            set;
        }

        public override bool FullyFillRendering
        {
            get
            {
                return false;
            }
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
            Rectangle area = RenderOrCalcMeasure(context, Point.Empty, 1, 100000, false);

            double w = context.Viewport.Width;
            double h = context.Viewport.Height;

            double scale_x = w / area.Width;
            double scale_y = h / area.Height;

            double scale = (scale_x < scale_y) ? scale_x : scale_y;

            Point loc = new Point();
            loc.X = -area.X * scale + (w - area.Width * scale) / 2;
            loc.Y = -area.Y * scale + (h - area.Height * scale) / 2;

            RenderOrCalcMeasure(context, loc, scale, Iterations, true);
        }

        private Rectangle RenderOrCalcMeasure(IGraphics2DContext context, Point loc, double scale, int iterations, bool render = true)
        {
            double x = 0;
            double y = 0;

            double x_min = x;
            double y_min = y;
            double x_max = x;
            double y_max = y;

            double temp_x;
            double h = context.Viewport.Height;
            Random rand = new Random(0);

            for (int i = 0; i < iterations; i++)
            {
                double probability = rand.NextDouble();
                temp_x = x;

                double sum = 0.0;
                Color color = Color ?? Rendering.Color.FromArgb(0, 0, 0);

                for (int j = 0; j < System.Count; j++)
                {
                    IteratedFunction func = System[j];

                    sum += func.P;

                    if (probability <= sum)
                    {
                        x = func.A * temp_x + func.B * y + func.E;
                        y = func.C * temp_x + func.D * y + func.F;

                        color = Color ?? _colors[j];

                        if (x > x_max) x_max = x;
                        if (x < x_min) x_min = x;
                        if (y > y_max) y_max = y;
                        if (y < y_min) y_min = y;
                        break;
                    }
                }

                if (render)
                    context.PutPixel(loc.X + x * scale, h - (loc.Y + y * scale), color);
            }

            return new Rectangle(x_min, y_min, x_max - x_min, y_max - y_min);
        }

        private void UpdateColors()
        {
            if (!_colorSeed.HasValue) _colorSeed = new Random().Next();
            Random rnd = new Random(_colorSeed.Value);
            _colors = new Color[System.Count];
            
            for (int i = 0; i < System.Count; i++)
            {
                _colors[i] = Rendering.Color.Random(rnd);
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
