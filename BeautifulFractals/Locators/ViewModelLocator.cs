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
                new ViewModelNinjectModule());
        }

        #endregion

        #region Properties

        public PreferencesWindowViewModel PreferencesWindowViewModel
        {
            get
            {
                return _kernel.Get<PreferencesWindowViewModel>();
            }
        }

        public AboutViewModel AboutViewModel
        {
            get
            {
                return _kernel.Get<AboutViewModel>();
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
