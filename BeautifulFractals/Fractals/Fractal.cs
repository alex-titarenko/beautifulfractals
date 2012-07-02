using System;
using System.Xml.Serialization;


namespace TAlex.BeautifulFractals.Fractals
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Fractal
    {
        public abstract string Caption { get; }

        public bool Display { get; set; }


        public Fractal()
        {
            Display = true;
        }
    }
}
