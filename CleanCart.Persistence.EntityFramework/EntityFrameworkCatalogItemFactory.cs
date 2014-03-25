using CleanCart.Domain;
using CleanCart.Persistence.EntityFramework.Entities;

namespace CleanCart.Persistence.EntityFramework
{
    public class EntityFrameworkCatalogItemFactory : ICatalogItemFactory
    {
        public CatalogItem CreateCatalogItem(CatalogItemCode code, string title)
        {
            return new EntityFrameworkCatalogItem(code, title);
        }
    }
}
