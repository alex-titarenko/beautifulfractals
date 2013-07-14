using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using TAlex.BeautifulFractals.Services.Windows;
using TAlex.Common.Environment;
using TAlex.Common.Licensing;
using TAlex.WPF.Mvvm.Commands;


namespace TAlex.BeautifulFractals.ViewModels
{
    public class AboutViewModel
    {
        #region Fields

        protected readonly ApplicationInfo ApplicationInfo;
        protected readonly LicenseBase AppLicense;
        protected readonly IRegistrationWindowService RegistrationWindowService;

        #endregion

        #region Properties

        public virtual string AboutLogoTitle
        {
            get
            {
                return ApplicationInfo.Title;
            }
        }

        public virtual Version Version
        {
            get
            {
                return ApplicationInfo.Version;
            }
        }

        /// <summary>
        /// Gets the email support title for this product.
        /// </summary>
        public virtual string EmailTitle
        {
            get
            {
                return EmailAddress.Replace("mailto:", String.Empty);
            }
        }

        /// <summary>
        /// Gets the email support for this product.
        /// </summary>
        public virtual string EmailAddress
        {
            get
            {
                return Properties.Resources.SupportEmail;
            }
        }

        /// <summary>
        /// Gets the homepage title of this product.
        /// </summary>
        public virtual string HomepageTitle
        {
            get
            {
                return HomepageUrl.Replace("http://", String.Empty);
            }
        }

        /// <summary>
        /// Gets the homepage url of this product.
        /// </summary>
        public virtual string HomepageUrl
        {
            get
            {
                return Properties.Resources.HomepageUrl;
            }
        }

        public virtual string Copyright
        {
            get
            {
                return ApplicationInfo.CopyrightDisplayText;
            }
        }


        public virtual string LicenseName
        {
            get
            {
                return AppLicense.LicenseName;
            }
        }

        public virtual bool LicenseInfoVisibility
        {
            get
            {
                return AppLicense.IsLicensed;
            }
        }

        public bool UnregisteredTextVisibility
        {
            get
            {
                return !LicenseInfoVisibility;
            }
        }

        #endregion

        #region Commands

        public ICommand OpenRegistrationDialogCommand { get; set; }

        #endregion

        #region Constructors

        public AboutViewModel(ApplicationInfo applicationInfo, LicenseBase appLicense, IRegistrationWindowService registrationWindowService)
        {
            ApplicationInfo = applicationInfo;
            AppLicense = appLicense;
            RegistrationWindowService = registrationWindowService;

            InitCommands();
        }

        #endregion

        #region Methods

        private void InitCommands()
        {
            OpenRegistrationDialogCommand = new RelayCommand(OpenRegistrationDialogCommandExecute);
        }

        private void OpenRegistrationDialogCommandExecute()
        {
            RegistrationWindowService.Show();
        }

        #endregion
    }
}
