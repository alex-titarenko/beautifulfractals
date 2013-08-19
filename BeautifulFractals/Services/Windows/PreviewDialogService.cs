using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.Fractals;
using TAlex.BeautifulFractals.Infrastructure;
using TAlex.BeautifulFractals.Views;

namespace TAlex.BeautifulFractals.Services.Windows
{
    public class PreviewDialogService : IPreviewDialogService
    {
        private PreviewWindow _window;


        #region IPreviewDialogService Members

        public void Show(ICollectionView fractalCollection)
        {
            if (_window == null || !_window.IsLoaded)
            {
                _window = new PreviewWindow();
                _window.FractalCollection = fractalCollection;
                _window.Show();
            }
            else
            {
                _window.Activate();
            }

            _window.RenderFractal();
        }

        #endregion
    }
}
