using CleanCart.ApplicationServices.Assemblers;
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
        const string TheTitle = "Title";
        const string FirstTitle = "FIRST TITLE";
        const string SecondTitle = "SECOND TITLE";

        private CatalogItemAssembler _assembler;

        [TestInitialize]
        public void SetupAssembler()
        {
            _assembler = new CatalogItemAssembler();
        }

        [TestMethod]
        public void ToDTOShouldMapTheTitle()
        {
            var catalogItem = CreateCatalogItem(TheTitle);
            var dto = _assembler.ToDto(catalogItem.Object);
            dto.Title.Should().Be(TheTitle);
        }

        [TestMethod]
        public void ToDTOListShouldMapAttributesForAllItemsInSameOrder()
        {
            var catalogItems = new List<CatalogItem> {CreateCatalogItem(FirstTitle).Object, 
                                                      CreateCatalogItem(SecondTitle).Object};

            var dtos = _assembler.ToDtoList(catalogItems);

            dtos.ElementAt(0).Title.Should().Be(FirstTitle);
            dtos.ElementAt(1).Title.Should().Be(SecondTitle);
            dtos.Should().HaveCount(catalogItems.Count);
        }

        private Mock<CatalogItem> CreateCatalogItem(string title)
        {
            var item = new Mock<CatalogItem>();
            item.Setup(x => x.Title).Returns(title);
            return item;
        }
    }
}
