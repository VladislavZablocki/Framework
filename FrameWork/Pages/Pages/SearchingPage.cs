using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace Pages
{
    public class SearchingPage : BasePage
    {
        [FindsBy(How=How.XPath,Using = @"//div[@class='resultCount']")]
        public IWebElement SearchingResultsCount { get; set; }

        [FindsBy(How = How.XPath, Using = @"//div[@class='wp-feature-articles']/div/article")]
        public IList<IWebElement> SearchingResults { get; set; }

        [FindsBy(How = How.XPath,Using = @"(//input[@value='Save Search'])[1]")]
        public IWebElement SaveButton { get; set; }

        [FindsBy(How = How.XPath, Using = @"//span[contains(@id,'UserActionsToolbar_lblAccount')]")]
        public IWebElement UserActionToolBar { get; set; }

        [FindsBy(How = How.XPath, Using = @"//a[contains(@id,'ucUserActionsToolbar_lnkSavedSearches')]")]
        public IWebElement SaveFolderToolBarButton { get; set; }

        public SearchingPage(IWebDriver driver)
        {
            this.driver = driver;
            this.Wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(20));
            Wait.Until(ExpectedConditions.ElementExists(By.XPath(@"//div[@id='main-container-content']")));
            PageFactory.InitElements(this.driver, this);
        }
    }
}
