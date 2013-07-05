using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.Common.Licensing;

namespace TAlex.BeautifulFractals.Services.Licensing
{
    internal class AppLicenseDataManager : LicFileLicenseDataManager
    {
        protected override byte[] IV
        {
            get { return new byte[] { 114, 254, 1, 2, 6, 34, 105, 18 }; }
        }

        protected override byte[] SK
        {
            get { return new byte[] { 11, 11, 103, 104, 15, 2, 0, 6 }; }
        }
    }
}
