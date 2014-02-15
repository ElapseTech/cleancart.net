using CleanCart.ConfigurationContexts.Configurations;

namespace CleanCart.ConfigurationContexts
{
    internal class ContextBase
    {
        protected void UseConfiguration<TConfiguration>() where TConfiguration : IConfiguration, new()
        {
            new TConfiguration().Install();
        }
    }
}