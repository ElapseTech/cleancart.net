using System.Collections.Generic;
using CleanCart.ApplicationServices.Dto;

namespace CleanCart.ViewModels.ShopCatalog
{
    public class ShopCatalogViewModel
    {
        public IEnumerable<CatalogItemDTO> CatalogItems { get; set; }
    }
}