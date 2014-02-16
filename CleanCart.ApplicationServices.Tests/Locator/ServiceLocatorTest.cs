
using System.Runtime.InteropServices;
using CleanCart.ApplicationServices.Locator;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CleanCart.ApplicationServices.Tests.Locator
{
    [TestClass]
    class ServiceLocatorTest
    {
        private TestImplementation _theImplementation;
        private ServiceLocator _locator;

        [TestInitialize]
        public void ResetServiceLocator()
        {
            _theImplementation = new TestImplementation();
            _locator = new ServiceLocator();
        }



        [TestMethod]
        public void CanResolveARegisteredComponent()
        {
            _locator.Register<ITestService>(_theImplementation);
            _locator.Resolve<ITestService>().Should().Be(_theImplementation);
        }

        [TestMethod, ExpectedException(typeof(CannotRegisterServiceTwiceException))]
        public void CannotRegisterSameServiceTwice()
        {
            _locator.Register<ITestService>(_theImplementation);
            _locator.Register<ITestService>(_theImplementation);
        }

        [TestMethod, ExpectedException(typeof(ServiceNotRegisteredException))]
        public void ResolvingAServiceThatIsNotRegisteredThrowsAnException()
        {
            _locator.Resolve<ITestService>();
        }

        [TestMethod]
        public void CanCheckIfAServiceIsRegistered()
        {
            _locator.Register<ITestService>(_theImplementation);
            _locator.IsServiceDefined(typeof(ITestService)).Should().BeTrue();
        }

        [TestMethod]
        public void UnregisteredServiceShouldNotBeDefined()
        {
            _locator.IsServiceDefined(typeof(ITestService)).Should().BeFalse();
        }


    }

    internal interface ITestService
    {
    }

    internal class TestImplementation : ITestService
    {
    }
}
