using Ninject;
using Ninject.Modules;
using System;
using System.Text;
using TAlex.BeautifulFractals.Locators.Modules;
using TAlex.BeautifulFractals.ViewModels;


namespace TAlex.BeautifulFractals.Locators
{
    public class ViewModelLocator
    {
        private IKernel _kernel;


        public ViewModelLocator()
        {
            _kernel = new StandardKernel(
                new BaseServicesNinjectModule(),
                new AppLicenseNinjectModule(),
                new ViewModelNinjectModule());
        }


        //public MainWindowViewModel MainWindowViewModel
        //{
        //    get
        //    {
        //        return _kernel.Get<MainWindowViewModel>();
        //    }
        //}

        public AboutViewModel AboutViewModel
        {
            get
            {
                return _kernel.Get<AboutViewModel>();
            }
        }

        public RegistrationViewModel RegistrationViewModel
        {
            get
            {
                return _kernel.Get<RegistrationViewModel>();
            }
        }
    }
}
