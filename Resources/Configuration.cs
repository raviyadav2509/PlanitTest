using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlanitTestSolution.Resources
{
    public class Configuration
    {
        public static object DesiredCapabilities;
        public static object DriverServices;
        public static double PageLoadTimeout = 30000;
        public static string START_URL = "http://jupiter.cloud.planittesting.com";
        public string DRIVER_EXE_FOLDER
        {
            get
            {
                return Path.GetFullPath(Path.Combine(
                    Path.GetFullPath(Assembly.GetExecutingAssembly()
                    .Location), @"..\..\..\DriverExecutables"));
            }
        }
    }
}
