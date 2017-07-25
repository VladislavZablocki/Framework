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
                    driver.Manage().Window.Maximize();
                    break;
                case AllDrivers.Edge:
                    driver = new EdgeDriver();
                    break;
                case AllDrivers.FireFox:
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Size = new System.Drawing.Size(1680,1050);
                    break;
            }
            return driver;
        }
    }
}
