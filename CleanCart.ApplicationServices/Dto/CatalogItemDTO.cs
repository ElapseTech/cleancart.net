using System;

namespace CleanCart.ApplicationServices.Dto
{
    public sealed class CatalogItemDTO
    {
        public CatalogItemDTO(string title)
        {
            Title = title;
        }

        public String Title { get; private set; }
    }
}