using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TAlex.BeautifulFractals.Fractals;
using TAlex.BeautifulFractals.Infrastructure;
using TAlex.BeautifulFractals.Rendering;
using TAlex.WPF.Mvvm;
using TAlex.WPF.Mvvm.Commands;


namespace TAlex.BeautifulFractals.ViewModels
{
    public class PreviewWindowViewModel : ViewModelBase
    {
        #region Fields

        private string _title;
        private bool _isBusy;
        private ImageSource _plot;

        protected ICollectionView FractalCollection { get; set; }

        #endregion

        #region Properties

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                Set(() => Title, ref _title, value);
            }
        }

        public double PlotWidth { get; set; }

        public double PlotHeight { get; set; }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                Set(() => IsBusy, ref _isBusy, value);
            }
        }

        public ImageSource Plot
        {
            get
            {
                return _plot;
            }

            set
            {
                Set(() => Plot, ref _plot, value);
            }
        }

        public ICommand ShowPrevCommand { get; set; }

        public ICommand ShowNextCommand { get; set; }

        #endregion

        #region Constructors

        public PreviewWindowViewModel(ICollectionView fractalCollection)
        {
            RegisterCommands();

            FractalCollection = fractalCollection;

            // Set default values
            Title = "Preview";
        }

        #endregion

        #region Methods

        public void RenderFractal()
        {
            if (IsBusy) return;
            IsBusy = true;

            Task.Run(() =>
            {
                WriteableBitmap wb = BitmapFactory.New((int)PlotWidth, (int)PlotHeight);
                Fractal2D targetFractal = FractalCollection.CurrentItem as Fractal2D;

                if (targetFractal != null)
                {
                    WriteableBitmapGraphicsContext context = new WriteableBitmapGraphicsContext(wb);
                    targetFractal.Render(context);
                    context.Invalidate();
                }

                App.Current.Dispatcher.Invoke(() =>
                {
                    Plot = wb;
                    IsBusy = false;
                    Title = String.Format("{0} - Preview", ((Fractal)FractalCollection.CurrentItem).Caption);
                });
            });
        }

        private void RegisterCommands()
        {
            ShowPrevCommand = new RelayCommand(ShowPrevCommandExecute, ShowPrevCommandCanExecute);
            ShowNextCommand = new RelayCommand(ShowNextCommandExecute, ShowNextCommandCanExecute);
        }

        private void ShowPrevCommandExecute()
        {
            FractalCollection.MoveCurrentToPrevious();
            RenderFractal();
        }

        private bool ShowPrevCommandCanExecute()
        {
            return !FractalCollection.IsCurrentBeforeFirst;
        }

        private void ShowNextCommandExecute()
        {
            FractalCollection.MoveCurrentToNext();
            RenderFractal();
        }

        private bool ShowNextCommandCanExecute()
        {
            return !FractalCollection.IsCurrentAfterLast;
        }

        #endregion
    }
}
