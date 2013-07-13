using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TAlex.BeautifulFractals.Fractals;
using TAlex.BeautifulFractals.Rendering;
using TAlex.BeautifulFractals.Services.Windows;
using TAlex.WPF.Mvvm;
using TAlex.WPF.Mvvm.Commands;


namespace TAlex.BeautifulFractals.ViewModels
{
    public class PreferencesWindowViewModel : ViewModelBase
    {
        #region Fields

        private string _title;
        private Color _primaryBackColor;
        private Color _secondaryBackColor;
        private BackgroundGradientType _backGradientType;
        private TimeSpan _delay;
        private bool _randomOrder;
        private bool _exitOnMouseMove;
        private bool _showFractalCaptions;

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

        public Color PrimaryBackColor
        {
            get
            {
                return _primaryBackColor;
            }

            set
            {
                Set(() => PrimaryBackColor, ref _primaryBackColor, value);
            }
        }

        public Color SecondaryBackColor
        {
            get
            {
                return _secondaryBackColor;
            }

            set
            {
                Set(() => SecondaryBackColor, ref _secondaryBackColor, value);
            }
        }

        public BackgroundGradientType BackGradientType
        {
            get
            {
                return _backGradientType;
            }

            set
            {
                Set(() => BackGradientType, ref _backGradientType, value);
            }
        }

        public TimeSpan Delay
        {
            get
            {
                return _delay;
            }

            set
            {
                Set(() => Delay, ref _delay, value);
            }
        }

        public bool RandomOrder
        {
            get
            {
                return _randomOrder;
            }

            set
            {
                Set(() => RandomOrder, ref _randomOrder, value);
            }
        }

        public bool ExitOnMouseMove
        {
            get
            {
                return _exitOnMouseMove;
            }

            set
            {
                Set(() => ExitOnMouseMove, ref _exitOnMouseMove, value);
            }
        }

        public bool ShowFractalCaptions
        {
            get
            {
                return _showFractalCaptions;
            }

            set
            {
                Set(() => ShowFractalCaptions, ref _showFractalCaptions, value);
            }
        }


        public ObservableCollection<Fractal> Fractals { get; set; }

        #endregion

        #region Commands

        public ICommand SwapBackgroundColors { get; set; }

        public ICommand SaveSettingsCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        #endregion

        #region Constructors

        public PreferencesWindowViewModel()
        {
            Fractals = new ObservableCollection<Fractal>();

            SwapBackgroundColors = new RelayCommand(SwapBackgroundColorsExecute);
            SaveSettingsCommand = new RelayCommand(SaveSettingsCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
        }

        #endregion

        #region Methods

        private void SwapBackgroundColorsExecute()
        {
            var temp = PrimaryBackColor;
            PrimaryBackColor = SecondaryBackColor;
            SecondaryBackColor = temp;
        }

        private void SaveSettingsCommandExecute()
        {
        }

        public void CancelCommandExecute()
        {

        }

        #endregion
    }

    public enum BackgroundGradientType
    {
        Vertical,
        Horizontal,
        ForwardDiagonal,
        BackwardDiagonal
    }
}
