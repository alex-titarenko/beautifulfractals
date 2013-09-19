using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.Fractals;


namespace TAlex.BeautifulFractals.Services
{
    public interface IFractalsManager
    {
        ObservableCollection<Fractal> Load(string filePath);
        void Save(ObservableCollection<Fractal> fractals, string filePath);
    }
}
