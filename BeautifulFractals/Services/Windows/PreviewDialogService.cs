using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.Fractals;
using TAlex.BeautifulFractals.Infrastructure;
using TAlex.BeautifulFractals.ViewModels;
using TAlex.BeautifulFractals.Views;

namespace TAlex.BeautifulFractals.Services.Windows
{
    public class PreviewDialogService : IPreviewDialogService
    {
        private PreviewWindow _window;


        #region IPreviewDialogService Members

        public void Show(ICollectionView fractalCollection)
        {
            PreviewWindowViewModel viewModel;

            if (_window == null || !_window.IsLoaded)
            {
                _window = new PreviewWindow();
                viewModel = new PreviewWindowViewModel(fractalCollection);
                _window.DataContext = viewModel;
                _window.Show();
            }
            else
            {
                viewModel = _window.DataContext as PreviewWindowViewModel;
                _window.Activate();
            }

            viewModel.RenderFractal();
        }

        #endregion
    }
}
