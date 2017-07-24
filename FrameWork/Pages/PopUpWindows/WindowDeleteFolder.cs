using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages
{
    public class WindowDeleteFolder
    {
        IWebDriver driver;
        [FindsBy(How = How.XPath,Using = @"//input[contains(@name,'deleteMyCollectionControl$btnDelete')]")]
        public IWebElement DeleteButton { get; set; }

        public WindowDeleteFolder(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
    }
}
