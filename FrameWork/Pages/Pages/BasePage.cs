using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Pages
{
    public abstract class BasePage
    {
        public IWebDriver driver;
        public WebDriverWait Wait;
    }
}
