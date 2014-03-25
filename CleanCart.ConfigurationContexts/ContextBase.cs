using CleanCart.ConfigurationContexts.Configurations;
using CleanCart.ConfigurationContexts.Fillers.ShopCatalog;

namespace CleanCart.ConfigurationContexts
{
    public class ContextBase
    {
        protected void UseConfiguration<TConfiguration>() where TConfiguration : IConfiguration, new()
        {
            new TConfiguration().Install();
        }

        protected void UseConfiguration<TConfiguration>(TConfiguration configuration) where TConfiguration : IConfiguration
        {
             configuration.Install();
        }

        protected void UseFiller<TFiller>() where TFiller : IFiller, new()
        {
            new TFiller().Apply();
        }

        public virtual void BeforeRequest() 
        {
        }

        public virtual void AfterRequest() 
        {
        }
    }
}