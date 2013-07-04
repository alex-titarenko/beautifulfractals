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
        #region Fields

        private static readonly byte[] _IV = new byte[]
        {
            34, 0, 1, 138, 16, 15, 2, 247
        };

        private static readonly byte[] _SK = new byte[]
        {
            11, 11, 2, 243, 13, 5, 118, 39
        };

        #endregion

        #region Properties

        protected override byte[] IV
        {
            get
            {
                return _IV;
            }
        }

        protected override byte[] SK
        {
            get
            {
                return _SK;
            }
        }

        protected override List<string> SKH
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


        #region Constructors

        public AppLicense(ILicenseDataManager licenseDataManager, ITrialPeriodDataProvider trialPeriodDataProvider)
            : base(licenseDataManager, trialPeriodDataProvider)
        {
        }

        #endregion
    }
}
