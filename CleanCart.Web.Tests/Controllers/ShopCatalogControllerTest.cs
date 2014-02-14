using CleanCart.ApplicationServices;
using CleanCart.ApplicationServices.Assemblers;
using CleanCart.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        [Ignore]
        public void IndexShouldListAllItems()
        {
            var assembler = new Mock<CatalogItemAssembler>();
            var service = new Mock<IShopCatalogService>();
            var shopCatalogController = new ShopCatalogController(service.Object);

            shopCatalogController.Index();

        }
    }
}
