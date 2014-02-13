using System.Collections.Generic;

namespace CleanCart.ViewModels.ShopCatalog
{
    public class ShopCatalogViewModel
    {
        public IEnumerable<CatalogItemViewModel> CatalogItems { get; set; }
    }
}