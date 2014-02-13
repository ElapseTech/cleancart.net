namespace CleanCart.Domain
{
    public interface ICatalogItemRepository
    {
        void Persist(StandardCatalogItem standardCatalogItem);
    }
}