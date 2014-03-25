using System;
using CleanCart.Domain;

namespace CleanCart.Persistence.EntityFramework.Entities
{
    public sealed class EntityFrameworkCatalogItem : CatalogItem
    {
        public int Id { get; set; }

        public override string Title { get; protected set; }
        public override CatalogItemCode Code { get; protected set; }

        public string CodeValue
        {
            get { return Code.CodeValue; }
            set { Code = new CatalogItemCode(value);}
        }

        public EntityFrameworkCatalogItem()
        {
        }

        public EntityFrameworkCatalogItem(CatalogItemCode code, string title)
        {
            Code = code;
            Title = title;
        }
    }
}
