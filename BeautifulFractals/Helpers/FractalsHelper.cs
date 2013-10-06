using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TAlex.BeautifulFractals.Helpers
{
    internal class FractalsHelper
    {
        public static Stream GetEmbeddedFractalsStream()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("TAlex.BeautifulFractals.Fractals.xml");
        }
    }
}
