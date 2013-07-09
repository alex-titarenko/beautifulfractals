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
using System.Windows.Shapes;
using System.Net;
using System.IO;

using TAlex.Common.Environment;
using TAlex.Common.Diagnostics.ErrorReporting;
using System.Net.Mail;
using System.Diagnostics;


namespace TAlex.BeautifulFractals.Services
{
    /// <summary>
    /// Interaction logic for ErrorReportingWindow.xaml
    /// </summary>
    public partial class ErrorReportingWindow : Window
    {
        #region Fields

        protected ApplicationInfo ApplicationInfo;

        public ErrorReport Report { get; set; }

        #endregion

        #region Constructors

        public ErrorReportingWindow()
        {
            InitializeComponent();
        }

        public ErrorReportingWindow(ErrorReport report, ApplicationInfo applicationInfo)
            : this()
        {
            ApplicationInfo = applicationInfo;
            Report = report;

            productTitleRun.Text = ApplicationInfo.Title;
            productTitle2Run.Text = ApplicationInfo.Title;
        }

        #endregion

        #region Methods

        #region Event Handlers

        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string body = Report.GenerateFullPlainTextReport();
                string encodedBody = body.Replace(Environment.NewLine, "%0D%0A"); // encode new line separator
                Process.Start(String.Format(@"mailto:support@talex-soft.com?subject={0}&body={1}", Report.Subject, encodedBody));
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, ApplicationInfo.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Application.Current.Shutdown();
            }
        }

        private void dontSendButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #endregion
    }
}
