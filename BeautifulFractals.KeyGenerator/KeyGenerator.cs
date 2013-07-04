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
            18, 52, 75, 11, 76, 14, 195, 87
        };

        private static readonly byte[] IV = new byte[]
        {
            246, 198, 57, 228, 138, 4, 126, 118
        };

        private static readonly string[] _secretKeys = new string[]
        {
            "gVmT19eF8TmAdg1IuJjN5AFrP0lpR9Xx2UH7boU8",
            "2PL5YLlABO09XcWryRy7iOls2mJc0yavmt30lfh3",
            "GxBF97x3zc6Xi8Up9D2c3HAiI1vC2M7IH3VD6PMR",
            "wIfv68Gd71e8aFi3Cg3D424866MVEqaNH49wqow8",
            "PlDI49u547UZI2S5kGTbWiqu2NScgusfoiH5d0Rx",
            "C54o507PIa7OwVcZO9T1900OT7E610o4b14DyQAi",
            "8uB8nUwflcFI0fWBrwIGC0u6Bvn85NiGmNuRTWla",
            "U4t2XR6Tg4VvHqThqI0ecrixAZQ7I77r090k0hCI",
            "TltPZIiQL0uN8VW7IfCjxw2k84sqi3Pf4j9QIGOr",
            "svEuV77z7j40A530f5e197yGI6bvD47k152xMM4C",
            "JItM8nVJ11v23H0DWIw2YA0CSsDmIo41wdk74lCS",
            "66THlxj9Dhj9FO3sn7D2J81kPTNqWKm088o53b2v",
            "QXECYxru7e5up3ngXC08klPGFQ6j3B2q0CmXFUJ1",
            "F2wc3X8R6vwz9m8Cum02paZ3A57ByU8ZY8m1ev5Q",
            "seRSWHfflJrmq8Ddos9JP7fk6Z4YAqsyHJ7r27xD",
            "4hE9N5A8J91qzx6f01qKS4S8bJ17mZw7Ls8zaP4D",
            "41CzYi1Nogu4zZ4vg7LF9fEbvVLsib8PUmZnUHWN",
            "2AoUh73v1pr3I9aKYaM0VzC83K280sLdN9883uh3",
            "11r3O7DEzjYgGD1nW2nUX09S7aH8WX4K3yf8d11d",
            "WMP290pIuwoXBR0ShlNY87Tf3UsEqeiEBL9v769N"
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
