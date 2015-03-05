using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.ComponentModel;


namespace TAlex.BeautifulFractals.Rendering
{
    public struct Color : IXmlSerializable
    {
        #region Fields

        private const int ARGBAlphaShift = 0x18;
        private const int ARGBRedShift = 0x10;
        private const int ARGBGreenShift = 8;
        private const int ARGBBlueShift = 0;

        public static readonly Color Empty;
        private static Random random;
        private long value;

        #endregion

        #region Properties

        [XmlAttribute]
        public byte R
        {
            get
            {
                return (byte)((this.Value >> ARGBRedShift) & 0xffL);
            }
        }

        [XmlAttribute]
        public byte G
        {
            get
            {
                return (byte)((this.Value >> ARGBGreenShift) & 0xffL);
            }
        }

        [XmlAttribute]
        public byte B
        {
            get
            {
                return (byte)(this.Value & 0xffL);
            }
        }

        [XmlAttribute]
        public byte A
        {
            get
            {
                return (byte)((this.Value >> ARGBAlphaShift) & 0xffL);
            }
        }

        [XmlIgnore]
        public bool IsEmpty
        {
            get
            {
                return (this == Empty);
            }
        }

        private string ARGBValue
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "ARGB=({0}, {1}, {2}, {3})", A, R, G, B);
            }
        }

        private long Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion

        #region Constructors

        static Color()
        {
            Empty = new Color();
            random = new Random();
        }

        private Color(long value)
        {
            this.value = value;
        }

        #endregion

        #region Methods

        private static void CheckByte(int value)
        {
            if ((value < 0) || (value > 0xff))
            {
                throw new ArgumentException("InvalidEx2BoundArgument");
            }
        }

        private static long MakeArgb(byte alpha, byte red, byte green, byte blue)
        {
            return (long)(((ulong)((((red << 0x10) | (green << 8)) | blue) | (alpha << 0x18))) & 0xffffffffL);
        }

        public static Color FromArgb(int argb)
        {
            return new Color(argb & ((long)0xffffffffL));
        }

        public static Color FromArgb(int alpha, int red, int green, int blue)
        {
            CheckByte(alpha);
            CheckByte(red);
            CheckByte(green);
            CheckByte(blue);
            return new Color(MakeArgb((byte)alpha, (byte)red, (byte)green, (byte)blue));
        }

        public static Color FromArgb(int alpha, Color baseColor)
        {
            CheckByte(alpha);
            return new Color(MakeArgb((byte)alpha, baseColor.R, baseColor.G, baseColor.B));
        }

        public static Color FromArgb(int red, int green, int blue)
        {
            return FromArgb(0xff, red, green, blue);
        }

        public static Color Random()
        {
            return Random(random);
        }

        public static Color Random(Random rnd)
        {
            return FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
        }

        public static Color Parse(string s)
        {
            Color c;
            if (!s.StartsWith("#"))
            {
                throw new FormatException();
            }

            c.value = long.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier);
            return c;
        }

        public float GetBrightness()
        {
            float num = ((float)this.R) / 255f;
            float num2 = ((float)this.G) / 255f;
            float num3 = ((float)this.B) / 255f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            return ((num4 + num5) / 2f);
        }

        public float GetHue()
        {
            if ((this.R == this.G) && (this.G == this.B))
            {
                return 0f;
            }
            float num = ((float)this.R) / 255f;
            float num2 = ((float)this.G) / 255f;
            float num3 = ((float)this.B) / 255f;
            float num7 = 0f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            float num6 = num4 - num5;
            if (num == num4)
            {
                num7 = (num2 - num3) / num6;
            }
            else if (num2 == num4)
            {
                num7 = 2f + ((num3 - num) / num6);
            }
            else if (num3 == num4)
            {
                num7 = 4f + ((num - num2) / num6);
            }
            num7 *= 60f;
            if (num7 < 0f)
            {
                num7 += 360f;
            }
            return num7;
        }

        public float GetSaturation()
        {
            float num = ((float)this.R) / 255f;
            float num2 = ((float)this.G) / 255f;
            float num3 = ((float)this.B) / 255f;
            float num7 = 0f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            if (num4 == num5)
            {
                return num7;
            }
            float num6 = (num4 + num5) / 2f;
            if (num6 <= 0.5)
            {
                return ((num4 - num5) / (num4 + num5));
            }
            return ((num4 - num5) / ((2f - num4) - num5));
        }

        public int ToArgb()
        {
            return (int)this.Value;
        }

        public override string ToString()
        {
            return String.Format("#{0:x}", Value);
        }

        public static bool operator ==(Color left, Color right)
        {
            if (left.value != right.value)
                return false;
            else
                return true;
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is Color)
            {
                Color color = (Color)obj;
                if (this.value == color.value)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (this.value.GetHashCode());
        }

        #endregion

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            string str = reader.ReadElementContentAsString();
            Color c = Parse(str);
            value = c.value;
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteValue(ToString());
        }

        #endregion
    }
}
