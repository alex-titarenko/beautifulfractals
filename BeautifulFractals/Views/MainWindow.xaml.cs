using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

using TAlex.BeautifulFractals.Fractals;
using Rendering = TAlex.BeautifulFractals.Rendering;
using TAlex.Common.Extensions;


namespace TAlex.BeautifulFractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private Thread _mainThread;
        private List<Fractal> _fractals;

        private TimeSpan _delay = TimeSpan.Zero;
        private bool _exitOnMouseMove;
        private Point? _oldMousePos = null;

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
            LoadFractals();

#if DEBUG
            Topmost = false;
#endif
        }

        #endregion

        #region Methods

        #region Event Handlers

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_oldMousePos != null && _exitOnMouseMove)
            {
                Quit();
            }
            _oldMousePos = e.GetPosition(this);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Quit();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Quit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.None;

            Rendering.GdiGraphicsContext context = new Rendering.GdiGraphicsContext(this);

            _mainThread = new Thread((ThreadStart)delegate() { RenderFractals(context); });
            _mainThread.Priority = ThreadPriority.Lowest;
            _mainThread.IsBackground = true;
            _mainThread.Start();

        }

        #endregion

        private void RenderFractals(Rendering.IGraphics2DContext context)
        {
            Properties.Settings settings = Properties.Settings.Default;

            Rendering.LinearGradientBrush brush = new Rendering.LinearGradientBrush();
            brush.FromColor = settings.PrimaryBackColor;
            brush.ToColor = settings.SecondaryBackColor;
            brush.Angle = 90;


            context.Clear(brush);
            Thread.Sleep(TimeSpan.FromSeconds(2));

            Rendering.Font captionFont = new Rendering.Font(settings.CaptionFontFamily, settings.CaptionFontSize);

            while (true)
            {
                foreach (Fractal2D fractal in _fractals)
                {
                    context.Clear(brush);

                    Rendering.Size captionSize = context.MeasureString(fractal.Caption, captionFont);
                    
                    context.DrawString(fractal.Caption, captionFont,
                        settings.CaptionFontColor,
                        new Rendering.Point(context.Viewport.Width / 2 - captionSize.Width / 2, context.Viewport.Height - 20 - captionSize.Height));
                    
                    fractal.Render(context);
                    Thread.Sleep(_delay);
                }
            }
        }

        private void LoadSettings()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;


            Properties.Settings settings = Properties.Settings.Default;

            _exitOnMouseMove = settings.ExitOnMouseMove;
            _delay = settings.Delay;
            captionTextBlock.Visibility = settings.ShowFractalCaptions ?
                System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }

        private void LoadFractals()
        {
            try
            {
                _fractals = FractalsManager.Load(Properties.Settings.Default.FractalsCollectionPath).
                    Where(f => f.Display).
                    ToList();

                if (Properties.Settings.Default.RandomOrder)
                    _fractals.Shuffle();
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Quit();
            }
        }

        private void Quit()
        {
            if (_mainThread != null)
                _mainThread.Abort();
            
            App.Current.Shutdown();
        }

        #endregion
    }
}
