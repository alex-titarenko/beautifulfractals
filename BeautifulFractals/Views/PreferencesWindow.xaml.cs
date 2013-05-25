using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using TAlex.WPF.CommonDialogs;
using TAlex.Common.Extensions;
using TAlex.Common.Environment;

using TAlex.BeautifulFractals.Helpers;


namespace TAlex.BeautifulFractals
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        #region Constructors

        public PreferencesWindow()
        {
            InitializeComponent();
            LoadFractals();
            LoadSettings();

            Title = String.Format("{0} Preferences", ApplicationInfo.Title);
        }

        #endregion

        #region Methds

        private void LoadFractals()
        {
            try
            {
                _fractals = FractalsManager.Load(Properties.Settings.Default.FractalsCollectionPath);
                fractalsListView.ItemsSource = _fractals; //FractalsManager.Load(Properties.Settings.Default.FractalsCollectionPath);
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSettings()
        {
            Properties.Settings settings = Properties.Settings.Default;
            
            randomOrderCheckBox.IsChecked = settings.RandomOrder;
            exitOnMouseMoveCheckBox.IsChecked = settings.ExitOnMouseMove;
            showFractalCaptionsCheckBox.IsChecked = settings.ShowFractalCaptions;

            delaySlider.Value = settings.Delay.TotalSeconds;

            firstBackColorChip.SelectedColor = ColorHelper.ToWpfColor(settings.Background_FirstColor);
            secondBackColorChip.SelectedColor = ColorHelper.ToWpfColor(settings.Background_SecondColor);
        }

        private void SaveSettings()
        {
            Properties.Settings settings = Properties.Settings.Default;

            settings.RandomOrder = (bool)randomOrderCheckBox.IsChecked;
            settings.ExitOnMouseMove = (bool)exitOnMouseMoveCheckBox.IsChecked;
            settings.ShowFractalCaptions = (bool)showFractalCaptionsCheckBox.IsChecked;
            
            settings.Delay = TimeSpan.FromSeconds(delaySlider.Value);

            settings.Background_FirstColor = ColorHelper.FromWpfColor(firstBackColorChip.SelectedColor);
            settings.Background_SecondColor = ColorHelper.FromWpfColor(secondBackColorChip.SelectedColor);

            settings.Save();
        }

        #region Event Handlers

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void swapBackColors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Color tempColor = firstBackColorChip.SelectedColor;
            firstBackColorChip.SelectedColor = secondBackColorChip.SelectedColor;
            secondBackColorChip.SelectedColor = tempColor;
        }

        private void bgTypeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBackgroundType();
        }

        private void captionStyleTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Properties.Settings settings = Properties.Settings.Default;

            FontChooserDialog fontChooser = new FontChooserDialog();
            fontChooser.ShowTextDecorations = false;
            fontChooser.Owner = this;

            fontChooser.SelectedFontFamily = new FontFamily(settings.Caption_FontFamily);
            fontChooser.SelectedFontSize = settings.Caption_FontSize;
            fontChooser.SelectedFontColor = ColorHelper.ToWpfColor(settings.Caption_FontColor);

            if (fontChooser.ShowDialog() == true)
            {
                settings.Caption_FontFamily = fontChooser.SelectedFontFamily.Source;
                settings.Caption_FontSize = fontChooser.SelectedFontSize;
                settings.Caption_FontColor = ColorHelper.FromWpfColor(fontChooser.SelectedFontColor);
            }
        }


        System.Collections.ObjectModel.ObservableCollection<Fractals.Fractal> _fractals;
        private void searchQueryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = searchQueryTextBox.Text;

            if (!String.IsNullOrEmpty(text))
            {
                string query = text;

                if (text.TrimEnd().Length == text.Length)
                    query += "*";
                query = query.Trim();

                fractalsListView.ItemsSource = _fractals.Search(query,
                    new List<Func<Fractals.Fractal, object>>() { { x => x.Caption } },
                    DefaultOperator.And, DefaultComplianceType.Strict);
            }
            else
            {
                fractalsListView.ItemsSource = _fractals;
            }

        }

        private void clearQueryButton_Click(object sender, RoutedEventArgs e)
        {
            searchQueryTextBox.Text = String.Empty;
        }

        #endregion

        #region Helpers

        private void UpdateBackgroundType()
        {
            Point startPoint = new Point();
            Point endPoint = new Point();

            if (verticalBgTypeRadioButton.IsChecked == true)
            {
                endPoint = new Point(0, 1);
            }
            else if (horizontalBgTypeRadioButton.IsChecked == true)
            {
                endPoint = new Point(1, 0);
            }
            else if (forwardDiagonalBgTypeRadioButton.IsChecked == true)
            {
                endPoint = new Point(1, 1);
            }
            else if (backwardDiagonalBgTypeRadioButton.IsChecked == true)
            {
                startPoint = new Point(1, 0);
                endPoint = new Point(0, 1);
            }
            else
                throw new ArgumentException();
            
            bgPreviewBrush.StartPoint = startPoint;
            bgPreviewBrush.EndPoint = endPoint;
        }

        #endregion

        #endregion
    }
}
