using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Globalization;


namespace TAlex.BeautifulFractals.MathCore
{
    public struct Complex
    {
        #region Fields

        private double _real;
        private double _imag;

        public static readonly Complex Zero = Complex.FromRealImaginary(0, 0);
        public static readonly Complex I = Complex.FromRealImaginary(0, 1);
        public static readonly Complex MaxValue = Complex.FromRealImaginary(double.MaxValue, double.MaxValue);

        #endregion

        #region Properties

        [XmlAttribute]
        public double Re
        {
            get { return _real; }
            set { _real = value; }
        }

        [XmlAttribute]
        public double Im
        {
            get { return _imag; }
            set { _imag = value; }
        }

        /// <summary>
        /// Gets a value that indicates whether the complex number is equal to zero.
        /// </summary>
        [XmlIgnore]
        public bool IsZero
        {
            get
            {
                return ((_real == 0.0) && (_imag == 0.0));
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the imaginary part is equal to zero.
        /// </summary>
        [XmlIgnore]
        public bool IsReal
        {
            get
            {
                return _imag == 0;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the real part is equal to zero.
        /// </summary>
        [XmlIgnore]
        public bool IsImaginary
        {
            get
            {
                return _real == 0;
            }
        }

        #endregion

        #region Methods

        public Complex(double real, double imaginary)
        {
            this._real = real;
            this._imag = imaginary;
        }

        // Create complex number from real and imaginary coords
        public static Complex FromRealImaginary(double real, double imaginary)
        {
            Complex c;
            c._real = real;
            c._imag = imaginary;
            return c;
        }

        // Create complex number from polar coordinates
        public static Complex FromModulusArgument(double modulus, double argument)
        {
            Complex c;
            c._real = modulus * Math.Cos(argument);
            c._imag = modulus * Math.Sin(argument);
            return c;
        }

        public override bool Equals(object obj)
        {
            Complex c2 = (Complex)obj;

            return (this == c2);
        }

        public override int GetHashCode()
        {
            return (_real.GetHashCode() ^ _imag.GetHashCode());
        }

        // Radius of complex vector in polar coordinates
        public static double Modulus(Complex c)
        {
            double x = c._real;
            double y = c._imag;
            return Math.Sqrt(x * x + y * y);
        }

        // Angle of complex vector in polar coordinates 
        public static double Argument(Complex c)
        {
            return Math.Atan2(c._imag, c._real);
        }

        public static Complex Add(Complex c1, Complex c2)
        {
            return (c1 + c2);
        }

        public static Complex Subtract(Complex c1, Complex c2)
        {
            return (c1 - c2);
        }

        public static Complex Multiply(Complex c1, Complex c2)
        {
            return (c1 * c2);
        }

        public static Complex Divide(Complex c1, Complex c2)
        {
            return (c1 / c2);
        }

        public static Complex Sqrt(Complex c)
        {
            double _halfOfRoot2 = 0.5 * Math.Sqrt(2);

            double x = c.Re;
            double y = c.Im;

            double modulus = Complex.Modulus(c);
            int sign = (y < 0) ? -1 : 1;

            c.Re = (double)(_halfOfRoot2 * Math.Sqrt(modulus + x));
            c.Im = (double)(_halfOfRoot2 * sign * Math.Sqrt(modulus - x));

            return c;
        }

        public static Complex Pow(Complex c, double exponent)
        {
            double x = c.Re;
            double y = c.Im;

            double modulus = Math.Pow(x * x + y * y, exponent * 0.5);
            double argument = Math.Atan2(y, x) * exponent;

            c.Re = (double)(modulus * System.Math.Cos(argument));
            c.Im = (double)(modulus * System.Math.Sin(argument));

            return c;
        }

        public static bool IsInfinity(Complex c)
        {
            return double.IsInfinity(c._real) || double.IsInfinity(c._imag);
        }

        public static bool IsNAN(Complex c)
        {
            return double.IsNaN(c._real) || double.IsNaN(c._imag);
        }

        public static Complex Parse(String s)
        {
            return Parse(s, null);
        }

        public static Complex Parse(string s, IFormatProvider provider)
        {
            const string pattern = @"([-+]?[0-9.,]+[ij]?)";

            MatchCollection matches = Regex.Matches(s, pattern);

            Complex c = Complex.Zero;

            foreach (Match match in matches)
            {
                string term = match.Value;

                if (term.EndsWith("i") || term.EndsWith("j"))
                    c += Complex.FromRealImaginary(0, double.Parse(term.Substring(0, term.Length - 1), provider));
                else
                    c += double.Parse(term, provider);
            }

            return c;
        }


        /// <summary>
        /// Converts the complex value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString(null, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the complex value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString(null, provider);
        }

        /// <summary>
        /// Converts the complex value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format)
        {
            return ToString(format, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the complex value of this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An System.IFormatProvider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        /// <exception cref="System.FormatException">The format is invalid.</exception>
        public string ToString(string format, IFormatProvider provider)
        {
            NumberFormatInfo formatInfo = NumberFormatInfo.GetInstance(provider);

            string real = _real.ToString(format, provider);
            string imag = Math.Abs(_imag).ToString(format, provider);

            if (this == Zero) return "0";
            if (IsReal) return real;
            else if (IsImaginary && _imag < 0)
                return String.Format("{0}{1}i", formatInfo.NegativeSign, imag);
            else if (IsImaginary && _imag >= 0)
                return String.Format("{0}i", imag);
            else if (_imag < 0)
                return String.Format("{0} {1} {2}i", real, formatInfo.NegativeSign, imag);
            else
                return String.Format("{0} {1} {2}i", real, formatInfo.PositiveSign, imag);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Returns a value indicating whether
        /// two instances of complex number are equal.
        /// </summary>
        /// <param name="c1">The first complex number to compare.</param>
        /// <param name="c2">The second complex number to compare.</param>
        /// <returns>True if the c1 and c2 parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Complex c1, Complex c2)
        {
            return (c1._real == c2._real && c1._imag == c2._imag);
        }

        /// <summary>
        /// Returns a value indicating whether
        /// two instances of complex number are not equal.
        /// </summary>
        /// <param name="c1">The first complex number to compare.</param>
        /// <param name="c2">The second complex number to compare.</param>
        /// <returns>True if c1 and c2 are not equal; otherwise, false.</returns>
        public static bool operator !=(Complex c1, Complex c2)
        {
            return !(c1 == c2);
        }

        /// <summary>
        /// Returns the value of the complex number operand
        /// (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The value of the operand, c.</returns>
        public static Complex operator +(Complex c)
        {
            return c;
        }

        /// <summary>
        /// Negates the value of the complex number operand.
        /// </summary>
        /// <param name="c">A complex number.</param>
        /// <returns>The result of c multiplied by negative one.</returns>
        public static Complex operator -(Complex c)
        {
            Complex complex;
            complex._real = (c._real != 0) ? -c._real : 0;
            complex._imag = (c._imag != 0) ? -c._imag : 0;

            return complex;
        }

        /// <summary>
        /// Adds two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the first term).</param>
        /// <param name="c2">A complex number (the second term).</param>
        /// <returns>The sum of complex numbers.</returns>
        public static Complex operator +(Complex c1, Complex c2)
        {
            Complex c;
            c._real = c1._real + c2._real;
            c._imag = c1._imag + c2._imag;

            return c;
        }

        /// <summary>
        /// Subtracts one complex number from another.
        /// </summary>
        /// <param name="c1">A complex number (the minuend).</param>
        /// <param name="c2">A complex number (the subtrahend).</param>
        /// <returns>The result of subtracting c2 from c1.</returns>
        public static Complex operator -(Complex c1, Complex c2)
        {
            Complex c;
            c._real = c1._real - c2._real;
            c._imag = c1._imag - c2._imag;

            return c;
        }

        /// <summary>
        /// Multiplies two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the multiplicand).</param>
        /// <param name="c2">A complex number (the multiplier).</param>
        /// <returns>The product of c1 and c2.</returns>
        public static Complex operator *(Complex c1, Complex c2)
        {
            Complex c;
            c._real = c1._real * c2._real - c1._imag * c2._imag;
            c._imag = c1._real * c2._imag + c1._imag * c2._real;

            return c;
        }

        /// <summary>
        /// Divides two complex numbers.
        /// </summary>
        /// <param name="c1">A complex number (the dividend).</param>
        /// <param name="c2">A complex number (the divisor).</param>
        /// <returns>The result of dividing c1 by c2.</returns>
        public static Complex operator /(Complex c1, Complex c2)
        {
            double ar = c1._real, ai = c1._imag;
            double br = c2._real, bi = c2._imag;

            Complex complex;

            if (Math.Abs(bi) < Math.Abs(br))
            {
                double denom = br + (bi * (bi / br));

                complex._real = (ar + (ai * (bi / br))) / denom;
                complex._imag = (ai - (ar * (bi / br))) / denom;
            }
            else
            {
                double denom = bi + (br * (br / bi));

                complex._real = (ai + (ar * (br / bi))) / denom;
                complex._imag = (-ar + (ai * (br / bi))) / denom;
            }

            return complex;
        }

        /// <summary>
        /// Converts a 16-bit signed integer to a complex number.
        /// </summary>
        /// <param name="value">A 16-bit signed integer.</param>
        /// <returns>A complex number that represents the converted 16-bit signed integer.</returns>
        public static implicit operator Complex(short value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a 32-bit signed integer to a complex number.
        /// </summary>
        /// <param name="value">A 32-bit signed integer.</param>
        /// <returns>A complex number that represents the converted 32-bit signed integer.</returns>
        public static implicit operator Complex(int value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a 64-bit signed integer to a complex number.
        /// </summary>
        /// <param name="value">A 64-bit signed integer.</param>
        /// <returns>A complex number that represents the converted 64-bit signed integer.</returns>
        public static implicit operator Complex(long value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a single-precision floating-point number to a complex number.
        /// </summary>
        /// <param name="value">A single-precision floating-point number.</param>
        /// <returns>A complex number that represents the converted single-precision floating-point number.</returns>
        public static implicit operator Complex(float value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a double-precision floating-point number to a complex number.
        /// </summary>
        /// <param name="value">A double-precision floating-point number.</param>
        /// <returns>A complex number that represents the converted double-precision floating-point number.</returns>
        public static implicit operator Complex(double value)
        {
            Complex c;
            c._real = value;
            c._imag = 0.0;

            return c;
        }

        /// <summary>
        /// Converts a complex number to a 16-bit signed integer.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A 16-bit signed integer that represents the converted complex number.</returns>
        public static explicit operator short(Complex value)
        {
            return (short)value._real;
        }

        /// <summary>
        /// Converts a complex number to a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A 32-bit signed integer that represents the converted complex number.</returns>
        public static explicit operator int(Complex value)
        {
            return (int)value._real;
        }

        /// <summary>
        /// Converts a complex number to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A 64-bit signed integer that represents the converted complex number.</returns>
        public static explicit operator long(Complex value)
        {
            return (long)value._real;
        }

        /// <summary>
        /// Converts a complex number to a single-precision floating-point number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A single-precision floating-point number that represents the converted complex number.</returns>
        public static explicit operator float(Complex value)
        {
            return (float)value._real;
        }

        /// <summary>
        /// Converts a complex number to a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A complex number.</param>
        /// <returns>A double-precision floating-point number that represents the converted complex number.</returns>
        public static explicit operator double(Complex value)
        {
            return value._real;
        }

        #endregion
    }
}
