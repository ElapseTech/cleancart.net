using CleanCart.ApplicationServices.Dto;
using CleanCart.Domain;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices.Assemblers
{
    public class CatalogItemAssembler
    {
        public virtual IEnumerable<CatalogItemDTO> ToDtoList(IEnumerable<ICatalogItem> catalogItems)
        {
            return null;
        }
    }
}