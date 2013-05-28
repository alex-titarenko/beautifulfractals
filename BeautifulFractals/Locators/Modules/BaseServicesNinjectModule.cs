using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.Common.Environment;

namespace TAlex.BeautifulFractals.Locators.Modules
{
    public class BaseServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ApplicationInfo>().ToConstant(ApplicationInfo.Current);
        }
    }
}
