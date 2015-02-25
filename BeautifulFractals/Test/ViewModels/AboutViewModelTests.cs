using System;
using NUnit.Framework;
using TAlex.BeautifulFractals.ViewModels;
using TAlex.Common.Environment;
using TAlex.Common.Licensing;
using NSubstitute;
using FluentAssertions;
using TAlex.BeautifulFractals.Services.Windows;


namespace TAlex.BeautifulFractals.Test.ViewModels
{
    [TestFixture]
    public class AboutViewModelTests
    {
        private AboutViewModel ViewModel;
        private ApplicationInfo ApplicationInfoMock;


        [SetUp]
        public void SetUp()
        {
            ApplicationInfoMock = Substitute.For<ApplicationInfo>();

            ViewModel = new AboutViewModel(ApplicationInfoMock);
        }


        [TestCase("Title")]
        [TestCase("Beautiful Fractals")]
        public void AboutLogoTitle_Test(string expected)
        {
            //arrange
            ApplicationInfoMock.Title.Returns(expected);

            //action
            string actual = ViewModel.AboutLogoTitle;

            //assert
            actual.Should().Be(expected);
        }

        [TestCase(1, 0, 0, 5)]
        public void Version_Test(int major, int minor, int build, int revision)
        {
            //arrange
            var expected = new Version(major, minor, build, revision);
            ApplicationInfoMock.Version.Returns(expected);

            //action
            Version actual = ViewModel.Version;

            //assert
            actual.Should().Be(expected);
        }

        [Test]
        public void EmailAddress_Test()
        {
            //action
            string actual = ViewModel.EmailAddress; 

            //assert
            actual.Should().Be(TAlex.BeautifulFractals.Properties.Resources.SupportEmail);
        }

        [Test]
        public void HomepageUrl_Test()
        {
            //action
            string actual = ViewModel.HomepageUrl;

            //assert
            actual.Should().Be(TAlex.BeautifulFractals.Properties.Resources.HomepageUrl);
        }

        [TestCase("Some Copyright")]
        public void Copyright_Test(string expected)
        {
            //arrange
            ApplicationInfoMock.CopyrightDisplayText.Returns(expected);

            //action
            string actual = ViewModel.Copyright;

            //assert
            actual.Should().Be(expected);
        }
    }
}
