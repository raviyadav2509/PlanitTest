using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using PlanitTestSolution.Helper;
using PlanitTestSolution.Pages;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using static PlanitTestSolution.Helper.Enumerations;

namespace PlanitTestSolution
{
    [TestFixture]
    public class BaseTest
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected IWebDriver Driver { get; private set; }
        protected TopMenuRibbon topMenuRibbon;
        protected HomePage homePage;

        [OneTimeSetUp]
        public void TestSuiteSetup()
        {
            Driver = WebDriverSingleton.GetInstance(BrowserType.Chrome);
        }

        [SetUp]
        public virtual void SetUp()
        {
            log.Info("________UiTest_" + TestContext.CurrentContext.Test.Name + "_Started_______");
            Driver.Manage().Window.Maximize();
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            topMenuRibbon = new TopMenuRibbon(Driver);
            homePage = new HomePage(Driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Success)
                Logger.WriteLog($"{TestContext.CurrentContext.Test.MethodName} - PASS");
            else
            {
                Logger.WriteLog($"{TestContext.CurrentContext.Test.MethodName} - FAIL");
                PerformCleanUpFromTest();
            }
            log.Info("________UiTest_" + TestContext.CurrentContext.Test.Name + "_Ended________");
        }

        [OneTimeTearDown]
        public void TestTeardown()
        {
            try
            {

                CleanUpInstances();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }

        public void CleanUpInstances()
        {
            if (Driver != null)
            {
                Driver.Dispose();
                Driver = null;
            }
        }

        private void PerformCleanUpFromTest()
        {
            //Take a screenshot for failed test and log it to the file
            int endIndex = 0;
            string path = Util.GetFolderPathInProjectRoot("Result");
            if (TestContext.CurrentContext.Test.Name.Length > 50)
                endIndex = 50;
            else
                endIndex = TestContext.CurrentContext.Test.Name.Length;
            string method = String.Join("", Regex.Unescape(TestContext.CurrentContext.Test.Name).Split('\"')).Substring(0, endIndex);
            path = $@"{path}{method}.png";
            Logger.WriteLog($"Performing Clean up for the failed test - {method}");
            Util.TakeScreenshot(Driver, path);
            Logger.WriteLog($"Screenshot taken: {path}");
        }
    }
}
