using OpenQA.Selenium;

namespace PlanitTestSolution.Pages
{
    public class SubmissionPage : BasePage
    {
        private IWebDriver _driver = null;
        static string SubmissionMessageXpath = "//div[@class='alert alert-success']";
        protected override By IsPageLoadedBy => By.XPath(SubmissionMessageXpath);

        public SubmissionPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public bool VerifySubmissionMessage(string forename)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.XPath(SubmissionMessageXpath)));
            return _driver.FindElement(By.XPath(SubmissionMessageXpath)).Text
                .Equals($"Thanks {forename}, we appreciate your feedback.") ? true : false;
        }
    }
}
