using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CleanCart.Domain.Tests
{

    [TestClass]
    public class CatalogItemFactoryTest
    {
        private Mock<CatalogItem> _concreteItem;
        private readonly CatalogItemCode _someItemCode = new CatalogItemCode("CODE");
        private readonly CatalogItemCode _emptyCode = new CatalogItemCode("");
        private const string SomeItemTitle = "Some Title";
        private const string EmptyTitle = "";

        private CatalogItemFactory _factory;

        [TestInitialize]
        public void ConfigurePartialCatalogItemFactory()
        {
            _concreteItem = new Mock<CatalogItem>();
            _factory = new FakeCatalogItemFactory(_concreteItem.Object);
        }

        [TestMethod]
        public void ShouldDelegateConcreteItemCreation()
        {
            var createdItem = _factory.CreateCatalogItem(_someItemCode, SomeItemTitle);
            Assert.AreEqual(_concreteItem.Object, createdItem);
        }

        [TestMethod, ExpectedException(typeof(CatalogItemCreationException))]
        public void EmptyCodeShouldThrowCreationException()
        {
            _factory.CreateCatalogItem(_emptyCode, SomeItemTitle);
        }

        [TestMethod, ExpectedException(typeof(CatalogItemCreationException))]
        public void EmptyTitleShouldThrowCreationException()
        {
            _factory.CreateCatalogItem(_someItemCode, EmptyTitle);
        }


    }

    internal class FakeCatalogItemFactory : CatalogItemFactory
    {
        private readonly CatalogItem _catalogItem;

        public FakeCatalogItemFactory(CatalogItem catalogItem)
        {
            _catalogItem = catalogItem;
        }

        protected override CatalogItem CreateConcreteCatalogItem(CatalogItemCode code, string title)
        {
            return _catalogItem;
        }
    }
}
