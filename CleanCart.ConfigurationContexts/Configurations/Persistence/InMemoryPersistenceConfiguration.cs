using CleanCart.ApplicationServices.Locator;
using CleanCart.Domain;
using CleanCart.Persistence.FakeInMemory;

namespace CleanCart.ConfigurationContexts.Configurations.Persistence
{
    class InMemoryPersistenceConfiguration : IConfiguration
    {
        public void Install()
        {
            ServiceLocator.Locator.Register<ICatalogItemRepository>(new InMemoryCatalogItemRepository());
        }
    }
}
