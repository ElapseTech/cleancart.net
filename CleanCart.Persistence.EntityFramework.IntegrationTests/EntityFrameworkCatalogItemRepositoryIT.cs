using System;
using System.Data.Entity;
using System.IO;
using CleanCart.Domain;
using CleanCart.Persistence.EntityFramework.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCrunch.Framework;

namespace CleanCart.Persistence.EntityFramework.IntegrationTests
{
    [TestClass, ExclusivelyUses("shop-database")]
    public class EntityFrameworkCatalogItemRepositoryIT
    {
        private EntityFrameworkCatalogItemRepository _repository;

        public EntityFrameworkCatalogItemRepositoryIT()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopDbContext>());
        }

        [TestMethod]
        public void CanPersistACatalogItem()
        {
            InitializeDatabase();

            _repository.Persist(new EntityFrameworkCatalogItem(new CatalogItemCode("I1"), "asdf"));

            _repository.FindByItemCode(new CatalogItemCode("I1"));
        }

        private void InitializeDatabase()
        {
            var provider = new DbContextProvider();
            var context = new ShopDbContext();

            provider.ConfigureShopDbContext(context);
            _repository = new EntityFrameworkCatalogItemRepository(provider);
        }
    }
}
