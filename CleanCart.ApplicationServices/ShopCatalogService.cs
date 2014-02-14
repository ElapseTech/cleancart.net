using CleanCart.ApplicationServices.Assemblers;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Domain;
using System.Collections;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices
{
    public class ShopCatalogService : IShopCatalogService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly CatalogItemAssembler _catalogItemAssembler;

        public ShopCatalogService(ICatalogItemRepository catalogItemRepository, CatalogItemAssembler catalogItemAssembler)
        {
            _catalogItemRepository = catalogItemRepository;
            _catalogItemAssembler = catalogItemAssembler;
        }

        public IEnumerable<CatalogItemDTO> ListCatalogItems()
        {
            var catalogItems = _catalogItemRepository.FindAll();
            var catalogItemDtos = _catalogItemAssembler.ToDtoList(catalogItems);
            return catalogItemDtos;
        }
    }
}