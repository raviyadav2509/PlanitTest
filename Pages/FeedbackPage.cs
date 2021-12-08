using OpenQA.Selenium;
using PlanitTestSolution.Helper;
using SeleniumExtras.WaitHelpers;

namespace PlanitTestSolution.Pages
{
    public  class FeedbackPage : BasePage
    {
        private IWebDriver _driver;
        protected override By IsPageLoadedBy => By.Id(SubmitButtonXPath);
        string SubmitButtonXPath = "//*[@class='btn-contact btn btn-primary']";
        string ForeNameTextFieldId = "forename";
        string EmailTextFieldId = "email";
        string MessageTextFieldId = "message";
        string GenericErrorXPath = "//*[contains(@id, 'err')]";
        string ForeNameErrorId = "forename-err";
        string EmailErrorId = "email-err";
        string MessageErrorId = "message-err";
        string MainErrorXpath = "//div[@class='alert alert-error ng-scope']";

        public FeedbackPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            Logger.WriteLog("Feedback Page displayed");
        }

        public SaveFeedback<FeedbackPage, SubmissionPage> ClickSubmitButton()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(SubmitButtonXPath)));
            var ele = _driver.FindElement(By.XPath(SubmitButtonXPath));
            ele.SendKeys("");
            ele.Click();

            return GetElementsByXPath(GenericErrorXPath).Count > 0 ?
                new SaveFeedback<FeedbackPage, SubmissionPage>(this, null) :
                new SaveFeedback<FeedbackPage, SubmissionPage>(this, new SubmissionPage(_driver));
        }

        public bool ValidateForenameErrorMessage()
        {
            var ForeNameError = _driver.FindElement(By.Id(ForeNameErrorId));
            return ForeNameError.Text.Equals("Forename is required") ? true : false;
        }

        public bool ValidateEmailErrorMessage()
        {
            var EmailError = _driver.FindElement(By.Id(EmailErrorId));
            return EmailError.Text.Equals("Email is required") ? true : false;
        }

        public bool ValidateMessageError()
        {
            var MessageError = _driver.FindElement(By.Id(MessageErrorId));
            return MessageError.Text.Equals("Message is required") ? true : false;
        }

        public bool ValidateMainErrorMessage()
        {
            var MainErrorElement = _driver.FindElement(By.XPath(MainErrorXpath));
            return
                MainErrorElement.Text.Equals("We welcome your feedback - " +
                "but we won't get it unless you complete the form correctly.") ?
                true : false;
        }

         public FeedbackPage EnterForename(string forename)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(ForeNameTextFieldId)));
            _driver.FindElement(By.Id(ForeNameTextFieldId)).SendKeys(forename);
            return this;
        }

        public FeedbackPage EnterEmail(string EmailValue)
        {
            _driver.FindElement(By.Id(EmailTextFieldId)).SendKeys(EmailValue);
            return this;
        }

        public FeedbackPage EnterMessage(string MessageValue)
        {
            _driver.FindElement(By.Id(MessageTextFieldId)).SendKeys(MessageValue);
            return this;
        }
    }
}