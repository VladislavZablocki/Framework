using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages
{
    public class WindowOperationStatus
    {
        IWebDriver driver;

        [FindsBy(How = How.XPath,Using = @"//input[contains(@name,'btnAddToMyCollection')]")]
        public IWebElement GoToFavoritesButton { get; set; }

        public WindowOperationStatus(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
    }
}
