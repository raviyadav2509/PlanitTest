using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Threading;
using static PlanitTestSolution.Helper.Enumerations;

namespace PlanitTestSolution.Helper
{
    public class WebDriverSingleton
    {
        private static IWebDriver _driver;
        private static ThreadLocal<IWebDriver> ThreadLocalDriver { get; set; }
        public static IWebDriver GetInstance(BrowserType browser)
        {
            if (_driver == null)
            {
                _driver = GetLocalDriver(browser);
            }
            return _driver;
        }

        private static IWebDriver GetLocalDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions options = new ChromeOptions();
                    ThreadLocalDriver = new ThreadLocal<IWebDriver>(() =>
                    {
                        return new ChromeDriver();
                    });
                    break;
                case BrowserType.FireFox:
                    ThreadLocalDriver = new ThreadLocal<IWebDriver>(() =>
                    {
                        return new FirefoxDriver();
                    });
                    break;
                case BrowserType.InternetExplorer:
                    ThreadLocalDriver = new ThreadLocal<IWebDriver>(() =>
                    {
                        return new InternetExplorerDriver();
                    });
                    break;
            }
            return (IWebDriver)ThreadLocalDriver.Value;
        }

    }
}
