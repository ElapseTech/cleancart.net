
using System.Collections.Generic;
using CleanCart.Domain;

namespace CleanCart.Persistence.FakeInMemory
{
    public class MemoryCatalogItemRepository  : ICatalogItemRepository
    {
        private IList<CatalogItem> items = new List<CatalogItem>();  

        public void Persist(CatalogItem catalogItem)
        {
            items.Add(catalogItem);
        }

        public IList<CatalogItem> FindAll()
        {
            return items;
        }
    }
}
