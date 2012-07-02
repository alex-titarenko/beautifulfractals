using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Threading;

using TAlex.Common.Diagnostics.ErrorReporting;
using TAlex.BeautifulFractals.Helpers;


namespace TAlex.BeautifulFractals
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructors

        public App()
        {
            DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        #endregion

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                string oprtion = e.Args[0].ToLower().Substring(0, 2);

                switch (oprtion)
                {
                    case ScreensaverCommandLineArgs.Configure:
                        new PreferencesWindow().Show();
                        return;

                    case ScreensaverCommandLineArgs.Preview:
                        IntPtr previewHandle = (IntPtr)uint.Parse(e.Args[1]);
                        Helpers.Win32.RECT rect = Helpers.Win32.GetClientRect(previewHandle);

                        HwndSourceParameters sourceParams = new HwndSourceParameters("sourceParams");

                        sourceParams.PositionX = 0;
	                    sourceParams.PositionY = 0;
                        sourceParams.Height = rect.Bottom - rect.Top;
                        sourceParams.Width = rect.Right - rect.Left;
                        sourceParams.ParentWindow = previewHandle;
    	                sourceParams.WindowStyle = (int)(
                            Win32.WindowStyles.WS_VISIBLE |
                            Win32.WindowStyles.WS_CHILD |
                            Win32.WindowStyles.WS_CLIPCHILDREN);

                        HwndSource winWPFContent = new HwndSource(sourceParams);
                        winWPFContent.Disposed += new EventHandler(winWPFContent_Disposed);

                        Image previewImage = new Image();
                        previewImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/;component/Resources/Images/preview.png"));
                        winWPFContent.RootVisual = previewImage;
                        return;

                    case ScreensaverCommandLineArgs.Show:
                        new MainWindow().Show();
                        return;

                    default:
                        MessageBox.Show(String.Format("Invalid command line argument: {0}", oprtion),
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }
            }
            
            new PreferencesWindow().Show();
        }

        private void winWPFContent_Disposed(object sender, EventArgs e)
	    {
	        Application.Current.Shutdown();
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ProcessingUnhandledException(e.Exception);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ProcessingUnhandledException(e.ExceptionObject as Exception);
        }
        
        private void ProcessingUnhandledException(Exception exc)
        {
            Trace.TraceError(exc.ToString());

            Services.ErrorReportingWindow reportWindow =
                new Services.ErrorReportingWindow(new ErrorReport(exc));

            Window activeWindow = null;
            foreach (Window w in Windows)
            {
                if (w.IsActive)
                {
                    activeWindow = w;
                    break;
                }
            }

            if (activeWindow != null)
            {
                reportWindow.Owner = activeWindow;
            }
            reportWindow.ShowDialog();
        }

        #endregion
    }
}
