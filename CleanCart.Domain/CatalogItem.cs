namespace CleanCart.Domain
{
    public abstract class CatalogItem
    {
        public abstract string Title { get; protected set; }
        public abstract CatalogItemCode Code { get; protected set; }
    }
}