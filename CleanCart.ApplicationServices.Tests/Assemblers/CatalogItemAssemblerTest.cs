using CleanCart.ApplicationServices.Assemblers;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace CleanCart.ApplicationServices.Tests.Assemblers
{
    [TestClass]
    public class CatalogItemAssemblerTest
    {
        private const string EggTitle = "The Better Egg";
        private const string EggCodeText = "THE_CODE";
        private readonly CatalogItemCode _eggCode = new CatalogItemCode(EggCodeText);

        private const string SomeTitle = "Title";
        const string FirstTitle = "FIRST TITLE";
        const string SecondTitle = "SECOND TITLE";

        private Mock<CatalogItemFactory> _itemFactory;

        private CatalogItemAssembler _itemAssembler;

        [TestInitialize]
        public void SetupAssembler()
        {
            _itemFactory = new Mock<CatalogItemFactory>();
            _itemAssembler = new CatalogItemAssembler();
        }

        [TestMethod]
        public void ToDTOShouldMapTheTitle()
        {
            var catalogItem = CreateCatalogItemMock(_eggCode, EggTitle);
            var dto = _itemAssembler.ToDto(catalogItem.Object);
            dto.Title.Should().Be(EggTitle);
        }

        [TestMethod]
        public void ToDTOShouldMapTheCode()
        {
            var catalogItem = CreateCatalogItemMock(_eggCode, SomeTitle);

            var dto = _itemAssembler.ToDto(catalogItem.Object);

            var dtoCode = new CatalogItemCode(dto.CodeText);
            dtoCode.Should().Be(_eggCode);
        }

        [TestMethod]
        public void ToDTOListShouldMapForAllItemsInSameOrder()
        {
            var catalogItems = new List<CatalogItem> {CreateCatalogItemMock(FirstTitle).Object, 
                                                      CreateCatalogItemMock(SecondTitle).Object};

            var dtos = _itemAssembler.ToDtoList(catalogItems);

            dtos.ElementAt(0).Title.Should().Be(FirstTitle);
            dtos.ElementAt(1).Title.Should().Be(SecondTitle);
            dtos.Should().HaveCount(catalogItems.Count);
        }

        [TestMethod]
        public void FromDTOShouldCreateTheItemWithAllAttributesUsingTheFactory()
        {
            var itemDTO = new CatalogItemDTO(EggCodeText, EggTitle);
            var egg = CreateCatalogItemMock(_eggCode, EggTitle);
            _itemFactory.Setup(x => x.CreateCatalogItem(_eggCode, EggTitle)).Returns(egg.Object);

            var itemReturned = _itemAssembler.FromDTO(itemDTO, _itemFactory.Object);

            Assert.AreEqual(egg.Object, itemReturned);
        }

        private Mock<CatalogItem> CreateCatalogItemMock(string title)
        {
            var code = new CatalogItemCode(EggCodeText);
            return CreateCatalogItemMock(code, title);
        }

        private Mock<CatalogItem> CreateCatalogItemMock(CatalogItemCode code, string title)
        {
            var item = new Mock<CatalogItem>();
            item.Setup(x => x.Title).Returns(title);
            item.Setup(x => x.Code).Returns(code);
            return item;
        }
    }
}
