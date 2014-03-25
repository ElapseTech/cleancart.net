using CleanCart.ApplicationServices.Locator;
using CleanCart.Domain;

namespace CleanCart.ConfigurationContexts.Fillers.ShopCatalog
{
    class ShopCatalogFiller : IFiller
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly ICatalogItemFactory _catalogItemFactory;

        public ShopCatalogFiller()
        {
            _catalogItemRepository = ServiceLocator.Locator.Resolve<ICatalogItemRepository>();
            _catalogItemFactory = ServiceLocator.Locator.Resolve<ICatalogItemFactory>();
        }

        public void Apply()
        {
            _catalogItemRepository.Persist(_catalogItemFactory.CreateCatalogItem(new CatalogItemCode("I1"), "Item #1"));
            _catalogItemRepository.Persist(_catalogItemFactory.CreateCatalogItem(new CatalogItemCode("I2"), "Item #2"));
            _catalogItemRepository.Persist(_catalogItemFactory.CreateCatalogItem(new CatalogItemCode("I3"), "Item #3"));
            _catalogItemRepository.Persist(_catalogItemFactory.CreateCatalogItem(new CatalogItemCode("I4"), "Item #4"));
        }
    }
}
