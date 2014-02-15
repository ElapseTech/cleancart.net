
using CleanCart.ApplicationServices.Locator;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCart.ApplicationServices.Tests.Locator
{
    [TestClass]
    class ServiceLocatorTest
    {

        [TestInitialize]
        public void ResetServiceLocator()
        {
            ServiceLocator.Reset();
        }

        [TestMethod, ExpectedException(typeof(ServiceNotRegisteredException))]
        public void ResolvingAServiceThatIsNotRegisteredThrowsAnException()
        {
            ServiceLocator.Resolve<ITestService>();
        }

        [TestMethod, ExpectedException(typeof(CannotRegisterServiceTwiceException))]
        public void CannotRegisterSameServiceTwice()
        {
            var firstImplementation = new TestImplementation();
            var secondImplementation = new TestImplementation();

            ServiceLocator.Register<ITestService>(firstImplementation);
            ServiceLocator.Register<ITestService>(secondImplementation);
        }

        [TestMethod]
        public void CanResolveARegisteredComponent()
        {
            var implementation = new TestImplementation();

            ServiceLocator.Register<ITestService>(implementation);

            ServiceLocator.Resolve<ITestService>().Should().Be(implementation);
        }
    }

    internal interface ITestService
    {
    }

    internal class TestImplementation : ITestService
    {
    }
}
