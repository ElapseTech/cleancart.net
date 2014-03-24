using CleanCart.ApplicationServices.Locator;
using CleanCart.Domain;
using CleanCart.Persistence.FakeInMemory.Entities;

namespace CleanCart.ConfigurationContexts.Fillers.ShopCatalog
{
    class InMemoryShopCatalogFiller : IFiller
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public InMemoryShopCatalogFiller()
        {
            _catalogItemRepository = ServiceLocator.Locator.Resolve<ICatalogItemRepository>();
        }

        public void Apply()
        {
            _catalogItemRepository.Persist(new InMemoryCatalogItem("Item #1"));
            _catalogItemRepository.Persist(new InMemoryCatalogItem("Item #2"));
            _catalogItemRepository.Persist(new InMemoryCatalogItem("Item #3"));
            _catalogItemRepository.Persist(new InMemoryCatalogItem("Item #4"));
        }
    }
}
