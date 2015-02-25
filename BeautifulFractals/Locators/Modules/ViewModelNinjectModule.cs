using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.ViewModels;


namespace TAlex.BeautifulFractals.Locators.Modules
{
    public class ViewModelNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<PreferencesWindowViewModel>().ToSelf();
            Bind<AboutViewModel>().ToSelf().InSingletonScope();
        }
    }
}
