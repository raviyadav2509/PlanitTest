using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace PlanitTestSolution.Pages
{
    public class ShoppingPage : BasePage
    {
        private IWebDriver _driver;
        string CartMenuId = "nav-cart";
        IDictionary<string, double> shoppingPageItemPrices { get; set; } 

        protected override By IsPageLoadedBy => By.Id(CartMenuId);

        public ShoppingPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            shoppingPageItemPrices = new Dictionary<string, double>();
        }

        public ShoppingPage AddItemsToTheCart(string itemName, int quantity)
        {
            IsDocumentReady();
            SaveSingleItemPrice(itemName);
            while (quantity > 0)
            {
                _driver.FindElement(By.XPath($"//h4[text()='{itemName}']/../p/a")).Click();
                quantity--;
            }
            return this;
        }

        public void SaveSingleItemPrice(string itemName)
        {
            string singleItemPrice = _driver.FindElement(By.XPath($"//h4[text()='{itemName}']/..//span")).Text;
            double price = Convert.ToDouble(singleItemPrice.Substring(1));
            shoppingPageItemPrices.Add(itemName, price);
        }

        public double GetSingleItemPrice(string itemName)
        {
            return shoppingPageItemPrices[itemName];
        }
    }
}
