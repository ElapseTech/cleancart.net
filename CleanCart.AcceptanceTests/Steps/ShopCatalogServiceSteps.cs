using CleanCart.ApplicationServices;
using CleanCart.ApplicationServices.Assemblers;
using CleanCart.ApplicationServices.Dto;
using CleanCart.ApplicationServices.Locator;
using CleanCart.ConfigurationContexts;
using CleanCart.Controllers;
using CleanCart.Domain;
using CleanCart.Persistence.FakeInMemory;
using CleanCart.Persistence.FakeInMemory.Entities;
using CleanCart.ViewModels.ShopCatalog;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System;
using System.Linq;
using System.Web.Mvc;
using TechTalk.SpecFlow;

namespace CleanCart.AcceptanceTests.Steps
{
    [Binding]
    class ShopCatalogServiceSteps
    {
        private readonly Fixture _autoGenerator = new Fixture();

        private const string NoTitle = "";
        private readonly String[] _titles = { "ITEM 1", "item 2" };

        private InMemoryCatalogItemRepository _catalogItemRepository;
        private CatalogItemFactory _catalogItemFactory;
        private CatalogItemAssembler _catalogItemAssembler;

        private ViewResult _shopCatalogViewResult;
        private ShopCatalogService _shopCatalogService;
        private ShopCatalogController _shopCatalogController;
        private bool _errorIsReported;

        [BeforeScenario]
        public void ConfigureControllerAndServices()
        {
            _catalogItemRepository = new InMemoryCatalogItemRepository();
            _catalogItemFactory = new InMemoryCatalogItemFactory();
            _catalogItemAssembler = new CatalogItemAssembler();

            _shopCatalogService = new ShopCatalogService(_catalogItemRepository, _catalogItemFactory,
                _catalogItemAssembler);
            _shopCatalogController = new ShopCatalogController(_shopCatalogService);
        }

        [BeforeScenario]
        public void ResetResults()
        {
            _shopCatalogViewResult = null;
            _errorIsReported = false;
        } 


        [Given(@"A shop catalog")]
        public void GivenAShopACatalog()
        {
        }

        [Given(@"Some items in the catalog")]
        public void GivenSomeItemsInTheCatalog()
        {
            PersistANewItemToCatalog(CreateItemCode(), _titles[0]);
            PersistANewItemToCatalog(CreateItemCode(), _titles[1]);
        }

        [Given(@"an item with code '(.*)' in the catalog")]
        public void GivenAnItemWithCodeInTheCatalog(string itemCodeText)
        {
            var itemTitle = CreateItemTitle();
            PersistANewItemToCatalog(itemCodeText, itemTitle);
        }



        [When(@"I request the catalog")]
        public void WhenIRequestTheCatalog()
        {
            _shopCatalogViewResult = _shopCatalogController.Index();
        }

        [When(@"I add a new item with no title")]
        public void WhenIAddANewItemWithNoTitle()
        {
            var newItemDTO = new CatalogItemDTO(CreateItemCode(), NoTitle);
            try
            {
                _shopCatalogService.AddCatalogItem(newItemDTO);
            }
            catch (CatalogItemCreationException e)
            {
                _errorIsReported = true;
            }
        }


        [Then(@"all items' title are present")]
        public void ThenAllItemsTitleArePresent()
        {
            var shopCatalogViewModel = _shopCatalogViewResult.Model as ShopCatalogViewModel;
            shopCatalogViewModel.CatalogItems.Select(x => x.Title).Should().BeEquivalentTo(_titles);
        }

        [Then(@"an error is reported")]
        public void ThenAnErrorIsReported()
        {
            Assert.IsTrue(_errorIsReported, "Error was not reported");
        }



        private void PersistANewItemToCatalog(string catalogItemCodeText, string itemTitle)
        {
            var catalogItemCode = new CatalogItemCode(catalogItemCodeText);
            var item = new InMemoryCatalogItem(catalogItemCode, itemTitle);
            _catalogItemRepository.Persist(item);
        }


        private string CreateItemCode()
        {
            return _autoGenerator.Create("Code");
        }

        private string CreateItemTitle()
        {
            return _autoGenerator.Create("Title");
        }
    }
}
