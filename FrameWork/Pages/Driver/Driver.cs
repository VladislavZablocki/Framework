using OpenQA.Selenium;
using System.Collections.Generic;

namespace Pages
{
    public class Driver
    {
        private Driver()
        { }

        private static AllDrivers driver;

        private static Dictionary<string, IWebDriver> driverDictionaryInstance;
        
        public static void AddToDictionary(string methodName)
        {
            if (driverDictionaryInstance == null)
            {
                driverDictionaryInstance = new Dictionary<string, IWebDriver>();
            }
            driverDictionaryInstance.Add(methodName, StaticDriverFactory.GetWebDriver(driver));
        }

        public static void SetDriver(AllDrivers neededDriver)
        {
            driver = neededDriver;
        }

        public static IWebDriver GetDriver(string methodName)
        {
            return driverDictionaryInstance[methodName];
        }
    }
}
