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
using TAlex.Common.Diagnostics.ErrorReporting;
using System.Net.Mail;
using System.Diagnostics;
using TAlex.Common.Models;


namespace TAlex.BeautifulFractals.Services
{
    /// <summary>
    /// Interaction logic for ErrorReportingWindow.xaml
    /// </summary>
    public partial class ErrorReportingWindow : Window
    {
        #region Fields

        protected readonly AssemblyInfo AssemblyInfo;
        protected readonly IErrorReportSender ErrorReportSender = new ErrorReportSender();

        public ErrorReport Report { get; set; }

        #endregion

        #region Constructors

        public ErrorReportingWindow()
        {
            InitializeComponent();
        }

        public ErrorReportingWindow(ErrorReport report, AssemblyInfo assemblyInfo)
            : this()
        {
            AssemblyInfo = assemblyInfo;
            Report = report;

            productTitleRun.Text = AssemblyInfo.Title;
            productTitle2Run.Text = AssemblyInfo.Title;
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
                ErrorReportSender.Send(new ErrorReportModel() { Subject = Report.Subject, Report = Report.GenerateFullHtmlReport() });
                MessageBox.Show(this, "Thank you. Your error report has been sent successfully.", AssemblyInfo.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, AssemblyInfo.Title, MessageBoxButton.OK, MessageBoxImage.Error);
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
