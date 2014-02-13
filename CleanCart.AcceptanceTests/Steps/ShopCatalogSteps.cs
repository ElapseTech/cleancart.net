using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using CleanCart.AcceptanceTests.Fakes;
using CleanCart.Controllers;
using CleanCart.Domain;
using CleanCart.ViewModels.ShopCatalog;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CleanCart.AcceptanceTests.Steps
{
    [Binding]
    class ShopCatalogSteps
    {
        private readonly String[] _titles = new String[] {"ITEM 1", "item 2"};

        private MemoryCatalogItemRepository _catalogItemRepository;
        private ViewResult _shopCatalogViewResult;

        [Given(@"A shop a catalog")]
        public void GivenAShopACatalog()
        {
            _catalogItemRepository = new MemoryCatalogItemRepository();
        }

        [Given(@"Some items in the catalog")]
        public void GivenSomeItemsInTheCatalog()
        {
            _catalogItemRepository.Persist(new StandardCatalogItem());
            _catalogItemRepository.Persist(new StandardCatalogItem());
        }
        
        [When(@"I visit the catalog")]
        public void WhenIVisitTheCatalog()
        {
            var shopCatalogController = new ShopCatalogController();
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
