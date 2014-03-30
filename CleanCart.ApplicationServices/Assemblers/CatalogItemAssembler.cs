using System;
using System.Linq;
using CleanCart.ApplicationServices.Dto;
using CleanCart.ApplicationServices.Locator;
using CleanCart.Domain;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices.Assemblers
{
    public class CatalogItemAssembler
    {
        public virtual CatalogItemDTO ToDto(CatalogItem catalogItem)
        {
            return new CatalogItemDTO(catalogItem.Code.CodeValue, catalogItem.Title);
        }

        public virtual IList<CatalogItemDTO> ToDtoList(IEnumerable<CatalogItem> catalogItems)
        {
            return catalogItems.Select(ToDto).ToList();
        }

        public virtual CatalogItem FromDTO(CatalogItemDTO itemDTO, CatalogItemFactory catalogItemFactory)
        {
            var code = new CatalogItemCode(itemDTO.CodeText ?? String.Empty);
            string title = itemDTO.Title;
            return catalogItemFactory.CreateCatalogItem(code, title);
        }
    }
}