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
        private PreviewWindowViewModel _viewModel;


        #region IPreviewDialogService Members

        public void Show(ICollectionView fractalCollection)
        {
            if (_viewModel == null || !Object.ReferenceEquals(_viewModel.FractalCollection, fractalCollection))
                _viewModel = new PreviewWindowViewModel(fractalCollection);

            if (_window == null || !_window.IsLoaded)
            {
                _viewModel.Plot = null;
                _viewModel.Title = String.Empty;
                _window = new PreviewWindow { DataContext = _viewModel };
                _window.Show();
            }
            else
            {
                _window.DataContext = _viewModel;
                _window.Activate();
            }

            _viewModel.RenderFractal();
        }

        #endregion
    }
}
