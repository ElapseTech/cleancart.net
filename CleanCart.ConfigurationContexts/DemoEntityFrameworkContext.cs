using CleanCart.ConfigurationContexts.Configurations.Persistence;
using CleanCart.ConfigurationContexts.Fillers.ShopCatalog;
using CleanCart.Persistence.EntityFramework;

namespace CleanCart.ConfigurationContexts
{
    class DemoEntityFrameworkContext : ContextBase
    {
        private DbContextProvider _dbContextProvider;

        public void Apply()
        {
            _dbContextProvider = new DbContextProvider();
            UseConfiguration(new EntityFrameworkPersistenceConfig(_dbContextProvider));
            UseFiller<ShopCatalogFiller>();
        }

        public override void BeforeRequest()
        {
            base.BeforeRequest();
            _dbContextProvider.ConfigureShopDbContext(new ShopDbContext());
        }
    }
}
