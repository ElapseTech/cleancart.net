using System.Collections.Generic;

namespace CleanCart.Domain
{
    public interface ICatalogItemRepository
    {
        void Persist(CatalogItem catalogItem);

        IList<CatalogItem> FindAll();

        CatalogItem FindByItemCode(CatalogItemCode catalogItemCode);
    }
}