namespace CleanCart.ApplicationServices.Dto
{
    public sealed class CatalogItemDTO
    {
        public CatalogItemDTO(string codeText, string title)
        {
            Title = title;
            CodeText = codeText;
        }

        public string Title { get; private set; }
        public string CodeText { get; private set; }
    }
}