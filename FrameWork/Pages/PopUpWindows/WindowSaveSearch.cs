using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace Pages
{
    public class WindowSaveSearch
    {
        IWebDriver driver;

        [FindsBy(How = How.XPath, Using = @"//input[contains(@id,'searchFacetsControl_saveSearchPopup_txtSaveSearchName')]")]
        public IWebElement SearchNameTextBox { get; set; }

        [FindsBy(How = How.XPath, Using = @"//input[contains(@id,'searchFacetsControl_saveSearchPopup_saveSearch')]")]
        public IWebElement SaveSearchButton { get; set; }

        [FindsBy(How= How.XPath,Using = @"//button[contains(@onclick,'closeSaveSearchSuccessWindow()')]")]
        public IWebElement CloseWindowButton { get; set; }

        public WindowSaveSearch(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
    }
}
