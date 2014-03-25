using CleanCart.AcceptanceTests.Fixtures;
using CleanCart.ApplicationServices;
using CleanCart.ApplicationServices.Assemblers;
using CleanCart.Controllers;
using CleanCart.Domain;
using CleanCart.Persistence.FakeInMemory;
using CleanCart.Persistence.FakeInMemory.Entities;
using CleanCart.ViewModels.ShopCatalog;
using FluentAssertions;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace CleanCart.AcceptanceTests.Steps
{
    [Binding]
    class ShopCatalogEnd2EndSteps
    {
        private const string NoTitle = "";
        private const string NoCode = "";
        private readonly String[] _titles = { "ITEM 1", "item 2" };

        private readonly Fixture _fixture = new Fixture();
        private AddShopCatalogFixture _addShopCatalogFixture;

        private string _lastAddedItemTitle;


        [AfterScenario]
        public void CloseBrowser()
        {
            if (_addShopCatalogFixture != null)
            {
                _addShopCatalogFixture.CloseBrowser();
            }
        }

        [Given(@"The shop catalog site")]
        public void GivenTheShopCatalogSite()
        {
            _addShopCatalogFixture = new AddShopCatalogFixture();
        }

        [When(@"I add a new item")]
        public void WhenIAddANewItem()
        {
            var itemCode = CreateItemCode();
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(itemCode, itemTitle);
            _lastAddedItemTitle = itemTitle;
        }

        [When(@"I add a new item with no title")]
        public void WhenIAddANewItemWithNoTitle()
        {
            var itemCode = CreateItemCode();
            FillInCatalogItemFormAndSubmitIt(itemCode, NoTitle);
        }

        [When(@"I add a new item with no item code")]
        public void WhenIAddANewItemWithNoItemCode()
        {
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(NoCode, itemTitle);
        }

        [When(@"I add a new item with the code '(.*)'")]
        public void WhenIAddANewItemWithTheCode(string itemCode)
        {
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(itemCode, itemTitle);
            _lastAddedItemTitle = itemTitle;
        }

        private void FillInCatalogItemFormAndSubmitIt(string itemCode, string itemTitle)
        {
            _addShopCatalogFixture.NavigateToForm();
            _addShopCatalogFixture.FillInItemCode(itemCode);
            _addShopCatalogFixture.FillInTitle(itemTitle);
            _addShopCatalogFixture.SubmitForm();
        }



        [Then(@"the item is shown in the catalog")]
        public void ThenTheItemIsAddedToTheCatalog()
        {
            var codeShown = _addShopCatalogFixture.CheckItemShown(_lastAddedItemTitle);
            codeShown.Should().BeTrue();
        }

        [Then(@"an error is reported")]
        public void ThenAnErrorIsReported()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I can add another item")]
        public void ThenICanAddAnotherItem()
        {
            ScenarioContext.Current.Pending();
        }



        private string CreateItemCode()
        {
            return _fixture.Create<String>("Code");
        }

        private string CreateItemTitle()
        {
            return _fixture.Create<String>("Title");
        }

    }
}
