using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages
{
    public class JournalPage : BasePage
    {
        public WindowOperationStatus WindowOperationStatus; 
        public WindowAddToFolder WindowAddToFolder;

        [FindsBy(How = How.XPath,Using = @"(//input[contains(@name,'SearchBox')])[1]")]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.XPath,Using = @"//button[@id='btnGlobalSearchMagnifier']")]
        public IWebElement SearchButton { get; set; }

        public JournalPage(IWebDriver driver)
        {
            this.driver = driver;
            this.Wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(20));
            Wait.Until(ExpectedConditions.ElementExists(By.XPath(@"//div[@id='main-container-content']")));
            PageFactory.InitElements(this.driver, this);
        }
    }
}
