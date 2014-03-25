using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CleanCart.AcceptanceTests.Fixtures
{
    class CleanCartWebFixture
    {
        private readonly Lazy<IWebDriver> _driver;

        protected IWebDriver Driver
        {
            get { return _driver.Value; }
        }

        public CleanCartWebFixture() : this(new Lazy<IWebDriver>(() => new FirefoxDriver()))
        {
        }

        public CleanCartWebFixture(Lazy<IWebDriver> driver)
        {
            _driver = driver;
        }

        public void CloseBrowser()
        {
            Driver.Close();
        }
    }
}