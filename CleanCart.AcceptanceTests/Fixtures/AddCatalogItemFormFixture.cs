using System;
using CleanCart.AcceptanceTests.Helpers;
using OpenQA.Selenium;

namespace CleanCart.AcceptanceTests.Fixtures
{
    class AddCatalogItemFormFixture : CleanCartWebFixture
    {
        public void NavigateToForm()
        {
            Driver.Navigate().GoToUrl(String.Format("http://localhost:{0}/", IISExpressRunner.Port));
        }

        public void FillInItemCode(string itemCode)
        {
            Driver.FindElement(By.Name("itemCode")).SendKeys(itemCode);
        }

        public void FillInTitle(string itemTitle)
        {
            Driver.FindElement(By.Name("itemTitle")).SendKeys(itemTitle);
        }

        public void SubmitForm()
        {
            Driver.FindElement(By.CssSelector("form button")).Click();
        }
    }
}
