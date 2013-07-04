﻿using System;
using NUnit.Framework;
using TAlex.BeautifulFractals.ViewModels;
using TAlex.Common.Environment;
using TAlex.Common.Licensing;
using NSubstitute;



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
            LicenseBaseMock = Substitute.For<LicenseBase>();

            ViewModel = new AboutViewModel(ApplicationInfoMock, LicenseBaseMock);
        }


        [Test]
        public void TestMethod1()
        {
        }
    }
}