using System.Collections;
using System.Collections.Generic;
using CleanCart.Domain;

namespace CleanCart.ApplicationServices
{
    public class ShopCatalogService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public ShopCatalogService(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public IEnumerable<ICatalogItem> ListCatalogItems()
        {
            return _catalogItemRepository.FindAll();
        }
    }
}