using CleanCart.ApplicationServices.Dto;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices
{
    public interface IShopCatalogService
    {
        IEnumerable<CatalogItemDTO> ListCatalogItems();

        void AddCatalogItem(CatalogItemDTO catalogItemDTO);
    }
}