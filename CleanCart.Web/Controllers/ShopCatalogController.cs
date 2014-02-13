using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CleanCart.ApplicationServices;

namespace CleanCart.Controllers
{
    public class ShopCatalogController : Controller
    {
        private readonly IShopCatalogService _shopCatalogService;

        public ShopCatalogController(IShopCatalogService shopCatalogService)
        {
            _shopCatalogService = shopCatalogService;
        }

        public ViewResult Index()
        {
            _shopCatalogService.ListCatalogItems();

            return View();
        }

    }
}
