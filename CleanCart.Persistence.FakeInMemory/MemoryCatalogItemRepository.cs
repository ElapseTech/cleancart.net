
using System.Collections.Generic;
using CleanCart.Domain;

namespace CleanCart.Persistence.FakeInMemory
{
    public class MemoryCatalogItemRepository  : ICatalogItemRepository
    {
        public void Persist(StandardCatalogItem standardCatalogItem)
        {
        }

        public IList<ICatalogItem> FindAll()
        {
            return null;
        }
    }
}
