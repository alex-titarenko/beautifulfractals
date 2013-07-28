using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.Rendering;
using TAlex.BeautifulFractals.ViewModels;


namespace TAlex.BeautifulFractals.Services
{
    public interface IAppSettings
    {
        Color PrimaryBackColor { get; set; }

        Color SecondaryBackColor {get; set; }

        BackgroundGradientType BackGradientType { get; set; }

        TimeSpan Delay { get; set; }

        bool RandomOrder { get; set; }

        bool ExitOnMouseMove { get; set; }

        bool ShowFractalCaptions { get; set; }


        string CaptionFontFamily { get; set; }

        double CaptionFontSize { get; set; }

        Color CaptionFontColor { get; set; }

        string FractalsCollectionPath { get; }

        void Save();
    }
}
