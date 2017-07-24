using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace Pages
{
    public class MyFoldersPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = @"//a[contains(@id,'lnkDeleteMyCollection')]")]
        public IWebElement DeleteFolderLink { get; set; }

        [FindsBy(How = How.XPath, Using = @"//a[contains(@id,'lnkDeleteMyCollection')]")]
        public IWebElement DeleteSearchLink { get; set; }

        [FindsBy(How = How.XPath, Using = @"//a[contains(@id,'lnkDeleteMyCollection')]")]
        public IWebElement SearchLink { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//button[contains(@onclick,'closeDeleteSearchItemWindow();')])[2]")]
        public IWebElement ClosePopUp { get; set; }

        [FindsBy(How = How.XPath, Using = @"(//input[@value='Delete'])[2]")]
        public IWebElement DeleteButton { get; set; }

        public WindowDeleteFolder WindowDeleteFolder;

        public MyFoldersPage(IWebDriver driver)
        {
            this.driver = driver;
            this.Wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(20));
            Wait.Until(ExpectedConditions.ElementExists(By.XPath(@"//div[@class='ej-box-issue-departments']")));
            PageFactory.InitElements(this.driver, this);
        }
    }
}
