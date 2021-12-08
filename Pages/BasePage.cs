using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PlanitTestSolution.Helper;
using System;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace PlanitTestSolution.Pages
{
    public abstract class BasePage : TopMenuRibbon
    {
        private static IWebDriver _driver;
        protected static WebDriverWait Wait = null;
        protected abstract By IsPageLoadedBy { get; }

        protected BasePage(IWebDriver driver) : base(driver)
        {
            SetWait(driver, 30);
            if (IsDocumentReady())
                PageFactory.InitElements(driver, this);
            else
                throw new Exception("Page not loaded correctly(BasePage-constructor)");
            _driver = driver;
        }

        public static void SetWait(IWebDriver driver, int WaitForElementInSeconds)
        {
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WaitForElementInSeconds));
        }

        public bool IsPageLoaded()
        {
            try
            {
                if (IsDocumentReady())
                    Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(IsPageLoadedBy));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IWebElement GetElement(By by)
        {
            try
            {
                bool pageLoad = IsDocumentReady();
                Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
                Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_driver.FindElement(by)));
                Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                return Wait.Until<IWebElement>(d => d.FindElement(by));
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message);
                return null;
            }
        }

        protected IList<IWebElement> GetElementsByXPath(string selector)
        {
            IList<IWebElement> ele =
                Wait.Until<IList<IWebElement>>(d => d.FindElements(By.XPath(selector)));
            return ele;
        }

        public bool IsDocumentReady()
        {
            return Wait.Until(driver =>
            {
                bool isDocumentReady = false;
                try
                {
                    isDocumentReady = (bool)((IJavaScriptExecutor)driver).
                    ExecuteScript("return document.readyState").
                    ToString().
                    Equals("complete");
                }
                catch (Exception E)
                {
                    Logger.WriteLog("Document Not Ready - \n" + E.Message);
                    return isDocumentReady;
                }
                return isDocumentReady;
            });
        }

        public class SaveFeedback<TCurrent, TNext>
        {
            public TCurrent Current { get; set; }
            public TNext Next { get; set; }

            public bool HasNext => Next != null;

            public SaveFeedback(TCurrent current, TNext next)
            {
                Current = current;
                Next = next;
            }
        }
    }
}
