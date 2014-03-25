using System.ComponentModel;

namespace CleanCart.ApplicationServices.Dto
{
    public sealed class CatalogItemDTO
    {
        public CatalogItemDTO()
        {
        }

        public CatalogItemDTO(string codeText, string title)
        {
            Title = title;
            CodeText = codeText;
        }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Code")]
        public string CodeText { get; set; }
    }
}