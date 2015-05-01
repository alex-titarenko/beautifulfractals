using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TAlex.BeautifulFractals.Fractals;
using TAlex.BeautifulFractals.Properties;
using TAlex.BeautifulFractals.Rendering;
using TAlex.BeautifulFractals.Services;
using TAlex.BeautifulFractals.Services.Windows;
using TAlex.Common.Extensions;
using TAlex.Common.Models;
using TAlex.Mvvm;
using TAlex.Mvvm.Commands;
using TAlex.Mvvm.Services;
using TAlex.Mvvm.ViewModels;


namespace TAlex.BeautifulFractals.ViewModels
{
    public class PreferencesWindowViewModel : ViewModelBase
    {
        #region Fields

        protected readonly IAppSettings AppSettings;
        protected readonly AssemblyInfo AssemblyInfo;
        protected readonly IFractalsManager FractalManager;
        protected readonly IFontChooserDialogService FontChooserDialogService;
        protected readonly BeautifulFractals.Infrastructure.ICollectionViewFactory CollectionViewFactory;
        protected readonly IPreviewDialogService PreviewDialogService;
        protected readonly IMessageService MessageService;

        private ObservableCollection<Fractal> _fractals;
        public BeautifulFractals.Infrastructure.ICollectionView _fractalsView;
        private bool? _displayAllFractals;
        private string _fractalsSearchQuery;
        private Func<Fractal, bool> _searchPredicate;
        private bool _closeSignal;
        private Object _syncObj = new Object();

        #endregion

        #region Properties

        public string Title
        {
            get
            {
                return String.Format("{0} Preferences", AssemblyInfo.Title);
            }
        }


        public Color PrimaryBackColor
        {
            get
            {
                return Color.Parse(AppSettings.PrimaryBackColor);
            }

            set
            {
                AppSettings.PrimaryBackColor = value.ToString();
                RaisePropertyChanged(() => PrimaryBackColor);
            }
        }

        public Color SecondaryBackColor
        {
            get
            {
                return Color.Parse(AppSettings.SecondaryBackColor);
            }

            set
            {
                AppSettings.SecondaryBackColor = value.ToString();
                RaisePropertyChanged(() => SecondaryBackColor);
            }
        }

        public BackgroundGradientType BackGradientType
        {
            get
            {
                return AppSettings.BackGradientType;
            }

            set
            {
                AppSettings.BackGradientType = value;
                RaisePropertyChanged(() => BackGradientType);
            }
        }

        public TimeSpan Delay
        {
            get
            {
                return AppSettings.Delay;
            }

            set
            {
                AppSettings.Delay = value;
                RaisePropertyChanged(() => Delay);
            }
        }

        public bool RandomOrder
        {
            get
            {
                return AppSettings.RandomOrder;
            }

            set
            {
                AppSettings.RandomOrder = value;
                RaisePropertyChanged(() => RandomOrder);
            }
        }

        public bool ExitOnMouseMove
        {
            get
            {
                return AppSettings.ExitOnMouseMove;
            }

            set
            {
                AppSettings.ExitOnMouseMove = value;
                RaisePropertyChanged(() => ExitOnMouseMove);
            }
        }

        public bool ShowFractalCaptions
        {
            get
            {
                return AppSettings.ShowFractalCaptions;
            }

            set
            {
                AppSettings.ShowFractalCaptions = value;
                RaisePropertyChanged(() => ShowFractalCaptions);
            }
        }


        public string CaptionFontFamily
        {
            get
            {
                return AppSettings.CaptionFontFamily;
            }

            set
            {
                AppSettings.CaptionFontFamily = value;
            }
        }

        public double CaptionFontSize
        {
            get
            {
                return AppSettings.CaptionFontSize;
            }

            set
            {
                AppSettings.CaptionFontSize = value;
            }
        }

        public Rendering.Color CaptionFontColor
        {
            get
            {
                return Color.Parse(AppSettings.CaptionFontColor); 
            }

            set
            {
                AppSettings.CaptionFontColor = value.ToString();
            }
        }

        public bool? DisplayAllFractals
        {
            get
            {
                return _displayAllFractals;
            }

            set
            {
                Set(() => DisplayAllFractals, ref _displayAllFractals, value);
                UpdateDisplayProperty();
            }
        }

        public string FractalsSearchQuery
        {
            get
            {
                return _fractalsSearchQuery;
            }

            set
            {
                lock (_syncObj)
                {
                    Set(() => FractalsSearchQuery, ref _fractalsSearchQuery, value);
                    ClearSearchQueryCommand.RaiseCanExecuteChanged();
                    UpdateSearchPredicate();
                    _fractalsView.Refresh();
                }
            }
        }

        public ObservableCollection<Fractal> Fractals
        {
            get
            {
                return _fractals;
            }

            set
            {
                _fractals = value;
                _fractalsView = CollectionViewFactory.GetView(_fractals);
                _fractalsView.Filter = SearchFilter;
            }
        }

        public BeautifulFractals.Infrastructure.ICollectionView FractalsView
        {
            get
            {
                return _fractalsView;
            }
        }

        public bool CloseSignal
        {
            get
            {
                return _closeSignal;
            }

            set
            {
                Set(() => CloseSignal, ref _closeSignal, value);
            }
        }

        #endregion

        #region Commands

        public ICommand SwapBackgroundColorsCommand { get; set; }

        public ICommand OpenCaptionStyleChooserDialogCommand { get; set; }

        public ICommand ClearSearchQueryCommand { get; set; }

        public ICommand OpenFractalPreviewCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        #endregion

        #region Constructors

        public PreferencesWindowViewModel(
            IAppSettings appSettings,
            AssemblyInfo assemblyInfo,
            FontChooserDialogService fontChooserDialogService,
            IFractalsManager fractalManager,
            BeautifulFractals.Infrastructure.ICollectionViewFactory collectionViewFactory,
            IPreviewDialogService previewDialogService,
            IMessageService messageService)
        {
            AppSettings = appSettings;
            AssemblyInfo = assemblyInfo;
            FontChooserDialogService = fontChooserDialogService;
            FractalManager = fractalManager;
            CollectionViewFactory = collectionViewFactory;
            PreviewDialogService = previewDialogService;
            MessageService = messageService;

            LoadFractals();
            InitCommands();
        }

        #endregion

        #region Methods

        private void InitCommands()
        {
            SwapBackgroundColorsCommand = new RelayCommand(SwapBackgroundColorsCommandExecute);
            OpenCaptionStyleChooserDialogCommand = new RelayCommand(OpenCaptionStyleChooserDialogCommandExecute);
            ClearSearchQueryCommand = new RelayCommand(ClearSearchQueryExecute, ClearSearchQueryCanExecute);
            OpenFractalPreviewCommand = new RelayCommand<Fractal>(OpenFractalPreviewExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
        }

        private void LoadFractals()
        {
            Fractals = FractalManager.Load(AppSettings.FractalsCollectionPath);
        }


        private void SwapBackgroundColorsCommandExecute()
        {
            var temp = PrimaryBackColor;
            PrimaryBackColor = SecondaryBackColor;
            SecondaryBackColor = temp;
        }

        private void OpenCaptionStyleChooserDialogCommandExecute()
        {
            FontChooserDialogService.SelectedFontFamily = CaptionFontFamily;
            FontChooserDialogService.SelectedFontSize = CaptionFontSize;
            FontChooserDialogService.SelectedFontColor = CaptionFontColor;

            if (FontChooserDialogService.ShowDialog() == true)
            {
                CaptionFontFamily = FontChooserDialogService.SelectedFontFamily;
                CaptionFontSize = FontChooserDialogService.SelectedFontSize;
                CaptionFontColor = FontChooserDialogService.SelectedFontColor;
            }
        }

        private bool ClearSearchQueryCanExecute()
        {
            return !String.IsNullOrEmpty(FractalsSearchQuery);
        }

        private void ClearSearchQueryExecute()
        {
            FractalsSearchQuery = String.Empty;
        }

        private void OpenFractalPreviewExecute(Fractal fractal)
        {
            if (fractal != null)
            {
                FractalsView.MoveCurrentTo(fractal);
                PreviewDialogService.Show(FractalsView);
            }
        }

        private void SaveCommandExecute()
        {
            AppSettings.Save();
            try
            {
                FractalManager.Save(Fractals, AppSettings.FractalsCollectionPath);
            }
            catch (IOException exc)
            {
                MessageService.ShowError(exc.Message, Resources.locInformationMessageCaption);
            }
            CloseSignal = true;
        }

        public void CancelCommandExecute()
        {
            CloseSignal = true;
        }


        private void UpdateDisplayProperty()
        {
            if (DisplayAllFractals.HasValue)
            {
                foreach (Fractal fractal in FractalsView) fractal.Display = DisplayAllFractals.Value;
            }
        }

        private void UpdateSearchPredicate()
        {
            string text = (FractalsSearchQuery + String.Empty);
            Func<Fractal, bool> predicate;

            if (!String.IsNullOrWhiteSpace(text))
            {
                string query = text;

                if (text.TrimEnd().Length == text.Length)
                    query += "*";
                query = query.Trim();

                predicate = EnumerableSearcher<Fractal>.BuildSearchPredicate(query,
                    new List<Func<Fractals.Fractal, object>>() { { x => x.Caption } },
                    DefaultOperator.And, DefaultComplianceType.Strict);
            }
            else
            {
                predicate = x => true;
            }

            _searchPredicate = predicate;
        }

        private bool SearchFilter(object o)
        {
            return _searchPredicate != null ? _searchPredicate(o as Fractal) : true;
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
