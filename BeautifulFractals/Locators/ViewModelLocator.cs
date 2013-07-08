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
        #region Fields

        private IKernel _kernel;

        #endregion

        #region Constructors

        public ViewModelLocator()
        {
            _kernel = new StandardKernel(
                new BaseServicesNinjectModule(),
                new AppLicenseNinjectModule(),
                new ViewModelNinjectModule());
        }

        #endregion


        //public MainWindowViewModel MainWindowViewModel
        //{
        //    get
        //    {
        //        return _kernel.Get<MainWindowViewModel>();
        //    }
        //}

        #region Properties

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

        #endregion

        #region Methods

        #region Methods

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }

        #endregion

        #endregion
    }
}
