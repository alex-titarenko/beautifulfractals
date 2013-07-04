using System;
using System.Collections.Generic;
using System.Text;

namespace TAlex.BeautifulFractals.KeyGenerator
{
    /// <summary>
    /// Key generator exception class.
    /// </summary>
    public class KeyGeneratorException : Exception
    {
        #region Fields

        public readonly ReturnCode ReturnCode;

        #endregion

        #region Constructors

        public KeyGeneratorException(string message, ReturnCode e)
            : base(message)
        {
            ReturnCode = e;
        }

        #endregion
    }
}
