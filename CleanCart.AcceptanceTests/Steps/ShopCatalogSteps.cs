using TechTalk.SpecFlow;

namespace CleanCart.AcceptanceTests.Steps
{
    [Binding]
    class ShopCatalogSteps
    {
         [Given(@"A shop a catalog")]
        public void GivenAShopACatalog()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"Some items in the catalog")]
        public void GivenSomeItemsInTheCatalog()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I visit the catalog")]
        public void WhenIVisitTheCatalog()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"all items are shown")]
        public void ThenAllItemsAreShown()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
