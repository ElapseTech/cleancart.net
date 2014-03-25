namespace CleanCart.Persistence.EntityFramework
{
    public class DbContextProvider
    {
        private ShopDbContext _shopDbContext;

        public void ConfigureShopDbContext(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public ShopDbContext GetShopDbContext()
        {
            return _shopDbContext;
        }
    }
}
