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


namespace TAlex.BeautifulFractals.Services
{
    /// <summary>
    /// Interaction logic for ErrorReportingWindow.xaml
    /// </summary>
    public partial class ErrorReportingWindow : Window
    {
        #region Fields

        public ErrorReport Report { get; set; }

        #endregion

        #region Constructors

        public ErrorReportingWindow()
        {
            InitializeComponent();
        }

        public ErrorReportingWindow(ErrorReport report)
            : this()
        {
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
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.talex-soft.com/company/contact.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    string requestStr = String.Format(
                        "name={0}&email={1}&subject={2}&message={3}",
                        "Anonymous user", "noname@gmail.com",
                        Report.Subject, Report.GenerateFullHtmlReport());

                    byte[] bytes = Encoding.UTF8.GetBytes(requestStr);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, ApplicationInfo.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    MessageBox.Show(this, "Thank you. Your error report has been sent successfully.", ApplicationInfo.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(this, "", ApplicationInfo.Title, MessageBoxButton.OK, MessageBoxImage.Error);

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
