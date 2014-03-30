using System.Collections.Generic;
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

        private const string NoTitle = null;
        private const string NoItemCode = null;

        private InMemoryCatalogItemRepository _catalogItemRepository;
        private CatalogItemFactory _catalogItemFactory;
        private CatalogItemAssembler _catalogItemAssembler;

        private IList<string> _registeredTitles;

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

            _shopCatalogService = new ShopCatalogService(_catalogItemRepository, _catalogItemFactory, _catalogItemAssembler);
            _shopCatalogController = new ShopCatalogController(_shopCatalogService);
        }

        [BeforeScenario]
        public void ResetResultsAndDatas()
        {
            _shopCatalogViewResult = null;
            _errorIsReported = false;

            _registeredTitles = new List<string>();
            ;
        }


        [Given(@"A shop catalog")]
        public void GivenAShopACatalog()
        {
        }

        [Given(@"Some items in the catalog")]
        public void GivenSomeItemsInTheCatalog()
        {
            PersistItemToCatalog(AnItemCodeText(), AnItemTitle());
            PersistItemToCatalog(AnItemCodeText(), AnItemTitle());
        }

        [Given(@"an item with code '(.*)' in the catalog")]
        public void GivenAnItemWithCodeInTheCatalog(string itemCodeText)
        {
            PersistItemToCatalog(itemCodeText, AnItemTitle());
        }



        [When(@"I request the catalog")]
        public void WhenIRequestTheCatalog()
        {
            _shopCatalogViewResult = _shopCatalogController.Index();
        }

        [When(@"I add a new item with no title")]
        public void WhenIAddANewItemWithNoTitle()
        {
            var itemDTO = new CatalogItemDTO(AnItemCodeText(), NoTitle);
            AddCatalogItemUsingTheServiceLayer(itemDTO);
        }

        [When(@"I add a new item with no item code")]
        public void WhenIAddANewItemWithNoItemCode()
        {
            var itemDTO = new CatalogItemDTO(NoItemCode, AnItemTitle());
            AddCatalogItemUsingTheServiceLayer(itemDTO);
        }



        [Then(@"all items' title are present")]
        public void ThenAllRegisteredItemsTitleArePresent()
        {
            var shopCatalogViewModel = _shopCatalogViewResult.Model as ShopCatalogViewModel;
            shopCatalogViewModel.CatalogItems.Select(x => x.Title).Should().BeEquivalentTo(_registeredTitles);
        }

        [Then(@"an error is reported")]
        public void ThenAnErrorIsReported()
        {
            Assert.IsTrue(_errorIsReported, "Error was not reported");
        }



        private void PersistItemToCatalog(string catalogItemCodeText, string itemTitle)
        {
            var catalogItemCode = new CatalogItemCode(catalogItemCodeText);
            var item = new InMemoryCatalogItem(catalogItemCode, itemTitle);
            _catalogItemRepository.Persist(item);

            _registeredTitles.Add(itemTitle);
        }

        private void AddCatalogItemUsingTheServiceLayer(CatalogItemDTO newItemDTO)
        {
            try
            {
                _shopCatalogService.AddCatalogItem(newItemDTO);
            }
            catch (CatalogItemCreationException e)
            {
                _errorIsReported = true;
            }
        }

        private string AnItemCodeText()
        {
            return _autoGenerator.Create("Code");
        }

        private string AnItemTitle()
        {
            return _autoGenerator.Create("Title");
        }
    }
}
