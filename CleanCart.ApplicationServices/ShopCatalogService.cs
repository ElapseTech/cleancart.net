using CleanCart.ApplicationServices.Assemblers;
using CleanCart.ApplicationServices.Dto;
using CleanCart.ApplicationServices.Locator;
using CleanCart.Domain;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices
{
    public class ShopCatalogService : IShopCatalogService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly CatalogItemAssembler _catalogItemAssembler;
        private ICatalogItemFactory _catalogItemFactory;

        public ShopCatalogService()
        {
            _catalogItemRepository = ServiceLocator.Locator.Resolve<ICatalogItemRepository>();
            _catalogItemFactory = ServiceLocator.Locator.Resolve<ICatalogItemFactory>();
            _catalogItemAssembler = new CatalogItemAssembler();
        }

        public ShopCatalogService(ICatalogItemRepository catalogItemRepository, ICatalogItemFactory catalogItemFactory, CatalogItemAssembler catalogItemAssembler)
        {
            _catalogItemFactory = catalogItemFactory;
            _catalogItemRepository = catalogItemRepository;
            _catalogItemAssembler = catalogItemAssembler;
        }

        public IEnumerable<CatalogItemDTO> ListCatalogItems()
        {
            var catalogItems = _catalogItemRepository.FindAll();
            var catalogItemDtos = _catalogItemAssembler.ToDtoList(catalogItems);
            return catalogItemDtos;
        }

        public void AddCatalogItem(CatalogItemDTO catalogItemDTO)
        {
            var catalogItem = _catalogItemAssembler.FromDTO(catalogItemDTO, _catalogItemFactory);
            _catalogItemRepository.Persist(catalogItem);
        }
    }
}