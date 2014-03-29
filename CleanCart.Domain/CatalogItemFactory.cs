using System;

namespace CleanCart.Domain
{
    public abstract class CatalogItemFactory
    {
        public CatalogItem CreateCatalogItem(CatalogItemCode code, string title)
        {
            ValidateCodeNotEmpty(code);
            ValidateTitleNotEmpty(title);

            return CreateConcreteCatalogItem(code, title);
        }

        private static void ValidateTitleNotEmpty(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                throw new CatalogItemCreationException("Title must not be empty");
            }
        }

        private static void ValidateCodeNotEmpty(CatalogItemCode code)
        {
            if (code.Equals(new CatalogItemCode("")))
            {
                throw new CatalogItemCreationException("Code must not be empty");
            }
        }

        protected abstract CatalogItem CreateConcreteCatalogItem(CatalogItemCode code, string title);
    }
}