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
using TAlex.BeautifulFractals.Views;
using TAlex.BeautifulFractals.Services;


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
        }

        #endregion

        #region Methds

        #region Event Handlers

        private void searchQueryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string text = searchQueryTextBox.Text;

            //if (!String.IsNullOrEmpty(text))
            //{
            //    string query = text;

            //    if (text.TrimEnd().Length == text.Length)
            //        query += "*";
            //    query = query.Trim();

            //    fractalsListView.ItemsSource = _fractals.Search(query,
            //        new List<Func<Fractals.Fractal, object>>() { { x => x.Caption } },
            //        DefaultOperator.And, DefaultComplianceType.Strict);
            //}
            //else
            //{
            //    fractalsListView.ItemsSource = _fractals;
            //}

        }

        #endregion

        #endregion
    }
}
