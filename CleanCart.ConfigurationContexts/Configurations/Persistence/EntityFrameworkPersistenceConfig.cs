using CleanCart.ApplicationServices.Locator;
using CleanCart.Domain;
using CleanCart.Persistence.EntityFramework;

namespace CleanCart.ConfigurationContexts.Configurations.Persistence
{
    class EntityFrameworkPersistenceConfig : IConfiguration
    {
        private readonly DbContextProvider _provider;

        public EntityFrameworkPersistenceConfig(DbContextProvider provider)
        {
            _provider = provider;
        }

        public void Install()
        {
            ServiceLocator.Locator.Register<ICatalogItemRepository>(new EntityFrameworkCatalogItemRepository(_provider));
            ServiceLocator.Locator.Register<ICatalogItemFactory>(new EntityFrameworkCatalogItemFactory());
        }
    }
}
