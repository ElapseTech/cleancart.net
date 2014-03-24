namespace CleanCart.Domain
{
    public interface ICatalogItemFactory
    {
        CatalogItem CreateCatalogItem(CatalogItemCode code, string title);
    }
}