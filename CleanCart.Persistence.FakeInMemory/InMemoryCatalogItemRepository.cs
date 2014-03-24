using System.Collections.Generic;
using System.Linq;
using CleanCart.Domain;

namespace CleanCart.Persistence.FakeInMemory
{
    public class InMemoryCatalogItemRepository  : ICatalogItemRepository
    {
        private readonly IList<CatalogItem> _items = new List<CatalogItem>();  

        public void Persist(CatalogItem catalogItem)
        {
            _items.Add(catalogItem);
        }

        public IList<CatalogItem> FindAll()
        {
            return _items;
        }

        public CatalogItem FindByItemCode(ItemCode itemCode)
        {
            CatalogItem item = _items.FirstOrDefault(x => x.Code == itemCode);
            if (item == null)
            {
                throw new CatalogItemNotFoundException();
            }
            return item;
        }
    }
}
