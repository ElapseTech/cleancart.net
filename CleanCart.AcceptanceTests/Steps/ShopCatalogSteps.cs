﻿using CleanCart.AcceptanceTests.Fixtures;
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
    class ShopCatalogSteps
    {
        private const string NoTitle = "";
        private const string NoCode = "";
        private readonly String[] _titles = { "ITEM 1", "item 2" };

        private readonly Fixture _fixture = new Fixture();

        private InMemoryCatalogItemRepository _catalogItemRepository;
        private readonly CatalogItemAssembler _catalogItemAssembler = new CatalogItemAssembler();
        private ViewResult _shopCatalogViewResult;
        private CatalogItemCode _lastAddedCatalogItemCode;

        private AddCatalogItemFormFixture _addCatalogItemFormFixture;

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

        [Given(@"A shop catalog")]
        public void GivenAShopACatalog()
        {
            _catalogItemRepository = new InMemoryCatalogItemRepository();
        }

        [Given(@"Some items in the catalog")]
        public void GivenSomeItemsInTheCatalog()
        {
            PersistANewItemToCatalog(new CatalogItemCode("I1"), _titles[0]);
            PersistANewItemToCatalog(new CatalogItemCode("I2"), _titles[1]);
        }

        [Given(@"an item with code '(.*)' in the catalog")]
        public void GivenAnItemWithCodeInTheCatalog(string itemCode)
        {
            var itemTitle = CreateItemTitle();
            PersistANewItemToCatalog(new CatalogItemCode(itemCode), itemTitle);
        }

        [Given(@"an item with code '(.*)' and the title '(.*)' in the catalog")]
        public void GivenAnItemWithCodeAndTitleInTheCatalog(string itemCode, string itemTitle)
        {
            PersistANewItemToCatalog(new CatalogItemCode(itemCode), itemTitle);
        }


        [When(@"I visit the catalog")]
        public void WhenIVisitTheCatalog()
        {
            var shopCatalogService = new ShopCatalogService(_catalogItemRepository, _catalogItemAssembler);
            var shopCatalogController = new ShopCatalogController(shopCatalogService);
            _shopCatalogViewResult = shopCatalogController.Index();
        }

        [When(@"I add a new item")]
        public void WhenIAddANewItem()
        {
            var itemCode = CreateItemCode();
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(itemCode, itemTitle);
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
        }

        private void FillInCatalogItemFormAndSubmitIt(string itemCode, string itemTitle)
        {
            _addCatalogItemFormFixture.NavigateToForm();
            _addCatalogItemFormFixture.FillInItemCode(itemCode);
            _addCatalogItemFormFixture.FillInTitle(itemTitle);
            _addCatalogItemFormFixture.SubmitForm();
        }



        [Then(@"all items' title are shown")]
        public void ThenAllItemsTitleAreShown()
        {
            var shopCatalogViewModel = _shopCatalogViewResult.Model as ShopCatalogViewModel;
            shopCatalogViewModel.CatalogItems.Select(x => x.Title).Should().BeEquivalentTo(_titles);
        }


        [Then(@"an error is reported")]
        public void ThenAnErrorIsReported()
        {
            ScenarioContext.Current.Pending();
        }


        [Then(@"the item is added to the catalog")]
        public void ThenTheItemIsAddedToTheCatalog()
        {
            _catalogItemRepository.FindByItemCode(_lastAddedCatalogItemCode);
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

        private void PersistANewItemToCatalog(CatalogItemCode catalogItemCode, string itemTitle)
        {
            var item = new InMemoryCatalogItem(catalogItemCode, itemTitle);
            _catalogItemRepository.Persist(item);
        }


    }
}
