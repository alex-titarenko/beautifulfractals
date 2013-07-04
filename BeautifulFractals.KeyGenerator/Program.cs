using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace TAlex.BeautifulFractals.KeyGenerator
{
    class Program
    {
        #region Fields

        /// <summary>
        /// Input file encoding.
        /// </summary>
        private static Encoding _fileEncoding = new UTF8Encoding();

        private static readonly IKeyGenerator BeautifulFractalsKeyGenerator = new BeautifulFractalsKeyGenerator();

        #endregion

        #region Methods

        /// <summary>
        /// Get input string values, return empty string if not defined.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(IDictionary<string, string> inputs, string key)
        {
            string value;

            if (inputs.TryGetValue(key, out value))
                return value;
            else
                return String.Empty;
        }

        /// <summary>
        /// Split a string at the first equals sign and add key/value to Inputs[].
        /// </summary>
        /// <param name="line"></param>
        public static void AddInputLine(IDictionary<string, string> inputs, string line)
        {
            int posEqual = line.IndexOf('=');

            if (posEqual > 0)
            {
                string key = line.Remove(posEqual, line.Length - posEqual);
                string value = line.Substring(posEqual + 1);

                if (value.Length > 0)
                {
                    inputs.Add(key, value);
                }
            }
        }

        /// <summary>
        /// Read the input file and parse its lines into the Inputs[] list.
        /// </summary>
        /// <param name="pathname"></param>
        public static IDictionary<string, string> ReadInputValues()
        {
            IDictionary<string, string> inputs = new Dictionary<string, string>();

            // process every line in the file
            for (String line = Console.ReadLine(); !String.IsNullOrEmpty(line); line = Console.ReadLine())
            {
                AddInputLine(inputs, line.Trim());
            }
            return inputs;
        }

        public static void Main(string[] args)
        {
            try
            {
                Console.InputEncoding = _fileEncoding;
                IDictionary<string, string> inputs = ReadInputValues();

                HandleWriteKey(BeautifulFractalsKeyGenerator.Generate(inputs));
            }
            catch (KeyGeneratorException e)
            {
                // set the exit code to the ERC of the exception object
                Console.Error.WriteLine(e.Message);
                Environment.ExitCode = (int)e.ReturnCode;
            }
            catch (Exception e)
            {
                // for general exceptions return ERC_ERROR
                Console.Error.WriteLine(e.Message);
                Environment.ExitCode = (int)ReturnCode.ERC_ERROR;
            }
        }

        private static void HandleWriteKey(object key)
        {
            if (key is String)
            {
                Console.Write((string)key);
                Environment.ExitCode = (int)ReturnCode.ERC_SUCCESS;
            }
            else if (key is byte[])
            {
                MemoryStream data = new MemoryStream((byte[])key);
                using (Stream console = Console.OpenStandardOutput())
                {
                    data.WriteTo(console);
                    Environment.ExitCode = (int)ReturnCode.ERC_SUCCESS_BIN;
                }
            }
            else
            {
                throw new KeyGeneratorException("Key type is not supported.", ReturnCode.ERC_INTERNAL);
            }
        }

        #endregion
    }
}