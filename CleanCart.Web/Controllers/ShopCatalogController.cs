using CleanCart.ApplicationServices;
using CleanCart.ViewModels.ShopCatalog;
using System.Web.Mvc;

namespace CleanCart.Controllers
{
    public class ShopCatalogController : Controller
    {
        private readonly IShopCatalogService _shopCatalogService;

        public ShopCatalogController()
        {
            _shopCatalogService = new ShopCatalogService();
        }

        public ShopCatalogController(IShopCatalogService shopCatalogService)
        {
            _shopCatalogService = shopCatalogService;
        }

        public ViewResult Index()
        {
            var catalogItems = _shopCatalogService.ListCatalogItems();
            var viewModel = new ShopCatalogViewModel {CatalogItems = catalogItems};
            return View(viewModel);
        }

    }
}
