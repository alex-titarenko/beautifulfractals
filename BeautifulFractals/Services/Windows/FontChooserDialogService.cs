using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.Helpers;
using TAlex.WPF.CommonDialogs;
using TAlex.WPF.Mvvm.Extensions;

namespace TAlex.BeautifulFractals.Services.Windows
{
    public class FontChooserDialogService : IFontChooserDialogService
    {
        #region IFontChooserDialogService Members

        public string SelectedFontFamily { get; set; }

        public double SelectedFontSize { get; set; }

        public Rendering.Color SelectedFontColor { get; set; }


        public bool? ShowDialog()
        {
            FontChooserDialog fontChooser = new FontChooserDialog();
            fontChooser.SelectedFontFamily = new System.Windows.Media.FontFamily(SelectedFontFamily);
            fontChooser.SelectedFontSize = SelectedFontSize;
            fontChooser.SelectedFontColor = ColorHelper.ToWpfColor(SelectedFontColor);

            fontChooser.ShowTextDecorations = false;
            fontChooser.Owner = App.Current.GetActiveWindow();

            var result = fontChooser.ShowDialog();

            if (result == true)
            {
                SelectedFontFamily = fontChooser.SelectedFontFamily.Source;
                SelectedFontSize = fontChooser.SelectedFontSize;
                SelectedFontColor = ColorHelper.FromWpfColor(fontChooser.SelectedFontColor);
            }

            return result;
        }

        #endregion
    }
}
