
using System.Collections.Generic;
using CleanCart.Domain;

namespace CleanCart.Persistence.FakeInMemory
{
    public class MemoryCatalogItemRepository  : ICatalogItemRepository
    {
        public void Persist(CatalogItem catalogItem)
        {
        }

        public IList<CatalogItem> FindAll()
        {
            return null;
        }
    }
}
