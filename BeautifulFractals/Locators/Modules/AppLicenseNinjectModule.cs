using Ninject.Modules;
using System;
using System.Collections.Generic;
using TAlex.BeautifulFractals.Services.Licensing;
using TAlex.Common.Licensing;


namespace TAlex.BeautifulFractals.Locators.Modules
{
    public class AppLicenseNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILicenseDataManager>().To<AppLicenseDataManager>().InSingletonScope();
            Bind<ITrialPeriodDataProvider>().To<AppTrialPeriodDataProvider>().InSingletonScope();
            Bind<LicenseBase>().To<AppLicense>().InSingletonScope();
        }
    }
}
