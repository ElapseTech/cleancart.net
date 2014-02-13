using System.Collections.Generic;
using CleanCart.Domain;

namespace CleanCart.ApplicationServices
{
    public interface IShopCatalogService
    {
        IEnumerable<ICatalogItem> ListCatalogItems();
    }
}