using System;
using CleanCart.AcceptanceTests.Helpers;
using CleanCart.ApplicationServices.Dto;
using OpenQA.Selenium;

namespace CleanCart.AcceptanceTests.Fixtures
{
    class AddShopCatalogFixture : CleanCartWebFixture
    {
        private readonly FormHelper<CatalogItemDTO> _formHelper = new FormHelper<CatalogItemDTO>();
 
        public void NavigateToForm()
        {
            Driver.Navigate().GoToUrl(CreateAbsoluteUrl());
        }

        public void FillInItemCode(string itemCode)
        {
            Driver.FindElement(By.Name(_formHelper.FieldName(x => x.CodeText))).SendKeys(itemCode);
        }

        public void FillInTitle(string itemTitle)
        {
            Driver.FindElement(By.Name(_formHelper.FieldName(x => x.Title))).SendKeys(itemTitle);
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
