﻿using CleanCart.ApplicationServices.Assemblers;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CleanCart.ApplicationServices.Tests
{
    [TestClass]
    class ShopCatalogServiceTest
    {
        private List<CatalogItem> _catalogItems;
        private List<CatalogItemDTO> _itemsDTOs;
        private Mock<ICatalogItemRepository> _catalogItemRepository;
        private Mock<ICatalogItemFactory> _catalogItemFactory; 
        private Mock<CatalogItemAssembler> _assembler;

        private ShopCatalogService _shopCatalogService;

        [TestInitialize]
        public void SetupShopCatalogServiceWithFindableItems()
        {
            SetupCatalogItemRespositoriesWithSomeItems();
            SetupAssemblerToConvertCatalogItems();
            _catalogItemFactory = new Mock<ICatalogItemFactory>();
            _shopCatalogService = new ShopCatalogService(_catalogItemRepository.Object, _catalogItemFactory.Object, _assembler.Object);
        }

        private void SetupCatalogItemRespositoriesWithSomeItems()
        {
            _catalogItems = new List<CatalogItem>();

            _catalogItemRepository = new Mock<ICatalogItemRepository>();
            _catalogItemRepository.Setup(x => x.FindAll()).Returns(_catalogItems);
        }

        private void SetupAssemblerToConvertCatalogItems()
        {
            _assembler = new Mock<CatalogItemAssembler>();
            _itemsDTOs = new List<CatalogItemDTO>();
            _assembler.Setup(x => x.ToDtoList(_catalogItems)).Returns(_itemsDTOs);
        }

        [TestMethod]
        public void ListItemsShouldFindAllItems()
        {
            _shopCatalogService.ListCatalogItems();
            _catalogItemRepository.Verify(x => x.FindAll());
        }

        [TestMethod]
        public void CanListAllCatalogItemsInDTO()
        {
            var itemsDTOReturned = _shopCatalogService.ListCatalogItems();
            itemsDTOReturned.Should().BeEquivalentTo(_itemsDTOs);
        }

        [TestMethod]
        public void CanAddAnItemFromDTO()
        {
            var item = new Mock<CatalogItem>();
            var itemDTO = new CatalogItemDTO("I1", "A title"); //TODO
            _assembler.Setup(x => x.FromDTO(itemDTO, _catalogItemFactory.Object)).Returns(item.Object);

            _shopCatalogService.AddCatalogItem(itemDTO);

            _catalogItemRepository.Verify(x => x.Persist(item.Object));
        }
    }
}
