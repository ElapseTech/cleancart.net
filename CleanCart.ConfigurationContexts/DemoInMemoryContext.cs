using CleanCart.ApplicationServices.Locator;
using CleanCart.ConfigurationContexts.Configurations.Persistence;
using CleanCart.ConfigurationContexts.Fillers.ShopCatalog;

namespace CleanCart.ConfigurationContexts
{
    public class DemoInMemoryContext : ContextBase
    {

        public void Apply()
        {
            Prepare();

            UseConfiguration<InMemoryPersistenceConfiguration>();
            UseFiller<InMemoryShopCatalogFiller>();
        }

        private void Prepare()
        {
            ServiceLocator.Locator.Reset();
        }
    }
}
