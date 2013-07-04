using System;
using System.Collections.Generic;
using System.Text;

namespace TAlex.BeautifulFractals.KeyGenerator.Helpers
{
    public static class StringHelper
    {
        private static Random _rnd = new Random();

        public static string GenerateRandomString(int length)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int mode = _rnd.Next(0, 3);

                if (mode == 0)
                    sb.Append((Char)('A' + _rnd.Next(0, 26)));
                else if (mode == 1)
                    sb.Append((Char)('a' + _rnd.Next(0, 26)));
                else if (mode == 2)
                    sb.Append((Char)('0' + _rnd.Next(0, 10)));
            }

            return sb.ToString();
        }
    }
}
