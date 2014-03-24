using System.Linq;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Domain;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices.Assemblers
{
    public class CatalogItemAssembler
    {
        private readonly ICatalogItemFactory _catalogItemFactory;

        public CatalogItemAssembler()
        {
            //TODO Use a service locator to use the same implementation of factories
        }

        public CatalogItemAssembler(ICatalogItemFactory catalogItemFactory)
        {
            _catalogItemFactory = catalogItemFactory;
        }

        public virtual CatalogItemDTO ToDto(CatalogItem catalogItem)
        {
            return new CatalogItemDTO(catalogItem.Code.CodeValue, catalogItem.Title);
        }

        public virtual IList<CatalogItemDTO> ToDtoList(IEnumerable<CatalogItem> catalogItems)
        {
            return catalogItems.Select(ToDto).ToList();
        }

        public virtual CatalogItem FromDTO(CatalogItemDTO itemDTO)
        {
            var code = new CatalogItemCode(itemDTO.CodeText);
            string title = itemDTO.Title;
            return _catalogItemFactory.CreateCatalogItem(code, title);
        }
    }
}