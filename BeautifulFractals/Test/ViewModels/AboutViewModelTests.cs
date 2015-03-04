using System;
using NUnit.Framework;
using TAlex.BeautifulFractals.ViewModels;
using NSubstitute;
using FluentAssertions;
using TAlex.BeautifulFractals.Services.Windows;
using TAlex.Common.Models;


namespace TAlex.BeautifulFractals.Test.ViewModels
{
    [TestFixture]
    public class AboutViewModelTests
    {
        private AboutViewModel ViewModel;
        private AssemblyInfo ApplicationInfoMock;


        [SetUp]
        public void SetUp()
        {
            ApplicationInfoMock = Substitute.For<AssemblyInfo>(typeof(AboutViewModel).Assembly);

            ViewModel = new AboutViewModel(ApplicationInfoMock);
        }


        [Test]
        public void AssemblyInfo_Test()
        {
            //action
            var actual = ViewModel.AssemblyInfo;

            //assert
            actual.Should().Be(ApplicationInfoMock);
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
    }
}
