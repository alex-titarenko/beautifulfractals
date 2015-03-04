using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.Infrastructure;
using TAlex.BeautifulFractals.Services;
using TAlex.BeautifulFractals.Services.Windows;
using TAlex.Mvvm.Services;
using TAlex.Common.Models;
using TAlex.Common.Extensions;


namespace TAlex.BeautifulFractals.Locators.Modules
{
    public class BaseServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<AssemblyInfo>().ToConstant(typeof(BaseServicesNinjectModule).Assembly.GetAssemblyInfo()).InSingletonScope();

            Bind<IMessageService>().To<MessageService>();
            Bind<IApplicationService>().To<ApplicationService>();
            Bind<IFontChooserDialogService>().To<FontChooserDialogService>();
            Bind<IPreviewDialogService>().To<PreviewDialogService>();

            Bind<IAppSettings>().ToMethod(x => Properties.Settings.Default).InSingletonScope();
            Bind<IFractalsManager>().To<FractalsManager>().InSingletonScope();
            Bind<ICollectionViewFactory>().To<CollectionViewFactory>().InSingletonScope();
        }
    }
}
