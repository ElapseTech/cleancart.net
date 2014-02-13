using System.Collections;
using System.Collections.Generic;

namespace CleanCart.Domain
{
    public interface ICatalogItemRepository
    {
        void Persist(StandardCatalogItem standardCatalogItem);
        IList<ICatalogItem> FindAll();
    }
}