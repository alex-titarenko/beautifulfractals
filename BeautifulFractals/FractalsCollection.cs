using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Serialization;

using TAlex.BeautifulFractals.Fractals;


namespace TAlex.BeautifulFractals
{
    public class FractalsCollection
    {
        [XmlArrayItem(typeof(ApollonianGasket))]
        [XmlArrayItem(typeof(CantorDust))]
        [XmlArrayItem(typeof(CantorSet))]
        [XmlArrayItem(typeof(Hopalong))]
        [XmlArrayItem(typeof(IFS))]
        [XmlArrayItem(typeof(JuliaSet))]
        [XmlArrayItem(typeof(LorenzAttractor))]
        [XmlArrayItem(typeof(LSystem))]
        [XmlArrayItem(typeof(MandelbrotSet))]
        [XmlArrayItem(typeof(NewtonBasins))]
        [XmlArrayItem(typeof(PhoenixSet))]
        [XmlArrayItem(typeof(SierpinskiCarpet))]
        [XmlArrayItem(typeof(SierpinskiTriangle))]
        public ObservableCollection<Fractal> Fractals { get; set; }
    }
}
