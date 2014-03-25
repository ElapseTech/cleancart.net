using System;
using OpenQA.Selenium;

namespace CleanCart.AcceptanceTests.Fixtures
{
    class AddShopCatalogFixture : CleanCartWebFixture
    {
        public void NavigateToForm()
        {
            Driver.Navigate().GoToUrl(CreateAbsoluteUrl());
        }

        public void FillInItemCode(string itemCode)
        {
            Driver.FindElement(By.Name("CodeText")).SendKeys(itemCode);
        }

        public void FillInTitle(string itemTitle)
        {
            Driver.FindElement(By.Name("Title")).SendKeys(itemTitle);
        }

        public void SubmitForm()
        {
            Driver.FindElement(By.CssSelector("form")).Submit();
        }

        public bool CheckItemShown(string itemTitle)
        {
            return Driver.FindElement(By.CssSelector("#catalog-list")).Text.Contains(itemTitle);
        }
    }
}
