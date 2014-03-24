using System;

namespace CleanCart.Domain
{
    public abstract class CatalogItem
    {
        public abstract String Title { get; protected set; }
        public abstract ItemCode Code { get; protected set; }
    }
}