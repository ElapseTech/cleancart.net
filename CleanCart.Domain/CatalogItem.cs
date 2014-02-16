using System;

namespace CleanCart.Domain
{
    public abstract class CatalogItem
    {
        public abstract String Title { get; protected set; }
    }
}