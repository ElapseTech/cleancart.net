
using System.Collections.Generic;
using CleanCart.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CleanCart.ApplicationServices.Tests
{
    [TestClass]
    class ShopCatalogServiceTest
    {

        [TestMethod]
        public void CanListAllCatalogItemsOfTheShop()
        {
            var bacon = new Mock<ICatalogItem>();
            var egg = new Mock<ICatalogItem>();
            var catalogItems = new List<ICatalogItem>() {bacon.Object, egg.Object};
            var catalogItemRepository = new Mock<ICatalogItemRepository>();
            catalogItemRepository.Setup(x => x.FindAll()).Returns(catalogItems);
            var shopCatalogService = new ShopCatalogService(catalogItemRepository.Object);

            var itemsReceived = shopCatalogService.ListCatalogItems();

            itemsReceived.Should().BeEquivalentTo(catalogItems);
        }
    }
}
