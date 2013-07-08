using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.Services.Windows;
using TAlex.Common.Environment;
using TAlex.WPF.Mvvm.Services;

namespace TAlex.BeautifulFractals.Locators.Modules
{
    public class BaseServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ApplicationInfo>().ToConstant(ApplicationInfo.Current);

            Bind<IMessageService>().To<MessageService>();
            Bind<IApplicationService>().To<ApplicationService>();
        }
    }
}
