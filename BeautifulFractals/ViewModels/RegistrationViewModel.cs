using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TAlex.Common.Licensing;
using TAlex.WPF.Mvvm.Commands;


namespace TAlex.BeautifulFractals.ViewModels
{
    public class RegistrationViewModel
    {
        #region Fields

        protected readonly ILicenseDataManager LicenseDataManager;

        #endregion

        #region Properties

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string LicenseName { get; set; }

        [Required]
        public string LicenseKey { get; set; }

        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constructors

        public RegistrationViewModel(ILicenseDataManager licenseDataManager)
        {
            LicenseDataManager = licenseDataManager;

            RegisterCommand = new RelayCommand(RegisterCommandExecute, RegisterCommandCanExecute);
        }

        #endregion

        #region Methods

        private void RegisterCommandExecute()
        {
            string lin = LicenseName.Trim();
            string lik = LicenseKey.Trim();

            //MessageBox.Show(this, "Please restart this program to verify your license data.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            LicenseDataManager.Save(new LicenseData { LicenseName = lin, LicenseKey = lik });
        }

        private bool RegisterCommandCanExecute()
        {
            return !String.IsNullOrWhiteSpace(LicenseName) && !String.IsNullOrWhiteSpace(LicenseKey);
        }

        #endregion
    }
}
