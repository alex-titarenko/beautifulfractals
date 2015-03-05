using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using TAlex.BeautifulFractals.Rendering;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// Represents the L-System fractal generation engine.
    /// </summary>
    public class LSystem : GeometricFractal2D
    {
        #region Properties

        public override string Caption
        {
            get
            {
                return String.Format("{0} (L-System)", Name);
            }
        }

        public string Axiom { get; set; }

        public List<Rule> Rules { get; set; }

        [XmlArrayItem("Var")]
        public string[] DrawingVariables { get; set; }

        public double Angle { get; set; }

        public double InitialAngle { get; set; }

        public Color Color
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public LSystem()
        {
        }

        public LSystem(string axiom, string[] drawVars, List<Rule> rules,
            double angle, double initialAngle, Color color, string name)
        {
            Axiom = axiom;
            DrawingVariables = drawVars;
            Rules = rules;
            Angle = angle;
            InitialAngle = initialAngle;

            Color = color;
            Name = name;
        }

        #endregion

        #region Methods

        public override void Render(IGraphics2DContext context)
        {
            string generator = Expander(Axiom, Rules, Iterations);

            Rectangle area = RenderOrCalcMeasure(context, generator, Angle, InitialAngle, Point.Empty, 1, false);

            double w = context.Viewport.Width;
            double h = context.Viewport.Height;

            double scale_x = w / area.Width;
            double scale_y = h / area.Height;

            double step = Math.Min(scale_x, scale_y);

            Point loc = new Point();
            loc.X = -area.X * step + (w - area.Width * step) / 2;
            loc.Y = -area.Y * step + (h - area.Height * step) / 2;

            RenderOrCalcMeasure(context, generator, Angle, InitialAngle, loc, step, true);
        }

        private string Expander(string axiom, IEnumerable<Rule> rules, int level)
        {
            string generator = axiom;

            for (int i = 0; i < level; i++)
            {
                foreach (Rule rule in rules)
                {
                    generator = generator.Replace(rule.Var, "{" + rule.Var + "}");
                }

                foreach (Rule rule in rules)
                {
                    generator = generator.Replace("{" + rule.Var + "}", rule.Replacement);
                }
            }

            foreach (string var in DrawingVariables)
            {
                if (Char.IsUpper(var, 0))
                    generator = generator.Replace(var, "F");
                else if (Char.IsLower(var, 0))
                    generator = generator.Replace(var, "f");
            }

            return generator;
        }

        private Rectangle RenderOrCalcMeasure(IGraphics2DContext context,
            string generator, double angle, double initAngle,
            Point loc, double step, bool render = true)
        {
            Stack<State> states = new Stack<State>();
            State state;

            double x = loc.X;
            double y = loc.Y;

            double xn;
            double yn;

            double xmin = x;
            double ymin = y;
            double xmax = x;
            double ymax = y;

            double currAngle = initAngle;

            foreach (char c in generator)
            {
                switch (c)
                {
                    case '+':
                        currAngle += angle;
                        break;

                    case '-':
                        currAngle -= angle;
                        break;

                    case '[':
                        states.Push(new State(currAngle, x, y));
                        break;

                    case ']':
                        state = states.Pop();
                        currAngle = state.Angle;
                        x = state.X;
                        y = state.Y;
                        break;

                    case 'f':
                    case 'F':
                        xn = x + step * Math.Cos(currAngle * Math.PI / 180);
                        yn = y + step * Math.Sin(currAngle * Math.PI / 180);

                        if (c == 'F' && render)
                            context.DrawLine(x, y, xn, yn, Color);

                        x = xn; y = yn;

                        if (x > xmax) xmax = x;
                        if (x < xmin) xmin = x;
                        if (y > ymax) ymax = y;
                        if (y < ymin) ymin = y;
                        break;

                    default:
                        break;
                }
            }

            return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
        }

        #endregion

        #region Nested Types

        public class Rule
        {
            [XmlAttribute]
            public string Var { get; set; }

            [XmlAttribute]
            public string Replacement { get; set; }


            public Rule()
            {
            }

            public Rule(string var, string replacement)
            {
                Var = var;
                Replacement = replacement;
            }
        }

        private struct State
        {
            private double _angle;
            private double _x;
            private double _y;

            public double Angle
            {
                get
                {
                    return _angle;
                }

                set
                {
                    _angle = value;
                }
            }

            public double X
            {
                get
                {
                    return _x;
                }

                set
                {
                    _x = value;
                }
            }

            public double Y
            {
                get
                {
                    return _y;
                }

                set
                {
                    _y = value;
                }
            }


            public State(double angle, double x, double y)
            {
                _angle = angle;
                _x = x;
                _y = y;
            }
        }

        #endregion
    }
}
