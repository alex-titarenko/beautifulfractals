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

        private static readonly List<string> _SKH = new List<string>
        {
            "Q0EwSwQvRccVkhNj1bC9Y8vbNlN8x2K8oJYUBYa9udt+nVnbIBLk/krYSnP6D+zUUFHlyGnI9kZJXcAqT3IQEw==",
	        "LX5L/y5XxHUZRT64NJxzk47AfLmW/sKAvyVggzdex/lxlvdwrnOPwGZe4+33Lf3VlCaKyfc0oPMe8QDJ1kOmJg==",
	        "K4Is81iQVjcSjv8lEk2ku2OlNKcxSPzkw33c+R7bSA6Ulycon3fScMPfhBeO2PuZw5MRLlCLV798LXQuInyIgA==",
	        "L0JNfNAgunD/eV8SBZupo1GzEFy+D2s8tx3XF9iKddwyoucr+QXXO7g7Zj1tqW9TU1Y0eNAw07nNWEoarqTHdg==",
	        "nqW6lrLsld3q4aFdHGeilduuwbtyyySpAYkUYTxW95mgJ5yn6iLidVZHfa67bMBvDAXYNF4r+nAqxck3oY004A==",
	        "Pp2V6UC29f90ZlLBI1QE/kbB4qjHlo7d64vGZgnXYCIlnBq44GI2vqLDIInKDt4w38iM38IkB/P0HvwgV+DZDg==",
	        "XPw+JzyeI/sJJx4QTtTsMr/aEE4GDFu8p9+JIVQ6x9zVrxODzbdwxxO/HG4ITxy4USwAZUVH8AmElXidrOSkNA==",
	        "ugVMZ6GMvCjEN2gJcX3LhwrVkySw3F0Fm0/fvvI/n8xxs6FiwWIYlhkUdTE6odisv2EaiG3LbR8Xq+DaE3S1zA==",
	        "0xeOReMwLeKk2+jTSu9wt7vn2lhdRbIkCUi9SZAaKG2zAa6+RM+NDb5i2uEfz9LncI+LuFqPPlz3pYV0KKJsrQ==",
	        "zl1apoyrcXd2LFJuMscf/5h9k4XJLB0Sowd3QtCw+J3N1iDyzTeWxgQGGm6t7f6DQ8h3jLyP3jukMTCPaSykiw==",
	        "0xuFGlhEUMaDQVtKQEGSVH9L1oz5mNBhw4XNZRNswcqt3hPsOvEtGArKmEBu6m6Goj3utjdtDc8V//zC/UtvLA==",
	        "h6VoBUlJAZzizSomFpJeuSWQV7Hda5SO/slB9lhvUUHZf11lwGhzOPRVWOSq2Q8d77Zvn30g0irOCmg8RPsJ9g==",
	        "edMD/CDJ4zUN7/+ICRobf9f7poN1HK/dR+66iULFbKyViXIUCp8bbA+foExZwvqNdtX6Muu4WqC3xp5MfLlEUg==",
	        "ZiwaQqDl93G8a6Q54ELJ4sLeHKazqFuf274a551rkRY8970neU4XLnnunVyP5d+UXxyd7AxzWQsX4M7EVU3Thw==",
	        "OvOZaBVDyLnujPspuf3uWgdx/RwjUdbh3jWC/WKfx7bsTd+TBtJcUl54GdO4xZBpRbybl/gLRAWb8hz7cRxBGg==",
	        "9HhXfoKP7lxZaKSKDgl0uWOIhRVhJaGJYujPwY+f5/9nSC8nMklfAhDcIdlDFclzjD1jfMJr2STPVJ7h1BhtYQ==",
	        "iUHH50mXW0Gwf3Qw4HWo7G+4Fc8HUrBlF24HHXHZfjf71V2vDhBzKKNUmrQJCUDpC7i0EHXKxSYEj5C8LmH/2g==",
	        "ZX5BtKvo0VoO2GQFG+4EcLTCcieP0S3b2T9Cp+sRf5zeXv2Qo1lBg75siD0W3dPNwLx6ad/N+aeDyE7SXnIrsg==",
	        "IX+dJflyHiJQZcjaZAzhr/0uukR+dLxWKP2vZC2LgTwR/l3HJBVQh5z1NSqeyjOTS2f/k0d0bISDpxqQESAtbQ==",
	        "rh695oqhE2r7JlQIiJsWOetgq6p8k9C0khOJ3smgGIgE7cm0SLL8dbiz/c/oBHy7nJ0D6HZLu9ArN9omRJZ+8Q=="
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
            get
            {
                return _SKH;
            }
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
