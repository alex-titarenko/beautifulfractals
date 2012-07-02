using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAlex.BeautifulFractals.Rendering
{
    public class Font
    {
        public string FamilyName { get; set; }

        public string Typeface { get; set; }

        public double Size { get; set; }


        public Font(string familyName, double size)
        {
            FamilyName = familyName;
            Size = size;
        }
    }
}
