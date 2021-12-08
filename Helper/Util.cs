using OpenQA.Selenium;
using System.IO;
using System.Reflection;

namespace PlanitTestSolution.Helper
{
    public class Util
    {
        #region Miscellanous - Get absolute folder path, Take screenshot
        public static string GetFolderPathInProjectRoot(string dirName)
        {
            return Path.Combine(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location), $@"..\..\..\{dirName}\");
        }

        public static void TakeScreenshot(IWebDriver driver, string saveLocation)
        {
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(saveLocation, ScreenshotImageFormat.Png);
        }

        #endregion
    }
}
