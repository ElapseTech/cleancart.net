using CleanCart.ApplicationServices;
using CleanCart.ApplicationServices.Assemblers;
using CleanCart.Controllers;
using CleanCart.Persistence.FakeInMemory;
using CleanCart.Persistence.FakeInMemory.Entities;
using CleanCart.ViewModels.ShopCatalog;
using FluentAssertions;
using System;
using System.Linq;
using System.Web.Mvc;
using TechTalk.SpecFlow;

namespace CleanCart.AcceptanceTests.Steps
{
    [Binding]
    class ShopCatalogSteps
    {
        private readonly String[] _titles = {"ITEM 1", "item 2"};

        private MemoryCatalogItemRepository _catalogItemRepository;
        private readonly CatalogItemAssembler _catalogItemAssembler = new CatalogItemAssembler();
        private ViewResult _shopCatalogViewResult;

        [Given(@"A shop a catalog")]
        public void GivenAShopACatalog()
        {
            _catalogItemRepository = new MemoryCatalogItemRepository();
        }

        [Given(@"Some items in the catalog")]
        public void GivenSomeItemsInTheCatalog()
        {
            _catalogItemRepository.Persist(new InMemoryCatalogItem(_titles[0]));
            _catalogItemRepository.Persist(new InMemoryCatalogItem(_titles[1]));
        }
        
        [When(@"I visit the catalog")]
        public void WhenIVisitTheCatalog()
        {
            var shopCatalogService = new ShopCatalogService(_catalogItemRepository, _catalogItemAssembler);
            var shopCatalogController = new ShopCatalogController(shopCatalogService);
            _shopCatalogViewResult = shopCatalogController.Index();
        }

        [Then(@"all items' title are shown")]
        public void ThenAllItemsTitleAreShown()
        {
            var shopCatalogViewModel = _shopCatalogViewResult.Model as ShopCatalogViewModel;
            shopCatalogViewModel.CatalogItems.Select(x => x.Title).Should().BeEquivalentTo(_titles);
        }
    }
}
