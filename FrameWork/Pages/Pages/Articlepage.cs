using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages
{
    public class ArticlePage : BasePage
    {
       
        [FindsBy(How = How.XPath,Using = @"(//a[contains(@onclick,'ArticleTools_ShowAddToMyCollectionsPopUp();')])[2]")]
        public IWebElement AddToFavorites { get; set; }

        public WindowOperationStatus WindowOperationStatus;
        public WindowAddToFolder WindowAddToFolder;

        public ArticlePage(IWebDriver driver)
        {
            this.driver = driver;
            this.Wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(20));
            Wait.Until(ExpectedConditions.ElementExists(By.XPath(@"//section[@id='wpArticleTools']")));
            PageFactory.InitElements(this.driver, this);
        }
    }
}
