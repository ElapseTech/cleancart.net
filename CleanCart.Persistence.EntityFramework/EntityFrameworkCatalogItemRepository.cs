using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CleanCart.Domain;
using CleanCart.Persistence.EntityFramework.Entities;

namespace CleanCart.Persistence.EntityFramework
{
    public class EntityFrameworkCatalogItemRepository : ICatalogItemRepository
    {
        private readonly DbContextProvider _provider;

        public EntityFrameworkCatalogItemRepository(DbContextProvider provider)
        {
            _provider = provider;
        }

        public void Persist(CatalogItem catalogItem)
        {
            var item = catalogItem as EntityFrameworkCatalogItem;
            if (item == null)
            {
                throw new ArgumentException("Cannot save a CatalogItem of type '" + catalogItem.GetType() + "' in an Entity Framework repository.");
            }

            if (_provider.GetShopDbContext().Entry(item).State == EntityState.Detached)
            {
                _provider.GetShopDbContext().CatalogItems.Add(item);
            }
            else
            {
                _provider.GetShopDbContext().Entry(item).State = EntityState.Modified;
            }

            _provider.GetShopDbContext().SaveChanges();

        }

        public IList<CatalogItem> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public CatalogItem FindByItemCode(CatalogItemCode catalogItemCode)
        {
            return _provider.GetShopDbContext()
                .CatalogItems.FirstOrDefault(x => x.CodeValue == catalogItemCode.CodeValue);
        }
    }
}
