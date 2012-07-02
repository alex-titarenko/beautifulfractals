using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Xml.Serialization;


namespace TAlex.BeautifulFractals.Rendering
{
    [Serializable]
    public struct Point
    {
        public static readonly Point Empty;
        private double _x;
        private double _y;

        static Point()
        {
            Empty = new Point();
        }

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        [XmlAttribute]
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

        [XmlAttribute]
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

        public bool IsEmpty
        {
            get
            {
                return ((_x == 0) && (_y == 0));
            }
        }


        public static Point operator +(Point pt, Size sz)
        {
            return Add(pt, sz);
        }

        public static Point operator -(Point pt, Size sz)
        {
            return Subtract(pt, sz);
        }

        public static bool operator ==(Point left, Point right)
        {
            return ((left.X == right.X) && (left.Y == right.Y));
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public static Point Add(Point pt, Size sz)
        {
            return new Point(pt.X + sz.Width, pt.Y + sz.Height);
        }

        public static Point Subtract(Point pt, Size sz)
        {
            return new Point(pt.X - sz.Width, pt.Y - sz.Height);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
            {
                return false;
            }
            Point tf = (Point)obj;
            return (((tf.X == this.X) && (tf.Y == this.Y)) && tf.GetType().Equals(base.GetType()));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{X={0}, Y={1}}}", new object[] { this._x, this._y });
        }
    }
}
