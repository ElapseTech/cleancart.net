using CleanCart.ApplicationServices;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Domain;
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
            return GenerateShopCatalogViewResult();
        }

        [HttpPost]
        public ViewResult AddItem(CatalogItemDTO catalogItemForm)
        {
            try
            {
                _shopCatalogService.AddCatalogItem(catalogItemForm);
            }
            catch (CatalogItemCreationException e)
            {
                ModelState.AddModelError("errors", e.Message);
            }

            return GenerateShopCatalogViewResult();
        }

        private ViewResult GenerateShopCatalogViewResult()
        {
            var catalogItems = _shopCatalogService.ListCatalogItems();
            var viewModel = new ShopCatalogViewModel
            {
                CatalogItems = catalogItems,
                AddItemForm = new CatalogItemDTO(codeText: "", title: "")
            };
            return View("Index", viewModel);
        }
    }
}
