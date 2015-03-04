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

        #region Event Handlers

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        #endregion
    }
}
