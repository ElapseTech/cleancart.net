using System.Data.Entity.ModelConfiguration;
using CleanCart.Persistence.EntityFramework.Entities;

namespace CleanCart.Persistence.EntityFramework.Configurations
{
    class EntityFrameworkCatalogItemConfiguration : EntityTypeConfiguration<EntityFrameworkCatalogItem>
    {
        public EntityFrameworkCatalogItemConfiguration()
        {
            ToTable("CatalogItem");
            HasKey(x => x.Id);

            Ignore(x => x.Code);

        }
    }
}
