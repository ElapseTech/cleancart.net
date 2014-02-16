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

        protected void UseFiller<TFiller>() where TFiller : IFiller, new()
        {
            new TFiller().Apply();
        }
    }
}