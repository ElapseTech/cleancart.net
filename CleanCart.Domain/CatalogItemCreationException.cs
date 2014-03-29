using System;

namespace CleanCart.Domain
{
    public class CatalogItemCreationException : Exception
    {
        public CatalogItemCreationException(string message) : base(message)
        {
        }
    }
}