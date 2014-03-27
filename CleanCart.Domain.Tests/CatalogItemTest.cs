
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleanCart.Domain.Tests
{
    [TestClass]
    class CatalogItemTest
    {

        [TestMethod, ExpectedException(typeof(Exception))] // TODO FluentValiation
        public void CreatingCtalogItemWilNotTitleShouldFail()
        {
        }

    }

    internal class CannotAddItemException : Exception
    {
    }
}
