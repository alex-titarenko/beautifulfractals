﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using TAlex.Common.Environment;
using TAlex.Common.Licensing;


namespace TAlex.BeautifulFractals.ViewModels
{
    public class AboutViewModel
    {
        #region Fields

        protected readonly ApplicationInfo ApplicationInfo;
        protected readonly LicenseBase AppLicense;

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

        #region Constructors

        public AboutViewModel(ApplicationInfo applicationInfo, LicenseBase appLicense)
        {
            ApplicationInfo = applicationInfo;
        }

        #endregion
    }
}