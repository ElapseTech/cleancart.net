using CleanCart.ApplicationServices.Assemblers;
using CleanCart.ApplicationServices.Dto;
using CleanCart.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;

namespace CleanCart.ApplicationServices.Tests.Assemblers
{
    //TODO Refactor this test
    [TestClass]
    public class CatalogItemAssemblerTest
    {
        private static readonly Fixture AutoGenerator = new Fixture();

        private const string EggTitle = "The Better Egg";
        private const string EggCodeText = "THE_CODE";
        private readonly CatalogItemCode _eggCode = new CatalogItemCode(EggCodeText);

        private readonly CatalogItemCode _firstCode = new CatalogItemCode(AutoGenerator.Create<string>());
        private readonly CatalogItemCode _secondCode = new CatalogItemCode(AutoGenerator.Create<string>());

        private const string SomeTitle = "Title";

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
            var catalogItem = CreateCatalogItemMock(AutoGenerator.Create<CatalogItemCode>(), EggTitle);
            var dtoReturned = _itemAssembler.ToDto(catalogItem.Object);
            dtoReturned.Title.Should().Be(EggTitle);
        }

        [TestMethod]
        public void ToDTOShouldMapTheCode()
        {
            var catalogItem = CreateCatalogItemMock(_eggCode, SomeTitle);
            var dtoReturned = _itemAssembler.ToDto(catalogItem.Object);
            dtoReturned.CodeText.Should().Be(_eggCode.CodeValue);
        }

        [TestMethod]
        public void ToDTOListShouldMapForAllItemsInSameOrder()
        {
            var catalogItems = new List<CatalogItem> {CreateCatalogItemMock(_firstCode).Object, 
                                                      CreateCatalogItemMock(_secondCode).Object};

            var dtos = _itemAssembler.ToDtoList(catalogItems);

            dtos.Should().HaveCount(catalogItems.Count);
            dtos.ElementAt(0).CodeText.Should().Be(_firstCode.CodeValue);
            dtos.ElementAt(1).CodeText.Should().Be(_secondCode.CodeValue);
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

        [TestMethod]
        public void NullCodeTextFromDTOShouldConvertToEmptyText()
        {
            var itemDTO = new CatalogItemDTO(null, EggTitle);
            var emptyCode = new CatalogItemCode(String.Empty);

            _itemAssembler.FromDTO(itemDTO, _itemFactory.Object);

            _itemFactory.Verify(x => x.CreateCatalogItem(
                It.Is<CatalogItemCode>(code => code.Equals(emptyCode)),
                It.IsAny<string>()
                ));
        }


        private Mock<CatalogItem> CreateCatalogItemMock(CatalogItemCode code)
        {
            return CreateCatalogItemMock(code, AutoGenerator.Create<string>());
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
