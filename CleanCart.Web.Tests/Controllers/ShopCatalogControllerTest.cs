using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using CleanCart.ApplicationServices;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Controllers;
using CleanCart.Domain;
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
        private ShopCatalogController _controller;

        private List<CatalogItemDTO> _allItemsDtos;
        private CatalogItemDTO _aNewItemForm;

        [TestInitialize]
        public void ConfigureTheControllerWithAServiceWithSomeItems()
        {
            _service = new Mock<IShopCatalogService>();
            _controller = new ShopCatalogController(_service.Object);

            _allItemsDtos = new List<CatalogItemDTO>();
            _service.Setup(x => x.ListCatalogItems()).Returns(_allItemsDtos);
        }

        [TestInitialize]
        public void ConfigureATypicalNewCatalogItemForm()
        {
            _aNewItemForm = new CatalogItemDTO(SomeCode, SomeTitle);
        }

        [TestMethod]
        public void IndexShouldGenerateAViewWithAllItems()
        {
            var viewResult = _controller.Index();
            AssertViewResultContainsAllItems(viewResult);
        }

        [TestMethod]
        public void AddItemShouldAddTheItemUsingTheServiceLayer()
        {
            _controller.AddItem(_aNewItemForm);
            _service.Verify(x => x.AddCatalogItem(_aNewItemForm));
        }

        [TestMethod]
        public void AddItemShouldGenerateAViewWithAllItems()
        {
            var viewResult = _controller.AddItem(_aNewItemForm);
            AssertViewResultContainsAllItems(viewResult);
        }

        [TestMethod]
        public void ErrorWhenAddItemShouldGenerateModelStateErrorWithCoresspingMessageOnTheField()
        {
            const string theErrorMessage = "The error message";
            ConfigureAddItemOnServiceToThrowException(theErrorMessage);

            var viewResult = _controller.AddItem(_aNewItemForm);

            AssertModeStateHasOnlyThisError(theErrorMessage);
        }


        private void ConfigureAddItemOnServiceToThrowException(string theErrorMessage)
        {
            var exception = new CatalogItemCreationException(theErrorMessage);
            _service.Setup(x => x.AddCatalogItem(It.IsAny<CatalogItemDTO>())).Throws(exception);
        }

        private void AssertModeStateHasOnlyThisError(string theErrorMessage)
        {
            var modelState = _controller.ModelState["errors"];
            modelState.Errors.Should().HaveCount(1);
            modelState.Errors.First().ErrorMessage.Should().Be(theErrorMessage);
        }

        private void AssertViewResultContainsAllItems(ViewResult viewResult)
        {
            var viewModel = viewResult.Model as ShopCatalogViewModel;
            viewModel.CatalogItems.Should().BeEquivalentTo(_allItemsDtos);
        }
    }
}
