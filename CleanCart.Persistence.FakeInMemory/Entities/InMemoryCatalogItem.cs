using CleanCart.Domain;

namespace CleanCart.Persistence.FakeInMemory.Entities
{
    public sealed class InMemoryCatalogItem : CatalogItem
    {
        public InMemoryCatalogItem(ItemCode code, string title)
        {
            Title = title;
            Code = code;
        }

        public override string Title { get; protected set; }
        public override ItemCode Code { get; protected set; }
    }
}