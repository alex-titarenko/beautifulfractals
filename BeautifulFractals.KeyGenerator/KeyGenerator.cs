using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TAlex.BeautifulFractals.KeyGenerator.Helpers;


namespace TAlex.BeautifulFractals.KeyGenerator
{
    internal class BeautifulFractalsKeyGenerator : IKeyGenerator
    {
        #region Fields

        private static readonly Random _rnd = new Random();

        private const int RegKeyLength = 150;

        private static readonly byte[] SK = new byte[]
        {
            11, 11, 2, 243, 13, 5, 118, 39
        };

        private static readonly byte[] IV = new byte[]
        {
            34, 0, 1, 138, 16, 15, 2, 247
        };

        private static readonly string[] _secretKeys = new string[]
        {
            "Ds9W668fHA8hKH402h587diEhS2xE942PKoid1N0",
	        "xNAGh3mm0kSug5s1TWT1pMHtr817G8GYjzYrQFtG",
	        "3L9Xhorv072Y5uPvOL6WdXRZKU16J4H1EX403s5E",
	        "7x57Gm833AGmKuJfj700x3i79l9yh5k3QD6gFquT",
	        "z2RKqZS62dkkxTC2qt3B952ayY3Nu2k3rvKO1k78",
	        "8j3Zedq5r3t8D6XjTuq50D15KxpL5P9sNwIPBOS5",
	        "4BBZ46435gZLK8hS36tiV7RUVk41ZrTg36YGA3l4",
	        "RnlXJ1q31GVM6zld38PfO2m1Q9k60G4713w65JGu",
	        "l5pHV6T8q25ITc0J3hl8W5H4R2G185ZQOge0WLCs",
	        "f0UnMaIJp5t0aXSiP93O8yX27tDe56ni20Fxb594",
	        "ih528v7hkL948v09zPerCEtL56d11hts0tpMdE46",
	        "cwT2GthRxuNnYZc3Ke3TELhec5Gt1sbrsybIY0uK",
	        "q5s30GuHacj6wj5mqtNV53hn8ao3VXGF4E4dy1c0",
	        "G53l13ma24o0a8zI4B2tJ0HCAn9R1noDrKg4Oi8o",
	        "Cb07tnnsKX92Ck48aOplrawYMbt29hiabB6BowUE",
	        "7QtxxX4FIr34dpLyMWZg832nW0zY9sxKqvsY7JG2",
	        "17Kv3ry212YEjn9vy4sjPL4H61JUr4yt1vKYPoqK",
	        "kw77I43B69wl65Xem9KWgaoGr8jL376YT67aCQ0b",
	        "oCL3M19G8iZ9mcai8R2fyVuh8DXmP27dFFmT068a",
	        "kb92ORpZQ9R53jr78Y1As6HfZ1l8h6zay33S6vM7"
        };

        #endregion

        #region IKeyGenerator Members

        public object Generate(IDictionary<string, string> inputs)
        {
            SHA512 sha = new SHA512Managed();

            string regName = inputs[LicenseData.RegistrationNameParam];
            if (regName.Length < 5)
            {
                throw new KeyGeneratorException("REGNAME must have at least 5 characters", ReturnCode.ERC_BAD_INPUT);
            }

            string regNameHash = CryptoHelper.SHA512Base64(regName, new UTF8Encoding());

            const int linHashStartIndex = 10;
            const int secretKeyStartIndex = 108;

            string regKey = StringHelper.GenerateRandomString(linHashStartIndex);
            regKey += regNameHash;
            regKey += StringHelper.GenerateRandomString(secretKeyStartIndex - regKey.Length);

            int secretKeyIndex = _rnd.Next(_secretKeys.Length);
            regKey += _secretKeys[secretKeyIndex];

            regKey += StringHelper.GenerateRandomString(RegKeyLength - regKey.Length);

            DESCryptoServiceProvider cipher = new DESCryptoServiceProvider();
            cipher.IV = IV;
            cipher.Key = SK;
            byte[] encRegKeyData = CryptoHelper.Encrypt(regKey, cipher);
            string encRegKey = Convert.ToBase64String(encRegKeyData);

            return encRegKey;
        }

        #endregion
    }
}
