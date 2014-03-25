using System;
using System.Security.Policy;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CleanCart.AcceptanceTests.Fixtures
{
    class CleanCartWebFixture
    {
        private const string WebsiteScheme = "http";
        private const string WebsiteHost = "localhost";
        private const string WebsiteBasePath = "CleanCart";
        private const string WebsiteRoot = "";

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

        protected Uri CreateAbsoluteUrl(string relativePath = WebsiteRoot)
        {
            return new UriBuilder()
            {
                Scheme = WebsiteScheme,
                Host = WebsiteHost,
                Path = string.Format("{0}/{1}", WebsiteBasePath, relativePath)
            }.Uri;
        }
    }
}