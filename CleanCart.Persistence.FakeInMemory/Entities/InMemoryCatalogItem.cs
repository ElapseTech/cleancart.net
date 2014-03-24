using CleanCart.Domain;

namespace CleanCart.Persistence.FakeInMemory.Entities
{
    public sealed class InMemoryCatalogItem : CatalogItem
    {
        public InMemoryCatalogItem(string title)
        {
            Title = title;
        }

        public override string Title { get; protected set; }
    }
}