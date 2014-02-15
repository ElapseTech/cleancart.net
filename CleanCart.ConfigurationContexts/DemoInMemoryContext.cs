using CleanCart.ConfigurationContexts.Configurations.Persistence;
using CleanCart.ConfigurationContexts.Fillers.ShopCatalog;

namespace CleanCart.ConfigurationContexts
{
    public class DemoInMemoryContext : ContextBase
    {
        public void Apply()
        {
            UseConfiguration<InMemoryPersistenceConfiguration>();
            
            UseFiller<InMemoryShopCatalogFiller>();
        }
    }
}
