using System.Runtime.InteropServices;
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
        private AddShopCatalogWebFixture _addShopCatalogWebFixture;

        private string _lastAddedItemTitle;


        [AfterScenario]
        public void CloseBrowser()
        {
            if (_addShopCatalogWebFixture != null)
            {
                _addShopCatalogWebFixture.CloseBrowser();
            }
        }


        [Given(@"The shop catalog page")]
        public void GivenTheShopCatalogPage()
        {
            _addShopCatalogWebFixture = new AddShopCatalogWebFixture();
        }

        [Given(@"The add item catalog form")]
        public void GivenTheAddItemCatalogForm()
        {
            GivenTheShopCatalogPage();
        }



        [When(@"I add a new item")]
        public void WhenIAddANewItem()
        {
            var itemCode = CreateItemCodeText();
            var itemTitle = CreateItemTitle();
            FillInCatalogItemFormAndSubmitIt(itemCode, itemTitle);
            _lastAddedItemTitle = itemTitle;
        }

        [When(@"I an error occurs")]
        public void WhenIAnErrorOccurs()
        {
            ScenarioContext.Current.Pending();
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

        [Then(@"the error is shown")]
        public void ThenTheErrorIsShown()
        {
            ScenarioContext.Current.Pending();
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
            _addShopCatalogWebFixture.NavigateToForm();
            _addShopCatalogWebFixture.FillInItemCode(itemCode);
            _addShopCatalogWebFixture.FillInTitle(itemTitle);
            _addShopCatalogWebFixture.SubmitForm();
        }

        private void AssertCatalogItemWithCodeIsShown(string itemTitle)
        {
            var codeShown = _addShopCatalogWebFixture.CheckItemShown(itemTitle);
            codeShown.Should().BeTrue();
        }

    }
}
