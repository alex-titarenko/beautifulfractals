using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using TAlex.BeautifulFractals.Fractals;


namespace TAlex.BeautifulFractals.Services
{
    public class FractalsManager : IFractalsManager
    {
        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(FractalsCollection));


        public ObservableCollection<Fractal> Load(Stream stream)
        {
            return ((FractalsCollection)_serializer.Deserialize(stream)).Fractals;
        }

        public ObservableCollection<Fractal> Load(string filePath)
        {
            ObservableCollection<Fractal> fractals = null;
            using (FileStream file = new FileStream(Environment.ExpandEnvironmentVariables(filePath), FileMode.Open, FileAccess.Read))
            {
                fractals = Load(file);
            }
            return fractals;
        }


        public void Save(ObservableCollection<Fractal> fractals, Stream stream)
        {
            FractalsCollection list = new FractalsCollection();
            list.Fractals = fractals;
            _serializer.Serialize(stream, list);
        }

        public void Save(ObservableCollection<Fractal> fractals, string filePath)
        {
            using (FileStream file = new FileStream(Environment.ExpandEnvironmentVariables(filePath), FileMode.Create, FileAccess.Write))
            {
                Save(fractals, file);
            }
        }
    }
}
