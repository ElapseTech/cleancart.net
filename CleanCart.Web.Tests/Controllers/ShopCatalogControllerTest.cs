using CleanCart.ApplicationServices;
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
    }
}
