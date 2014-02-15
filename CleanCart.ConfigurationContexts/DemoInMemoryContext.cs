using CleanCart.ConfigurationContexts.Configurations.Persistence;

namespace CleanCart.ConfigurationContexts
{
    class DemoInMemoryContext : ContextBase
    {
        public void Apply()
        {
            UseConfiguration<InMemoryPersistenceConfiguration>();
        }
    }
}
