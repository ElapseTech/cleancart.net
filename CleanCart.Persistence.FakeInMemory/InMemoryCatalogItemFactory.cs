using CleanCart.Domain;
using CleanCart.Persistence.FakeInMemory.Entities;

namespace CleanCart.Persistence.FakeInMemory
{
    public class InMemoryCatalogItemFactory
    {
        CatalogItem CreateCatalogItem(CatalogItemCode code, string title)
        {
            return new InMemoryCatalogItem(code, title);
        }
    }
}