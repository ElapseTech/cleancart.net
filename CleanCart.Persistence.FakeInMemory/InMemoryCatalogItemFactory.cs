using CleanCart.Domain;
using CleanCart.Persistence.FakeInMemory.Entities;

namespace CleanCart.Persistence.FakeInMemory
{
    public class InMemoryCatalogItemFactory : ICatalogItemFactory
    {
        public CatalogItem CreateCatalogItem(CatalogItemCode code, string title)
        {
            return new InMemoryCatalogItem(code, title);
        }
    }
}