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
        private readonly String[] _titles = {"ITEM 1", "item 2"};

        private readonly Fixture _fixture = new Fixture();

        private InMemoryCatalogItemRepository _catalogItemRepository;
        private readonly CatalogItemAssembler _catalogItemAssembler = new CatalogItemAssembler();
        private ViewResult _shopCatalogViewResult;
        private ItemCode _lastAddedItemCode;

        [Given(@"A shop catalog")]
        public void GivenAShopACatalog()
        {
            _catalogItemRepository = new InMemoryCatalogItemRepository();
        }

        [Given(@"Some items in the catalog")]
        public void GivenSomeItemsInTheCatalog()
        {
            _catalogItemRepository.Persist(new InMemoryCatalogItem(new ItemCode("I1"), _titles[0]));
            _catalogItemRepository.Persist(new InMemoryCatalogItem(new ItemCode("I2"), _titles[1]));
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
            var itemTitle = _fixture.Create<String>();
            WhenIAddANewItemWithTheTitle(itemTitle);
        }

        [When(@"I add a new item with the title '(.*)'")]
        public void WhenIAddANewItemWithTheTitle(string itemTitle)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"all items' title are shown")]
        public void ThenAllItemsTitleAreShown()
        {
            var shopCatalogViewModel = _shopCatalogViewResult.Model as ShopCatalogViewModel;
            shopCatalogViewModel.CatalogItems.Select(x => x.Title).Should().BeEquivalentTo(_titles);
        }


        [Then(@"the item is added to the catalog")]
        public void ThenTheItemIsAddedToTheCatalog()
        {
            _catalogItemRepository.FindByItemCode(_lastAddedItemCode);
        }


        [Then(@"I can add another item")]
        public void ThenICanAddAnotherItem()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
