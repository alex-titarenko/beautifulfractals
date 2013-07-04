using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TAlex.BeautifulFractals.KeyGenerator.Helpers
{
    public static class CryptoHelper
    {
        public static string SHA512Base64(string source, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(source);
            return Convert.ToBase64String(new SHA512Managed().ComputeHash(data));
        }

        public static byte[] Encrypt(string plainText, SymmetricAlgorithm cipher)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream encStream = new CryptoStream(ms, cipher.CreateEncryptor(), CryptoStreamMode.Write);

            StreamWriter sw = new StreamWriter(encStream);
            sw.WriteLine(plainText);

            sw.Close();
            encStream.Close();

            byte[] buffer = ms.ToArray();

            ms.Close();
            return buffer;
        }
    }
}
