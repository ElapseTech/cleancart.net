using CleanCart.ApplicationServices.Locator;
using CleanCart.ConfigurationContexts.Configurations.Persistence;
using CleanCart.ConfigurationContexts.Fillers.ShopCatalog;

namespace CleanCart.ConfigurationContexts
{
    public class EmptyInMemoryContext : ContextBase
    {

        public void Apply()
        {
            Prepare();

            UseConfiguration<InMemoryPersistenceConfiguration>();
        }

        private void Prepare()
        {
            ServiceLocator.Locator.Reset();
        }
    }
}
