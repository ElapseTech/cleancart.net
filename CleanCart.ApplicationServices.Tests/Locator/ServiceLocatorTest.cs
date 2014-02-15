
using CleanCart.ApplicationServices.Locator;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCart.ApplicationServices.Tests.Locator
{
    [TestClass]
    class ServiceLocatorTest
    {
        [TestMethod, ExpectedException(typeof(ServiceNotRegisteredException))]
        public void ResolvingAServiceThatIsNotRegisteredThrowsAnException()
        {
            var locator = new ServiceLocator();

            locator.Resolve<ITestService>();
        }

        [TestMethod, ExpectedException(typeof(CannotRegisterServiceTwiceException))]
        public void CannotRegisterSameServiceTwice()
        {
            var locator = new ServiceLocator();
            var firstImplementation = new TestImplementation();
            var secondImplementation = new TestImplementation();

            locator.Register<ITestService>(firstImplementation);
            locator.Register<ITestService>(secondImplementation);
        }

        [TestMethod]
        public void CanResolveARegisteredComponent()
        {
            var locator = new ServiceLocator();
            var implementation = new TestImplementation();

            locator.Register<ITestService>(implementation);

            locator.Resolve<ITestService>().Should().Be(implementation);
        }
    }

    internal interface ITestService
    {
    }

    internal class TestImplementation : ITestService
    {
    }
}
