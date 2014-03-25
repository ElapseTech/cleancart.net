using System.Data.Entity;
using CleanCart.Domain;
using CleanCart.Persistence.EntityFramework.Configurations;
using CleanCart.Persistence.EntityFramework.Entities;

namespace CleanCart.Persistence.EntityFramework
{
    public class ShopDbContext : DbContext
    {
        public IDbSet<EntityFrameworkCatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new EntityFrameworkCatalogItemConfiguration());
        }
    }
}
