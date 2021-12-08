using System;

namespace PlanitTestSolution.Helper
{
    public class Logger
    {
        public static void WriteLog(string text)
        {
            BaseTest.log.Info(DateTime.Now.ToString("yyyyMMddHHmmss") + $" - {text};");
        }
    }
}
