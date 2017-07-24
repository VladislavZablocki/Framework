using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Pages
{
    public static class StaticDriverFactory
    {
        public static IWebDriver GetWebDriver(AllDrivers requiredDriver)
        {
            IWebDriver driver = null;
            switch (requiredDriver)
            {
                case AllDrivers.Chrome:
                    driver = new ChromeDriver();
                    break;
                case AllDrivers.Edge:
                    driver = new EdgeDriver();
                    break;
                case AllDrivers.FireFox:
                    driver = new FirefoxDriver();
                    break;
            }
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
