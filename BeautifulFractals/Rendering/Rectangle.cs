using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;


namespace TAlex.BeautifulFractals.Rendering
{
    [Serializable]
    public struct Rectangle
    {
        public static readonly Rectangle Empty;
        private double _x;
        private double _y;
        private double _width;
        private double _height;


        static Rectangle()
        {
            Empty = new Rectangle();
        }

        public Rectangle(double x, double y, double width, double height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public Rectangle(Point location, Size size)
        {
            _x = location.X;
            _y = location.Y;
            _width = size.Width;
            _height = size.Height;
        }

        public static Rectangle FromLTRB(double left, double top, double right, double bottom)
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }

        [XmlIgnore]
        public Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        [XmlIgnore]
        public Size Size
        {
            get
            {
                return new Size(this.Width, this.Height);
            }
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        [XmlAttribute]
        public double X
        {
            get
            {
                return this._x;
            }
            set
            {
                this._x = value;
            }
        }

        [XmlAttribute]
        public double Y
        {
            get
            {
                return this._y;
            }
            set
            {
                this._y = value;
            }
        }

        [XmlAttribute]
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        [XmlAttribute]
        public double Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public double Left
        {
            get
            {
                return this.X;
            }
        }

        public double Top
        {
            get
            {
                return this.Y;
            }
        }

        public double Right
        {
            get
            {
                return (this.X + this.Width);
            }
        }

        public double Bottom
        {
            get
            {
                return (this.Y + this.Height);
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (this.Width > 0.0)
                {
                    return (this.Height <= 0.0);
                }
                return true;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
            {
                return false;
            }
            Rectangle ef = (Rectangle)obj;
            return ((((ef.X == this.X) && (ef.Y == this.Y)) && (ef.Width == this.Width)) && (ef.Height == this.Height));
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return ((((left.X == right.X) && (left.Y == right.Y)) && (left.Width == right.Width)) && (left.Height == right.Height));
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        public bool Contains(double x, double y)
        {
            return ((((this.X <= x) && (x < (this.X + this.Width))) && (this.Y <= y)) && (y < (this.Y + this.Height)));
        }

        public bool Contains(Point pt)
        {
            return this.Contains(pt.X, pt.Y);
        }

        public bool Contains(Rectangle rect)
        {
            return ((((this.X <= rect.X) && ((rect.X + rect.Width) <= (this.X + this.Width))) && (this.Y <= rect.Y)) && ((rect.Y + rect.Height) <= (this.Y + this.Height)));
        }

        public override int GetHashCode()
        {
            return (int)(((((uint)this.X) ^ ((((uint)this.Y) << 13) | (((uint)this.Y) >> 0x13))) ^ ((((uint)this.Width) << 0x1a) | (((uint)this.Width) >> 6))) ^ ((((uint)this.Height) << 7) | (((uint)this.Height) >> 0x19)));
        }

        public void Inflate(double x, double y)
        {
            this.X -= x;
            this.Y -= y;
            this.Width += 2.0 * x;
            this.Height += 2.0 * y;
        }

        public void Inflate(Size size)
        {
            this.Inflate(size.Width, size.Height);
        }

        public static Rectangle Inflate(Rectangle rect, double x, double y)
        {
            Rectangle ef = rect;
            ef.Inflate(x, y);
            return ef;
        }

        public void Intersect(Rectangle rect)
        {
            Rectangle ef = Intersect(rect, this);
            this.X = ef.X;
            this.Y = ef.Y;
            this.Width = ef.Width;
            this.Height = ef.Height;
        }

        public static Rectangle Intersect(Rectangle a, Rectangle b)
        {
            double x = Math.Max(a.X, b.X);
            double num2 = Math.Min(a.X + a.Width, b.X + b.Width);
            double y = Math.Max(a.Y, b.Y);
            double num4 = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if ((num2 >= x) && (num4 >= y))
            {
                return new Rectangle(x, y, num2 - x, num4 - y);
            }
            return Empty;
        }

        public bool IntersectsWith(Rectangle rect)
        {
            return ((((rect.X < (this.X + this.Width)) && (this.X < (rect.X + rect.Width))) && (rect.Y < (this.Y + this.Height))) && (this.Y < (rect.Y + rect.Height)));
        }

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            double x = Math.Min(a.X, b.X);
            double num2 = Math.Max(a.X + a.Width, b.X + b.Width);
            double y = Math.Min(a.Y, b.Y);
            double num4 = Math.Max(a.Y + a.Height, b.Y + b.Height);
            return new Rectangle(x, y, num2 - x, num4 - y);
        }

        public void Offset(Point pos)
        {
            this.Offset(pos.X, pos.Y);
        }

        public void Offset(double x, double y)
        {
            this.X += x;
            this.Y += y;
        }

        public override string ToString()
        {
            return ("{X=" + this.X.ToString(CultureInfo.CurrentCulture) + ",Y=" + this.Y.ToString(CultureInfo.CurrentCulture) + ",Width=" + this.Width.ToString(CultureInfo.CurrentCulture) + ",Height=" + this.Height.ToString(CultureInfo.CurrentCulture) + "}");
        }
    }

}