using CleanCart.Domain;

namespace CleanCart.Persistence.FakeInMemory.Entities
{
    public class InMemoryCatalogItem : CatalogItem
    {
        public override string Title { get; protected set; }
    }
}