using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.Common.Licensing;

namespace TAlex.BeautifulFractals.Services.Licensing
{
    internal class AppLicense : LicenseBase
    {
        protected override byte[] IV
        {
            get { throw new NotImplementedException(); }
        }

        protected override byte[] SK
        {
            get { throw new NotImplementedException(); }
        }

        protected override List<string> SKH
        {
            get { throw new NotImplementedException(); }
        }


        #region Constructors

        public AppLicense(ILicenseDataManager licenseDataManager, ITrialPeriodDataProvider trialPeriodDataProvider)
            : base(licenseDataManager, trialPeriodDataProvider)
        {
        }

        #endregion
    }
}
