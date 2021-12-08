using OpenQA.Selenium;
using PlanitTestSolution.Helper;
using PlanitTestSolution.Resources;

namespace PlanitTestSolution.Pages
{
    public class HomePage : BasePage
    {
        private IWebDriver _driver;
        protected override By IsPageLoadedBy => By.Id("nav-contact");

        public HomePage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(Configuration.START_URL);
            Logger.WriteLog("Jupiter Cloud Landing Page");
        }
    }
}
