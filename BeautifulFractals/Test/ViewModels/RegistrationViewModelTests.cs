using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAlex.BeautifulFractals.Services.Windows;
using TAlex.BeautifulFractals.ViewModels;
using TAlex.Common.Licensing;
using TAlex.WPF.Mvvm.Services;
using FluentAssertions;


namespace TAlex.BeautifulFractals.Test.ViewModels
{
    [TestFixture]
    public class RegistrationViewModelTests
    {
        protected RegistrationViewModel ViewModel;
        protected ILicenseDataManager LicenseDataManager;
        protected IMessageService MessageService;
        protected IApplicationService ApplicationService;


        [SetUp]
        public virtual void SetUp()
        {
            LicenseDataManager = Substitute.For<ILicenseDataManager>();
            MessageService = Substitute.For<IMessageService>();
            ApplicationService = Substitute.For<IApplicationService>();

            ViewModel = new RegistrationViewModel(LicenseDataManager, MessageService, ApplicationService)
            {
                LicenseKey = "some key",
                LicenseName = "some name"
            };
        }
        

        [Test]
        public void RegisterCommand_SaveLicense()
        {
            //action
            ViewModel.RegisterCommand.Execute(null);

            //assert
            LicenseDataManager.Received().Save(Arg.Is<LicenseData>(x => LicenseDataPredicate(x, ViewModel.LicenseName, ViewModel.LicenseKey)));
        }

        [Test]
        public void RegisterCommand_ShowMessage()
        {
            //action
            ViewModel.RegisterCommand.Execute(null);

            //assert
            MessageService.Received().ShowInformation(TAlex.BeautifulFractals.Properties.Resources.locPleaseRestartToVerifyLicense,
                TAlex.BeautifulFractals.Properties.Resources.locInformationMessageCaption);         
        }

        [Test]
        public void RegisterCommand_Shutdown()
        {
            //action
            ViewModel.RegisterCommand.Execute(null);

            //assert
            ApplicationService.Received().Shutdown();
        }


        [TestCase("Some name", "Some key", true)]
        [TestCase("Some name", "", false)]
        [TestCase("", "Some key", false)]
        [TestCase("", "", false)]
        public void RegisterCommandCanExecute(string licenseName, string licenseKey, bool expected)
        {
            //arrange
            ViewModel.LicenseName = licenseName;
            ViewModel.LicenseKey = licenseKey;

            //action
            bool actual = ViewModel.RegisterCommand.CanExecute(null);

            //assert
            actual.Should().Be(expected);
        }

        #region Helpers

        private bool LicenseDataPredicate(LicenseData data1, string licenseName, string licenseKey)
        {
            return data1.LicenseName == licenseName && data1.LicenseKey == licenseKey;
        }

        #endregion
    }
}
