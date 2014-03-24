using System.Linq;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Domain;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices.Assemblers
{
    public class CatalogItemAssembler
    {
        public virtual CatalogItemDTO ToDto(CatalogItem catalogItem)
        {
            return new CatalogItemDTO(catalogItem.Title);
        }

        public virtual IList<CatalogItemDTO> ToDtoList(IEnumerable<CatalogItem> catalogItems)
        {
            return catalogItems.Select(ToDto).ToList();
        }
    }
}