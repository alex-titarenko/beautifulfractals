using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Xml.Serialization;


namespace TAlex.BeautifulFractals.Rendering
{
    [Serializable]
    public struct Size
    {
        #region Fields

        public static readonly Size Empty;

        private double _width;
        private double _height;

        #endregion

        #region Properties

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
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return ((this._width == 0) && (this._height == 0));
            }
        }

        #endregion

        #region Constructors

        static Size()
        {
            Empty = new Size();
        }

        public Size(Size size)
            : this(size.Width, size.Height)
        {
        }

        public Size(Point pt)
            : this(pt.X, pt.Y)
        {
        }

        public Size(double width, double height)
        {
            _width = width;
            _height = height;
        }

        #endregion

        #region Methods

        public static Size Add(Size sz1, Size sz2)
        {
            return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        public static Size Subtract(Size sz1, Size sz2)
        {
            return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Size))
            {
                return false;
            }
            Size ef = (Size)obj;
            return (((ef.Width == this.Width) && (ef.Height == this.Height)) && ef.GetType().Equals(base.GetType()));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Point ToPoint()
        {
            return (Point)this;
        }

        public override string ToString()
        {
            return ("{Width=" + this._width.ToString(CultureInfo.CurrentCulture) + ", Height=" + this._height.ToString(CultureInfo.CurrentCulture) + "}");
        }

        #endregion


        public static Size operator +(Size sz1, Size sz2)
        {
            return Add(sz1, sz2);
        }

        public static Size operator -(Size sz1, Size sz2)
        {
            return Subtract(sz1, sz2);
        }

        public static bool operator ==(Size sz1, Size sz2)
        {
            return ((sz1.Width == sz2.Width) && (sz1.Height == sz2.Height));
        }

        public static bool operator !=(Size sz1, Size sz2)
        {
            return !(sz1 == sz2);
        }

        public static explicit operator Point(Size size)
        {
            return new Point(size.Width, size.Height);
        }
    }
}
