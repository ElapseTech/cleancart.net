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
        private AddCatalogItemFormFixture _addCatalogItemFormFixture;

        private CatalogItemCode _lastAddedCatalogItemCode;


        [BeforeScenario]
        public void CreateFixture()
        {
            _addCatalogItemFormFixture = new AddCatalogItemFormFixture();
        }

        [AfterScenario]
        public void CloseBrowser()
        {
            _addCatalogItemFormFixture.CloseBrowser();
        }

        [Given(@"The shop catalog site")]
        public void GivenTheShopCatalogSite()
        {
        }

        [When(@"I add a new item")]
        public void WhenIAddANewItem()
        {
            var itemCode = CreateItemCode();
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(itemCode, itemTitle);
            _lastAddedCatalogItemCode = new CatalogItemCode(itemCode);
        }

        [When(@"I add a new item with no title")]
        public void WhenIAddANewItemWithNoTitle()
        {
            var itemCode = CreateItemCode();
            FillInCatalogItemFormAndSubmitIt(itemCode, NoTitle);
            _lastAddedCatalogItemCode = new CatalogItemCode(itemCode);
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
        }

        private void FillInCatalogItemFormAndSubmitIt(string itemCode, string itemTitle)
        {
            _addCatalogItemFormFixture.NavigateToForm();
            _addCatalogItemFormFixture.FillInItemCode(itemCode);
            _addCatalogItemFormFixture.FillInTitle(itemTitle);
            _addCatalogItemFormFixture.SubmitForm();
        }



        [Then(@"an error is reported")]
        public void ThenAnErrorIsReported()
        {
            ScenarioContext.Current.Pending();
        }


        [Then(@"the item is shown in the catalog")]
        public void ThenTheItemIsAddedToTheCatalog()
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
