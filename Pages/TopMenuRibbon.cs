using OpenQA.Selenium;

namespace PlanitTestSolution.Pages
{
    public class TopMenuRibbon
    {
        readonly string ShopButtonId = "nav-shop";
        readonly string ContactButtonXPath = "//*[@id='nav-contact']";
        readonly string CartMenuId = "nav-cart";
        private IWebDriver _driver;
        public TopMenuRibbon(IWebDriver driver)
        {
            _driver = driver;
        }

        public CartPage ClickCartMenu()
        {
            var CartButton = _driver.FindElement(By.Id(CartMenuId));
            CartButton.Click();
            return new CartPage(_driver);
        }

        public ShoppingPage ClickShopButton()
        {
            var ShoppingButton = _driver.FindElement(By.Id(ShopButtonId));
            ShoppingButton.Click();
            return new ShoppingPage(_driver);
        }

        public FeedbackPage ClickContactButton()
        {
            var ContactButton = _driver.FindElement(By.XPath(ContactButtonXPath));
            ContactButton.Click();
            return new FeedbackPage(_driver);
        }
    }
}
