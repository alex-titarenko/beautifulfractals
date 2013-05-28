using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TAlex.BeautifulFractals.Views
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            //string lin = linTextBox.Text.Trim();
            //string lik = likTextBox.Text.Trim();

            //if (String.IsNullOrEmpty(lin))
            //{
            //    MessageBox.Show(this, "Please input license name.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}
            //else if (String.IsNullOrEmpty(lik))
            //{
            //    MessageBox.Show(this, "Please input license key.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}

            //MessageBox.Show(this, "Please restart this program to verify your license data.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            //LicenseDataProvider licData = new LicFileLicenseDataProvider();
            //licData.LicenseName = lin;
            //licData.LicenseKey = lik;
            //licData.Save();

            DialogResult = true;
            App.Current.Shutdown();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
