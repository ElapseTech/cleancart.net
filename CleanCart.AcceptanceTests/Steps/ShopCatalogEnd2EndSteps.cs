using CleanCart.AcceptanceTests.Fixtures;
using FluentAssertions;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace CleanCart.AcceptanceTests.Steps
{
    /// <summary>
    /// Those steps are for end-to-end tests. 
    /// 
    /// The range of the test is from UI down to the DB all integrated. 
    ///     - Input are made by piloting the UI (Selenium).
    ///     - Verifications are made on the resulting UI (Selenium) or by cheching directly in the DB
    /// 
    /// It means that they use a real web server, with a real DB (TODO) 
    /// 
    /// WARNING: Those tests are more fragile and don't reset since we use a real published Web Server. So, 
    /// we should try to use them as little as possible.
    /// </summary>
    [Binding, Scope(Tag = "Web")]
    class ShopCatalogEnd2EndSteps
    {
        private const string NoTitle = "";
        private const string NoCode = "";

        private readonly Fixture _autoGenerator = new Fixture();
        private AddShopCatalogFixture _addShopCatalogFixture;

        private string _lastAddedItemTitle;


        [AfterScenario]
        public void CloseBrowser()
        {
            if (_addShopCatalogFixture != null)
            {
                _addShopCatalogFixture.CloseBrowser();
            }
        }

        [Given(@"The shop catalog page")]
        public void GivenTheShopCatalogPage()
        {
            _addShopCatalogFixture = new AddShopCatalogFixture();
        }

        [When(@"I add a new item")]
        public void WhenIAddANewItem()
        {
            var itemCode = CreateItemCodeText();
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(itemCode, itemTitle);
            _lastAddedItemTitle = itemTitle;
        }

        [When(@"I add a new item with no title")]
        public void WhenIAddANewItemWithNoTitle()
        {
            var itemCode = CreateItemCodeText();
            FillInCatalogItemFormAndSubmitIt(itemCode, NoTitle);
        }

        [When(@"I add a new item with no item code")]
        public void WhenIAddANewItemWithNoItemCode()
        {
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(NoCode, itemTitle);
        }

        [When(@"I add a new item with the code '(.*)'")]
        public void WhenIAddANewItemWithTheCode(string itemCode)
        {
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(itemCode, itemTitle);
            _lastAddedItemTitle = itemTitle;
        }


        [Then(@"the item is shown in the catalog")]
        public void ThenTheItemIsAddedToTheCatalog()
        {
            AssertCatalogItemWithCodeIsShown(_lastAddedItemTitle);
        }

        [Then(@"I can add another item")]
        public void ThenICanAddAnotherItem()
        {
            var itemCode = CreateItemCodeText();
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(itemCode, itemTitle);

            AssertCatalogItemWithCodeIsShown(itemTitle);
        }


        private string CreateItemCodeText()
        {
            return _autoGenerator.Create("Code");
        }

        private string CreateItemTitle()
        {
            return _autoGenerator.Create("Title");
        }

        private void FillInCatalogItemFormAndSubmitIt(string itemCode, string itemTitle)
        {
            _addShopCatalogFixture.NavigateToForm();
            _addShopCatalogFixture.FillInItemCode(itemCode);
            _addShopCatalogFixture.FillInTitle(itemTitle);
            _addShopCatalogFixture.SubmitForm();
        }

        private void AssertCatalogItemWithCodeIsShown(string itemTitle)
        {
            var codeShown = _addShopCatalogFixture.CheckItemShown(itemTitle);
            codeShown.Should().BeTrue();
        }

    }
}
