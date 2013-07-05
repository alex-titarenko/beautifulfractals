using System;
using NUnit.Framework;
using TAlex.BeautifulFractals.ViewModels;
using TAlex.Common.Environment;
using TAlex.Common.Licensing;
using NSubstitute;
using FluentAssertions;


namespace TAlex.BeautifulFractals.Test.ViewModels
{
    [TestFixture]
    public class AboutViewModelTests
    {
        private AboutViewModel ViewModel;
        private ApplicationInfo ApplicationInfoMock;
        private LicenseBase LicenseBaseMock;


        [SetUp]
        public void SetUp()
        {
            ApplicationInfoMock = Substitute.For<ApplicationInfo>();
            LicenseBaseMock = Substitute.For<LicenseBase>(Substitute.For<ILicenseDataManager>(), Substitute.For<ITrialPeriodDataProvider>());

            ViewModel = new AboutViewModel(ApplicationInfoMock, LicenseBaseMock);
        }


        [TestCase("Title")]
        [TestCase("Beautiful Fractals")]
        public void AboutLogoTitle_Test(string expected)
        {
            //arrange
            ApplicationInfoMock.Title.Returns(expected);

            //action
            var actual = ViewModel.AboutLogoTitle;

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void Version_Test()
        {
            //arrange
            var expected = new Version(1, 0, 0, 5);
            ApplicationInfoMock.Version.Returns(expected);

            //action
            var actual = ViewModel.Version;

            //assert
            actual.Should().Be(expected);
        }
    }
}
