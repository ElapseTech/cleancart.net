using CleanCart.Domain;

namespace CleanCart.Persistence.FakeInMemory.Entities
{
    public class InMemoryCatalogItem : CatalogItem
    {
        public InMemoryCatalogItem(string title)
        {
            Title = title;
        }

        public override string Title { get; protected set; }
    }
}