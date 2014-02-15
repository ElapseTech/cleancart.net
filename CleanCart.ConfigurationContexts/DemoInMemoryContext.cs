using CleanCart.ConfigurationContexts.Configurations.Persistence;

namespace CleanCart.ConfigurationContexts
{
    public class DemoInMemoryContext : ContextBase
    {
        public void Apply()
        {
            UseConfiguration<InMemoryPersistenceConfiguration>();
        }
    }
}
