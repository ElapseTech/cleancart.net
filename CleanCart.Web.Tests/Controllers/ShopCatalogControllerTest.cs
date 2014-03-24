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

        [TestMethod]
        public void IndexShouldRetrivedListItemsFromService()
        {
            var service = new Mock<IShopCatalogService>();
            var shopCatalogController = new ShopCatalogController(service.Object);

            shopCatalogController.Index();

            service.Verify(x => x.ListCatalogItems());
        }

        [TestMethod]
        public void IndexShouldListAllItems()
        {
            var service = new Mock<IShopCatalogService>();
            var catalogItemDtos = new List<CatalogItemDTO>();
            service.Setup(x => x.ListCatalogItems()).Returns(catalogItemDtos);
            var shopCatalogController = new ShopCatalogController(service.Object);

            var viewResult = shopCatalogController.Index();

            var viewModel = viewResult.Model as ShopCatalogViewModel;
            viewModel.CatalogItems.Should().BeEquivalentTo(catalogItemDtos);
        }
    }
}
