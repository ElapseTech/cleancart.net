using CleanCart.AcceptanceTests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CleanCart.AcceptanceTests.Fixtures
{
    class CleanCartWebFixture
    {
        protected readonly IWebDriver Driver;

        public CleanCartWebFixture() : this(new FirefoxDriver()) { }

        public CleanCartWebFixture(IWebDriver driver)
        {
            Driver = driver;
        }

        public void CloseBrowser()
        {
            Driver.Close();
        }
    }
}