using System.Web.Mvc;
using CleanCart.ApplicationServices;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Controllers;
using CleanCart.ViewModels.ShopCatalog;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CleanCart.Tests.Controllers
{
    [TestClass]
    public class ShopCatalogControllerTest
    {
        private const string SomeCode = "CODE";
        private const string SomeTitle = "Title";

        private Mock<IShopCatalogService> _service;
        private ShopCatalogController _shopCatalogController;
        private List<CatalogItemDTO> _catalogItemsDtos;

        [TestInitialize]
        public void ConfigureTheControllerWithAServiceWithSomeItems()
        {
            _service = new Mock<IShopCatalogService>();
            _shopCatalogController = new ShopCatalogController(_service.Object);

            _catalogItemsDtos = new List<CatalogItemDTO>();
            _service.Setup(x => x.ListCatalogItems()).Returns(_catalogItemsDtos);
        }

        [TestMethod]
        public void IndexShouldGenerateAViewWithAllItems()
        {
            var viewResult = _shopCatalogController.Index();
            AssertViewResultContainsAllItems(viewResult);
        }

        [TestMethod]
        public void AddItemShouldAddTheItem()
        {
            var newItemForm = new CatalogItemDTO(SomeCode, SomeTitle);
            _shopCatalogController.AddItem(newItemForm);
            _service.Verify(x => x.AddCatalogItem(newItemForm));
        }

        [TestMethod]
        public void AddItemShouldGenerateAViewWithAllItems()
        {
            var newItemForm = new CatalogItemDTO(SomeCode, SomeTitle);
            var viewResult = _shopCatalogController.AddItem(newItemForm);
            AssertViewResultContainsAllItems(viewResult);
        }

        private void AssertViewResultContainsAllItems(ViewResult viewResult)
        {
            var viewModel = viewResult.Model as ShopCatalogViewModel;
            viewModel.CatalogItems.Should().BeEquivalentTo(_catalogItemsDtos);
        }
    }
}
