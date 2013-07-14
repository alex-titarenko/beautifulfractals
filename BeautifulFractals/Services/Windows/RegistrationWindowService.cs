using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TAlex.BeautifulFractals.Views;
using TAlex.WPF.Mvvm.Extensions;


namespace TAlex.BeautifulFractals.Services.Windows
{
    public class RegistrationWindowService : IRegistrationWindowService
    {
        public void Show()
        {
            Window window = new RegistrationWindow();
            window.Owner = App.Current.GetActiveWindow();
            window.ShowDialog();
        }
    }
}
