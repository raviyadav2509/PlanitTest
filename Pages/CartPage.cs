using OpenQA.Selenium;
using System;

namespace PlanitTestSolution.Pages
{
    public class CartPage : BasePage
    {
        private IWebDriver _driver;
        string TotalPriceXPath = "//td/strong";
        protected override By IsPageLoadedBy => By.XPath(TotalPriceXPath);

        public CartPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public bool IsItemPresentInCart(string itemName, int quantity)
        {
            IsDocumentReady();
            var ele = _driver.FindElement(By.XPath($"//td[contains(text(), '{itemName}')]/..//input"));
            int quant = Convert.ToInt32(ele.GetAttribute("value"));
            return quantity == quant;
        }

        internal double GetSingleItemPrice(string itemName)
        {
            string singleItemPrice = _driver.FindElement(By.XPath($"//td[contains(text(), '{itemName}')]/../td[2]")).Text;
            double price = Convert.ToDouble(singleItemPrice.Substring(1));
            return price;
        }

        public double GetItemSubTotalPrice(string itemName)
        {
            string singleItemPrice = _driver.FindElement(By.XPath($"//td[contains(text(), '{itemName}')]/../td[4]")).Text;
            double price = Convert.ToDouble(singleItemPrice.Substring(1));
            return price;
        }

        public double GetTotalPrice()
        {
            string totalPrice = _driver.FindElement(By.XPath(TotalPriceXPath)).Text;
            totalPrice = totalPrice.Substring(7);
            return Convert.ToDouble(totalPrice);
        }
    }
}
