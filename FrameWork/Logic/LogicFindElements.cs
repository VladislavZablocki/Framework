using OpenQA.Selenium;
using Pages;

namespace Logic
{
    public static class LogicFindElements
    {
        public static IWebElement GetJournalByName(this LoginPage page, string name)
        {
            return page.driver.FindElement(By.XPath(string.Concat("//h4/a[text()='", name, "']")));
        }

        public static IWebElement GetBeginningSymbolButtonBySymbol(this LoginPage page, string symbol)
        {
            return page.driver.FindElement(By.XPath(string.Concat("//div[@id='ej-journals-a-z-alpha-list']/a[text()='", symbol, "']")));
        }

        public static IWebElement GetAddToFavoritesLinkFromListByArticleName(this JournalPage page, string articleName)
        {
            return page.driver.FindElement(By.XPath(string.Concat("//a[@title='", articleName, "']//ancestor::div[1]//a[contains(@onclick,'addToMyCollectionsLinkClicked')]")));
        }

        public static IWebElement GetFolder(this MyFoldersPage page, string folderName)
        {
            return page.driver.FindElement(By.XPath(string.Concat("//tr//a[text()='", folderName, "']")));
        }

        public static IWebElement GetArticleLinkByName(this JournalPage page, string articleName)
        {
            return page.driver.FindElement(By.XPath(string.Concat("//a[@title='", articleName, "']")));
        }

        public static IWebElement GetNumberPageOfSearch(this SearchingPage page, string number)
        {
            return page.driver.FindElement(By.XPath(string.Concat("//div[@class='pagenumbers']/a[text()='", number, "']")));
        }

        public static IWebElement GetDeleteLinkForSearch(this MyFoldersPage page, string name)
        {
            return page.driver.FindElement(By.XPath(string.Concat("//a[contains(@onclick,'", name, "') and contains(text(),'Delete')]")));
        }

        public static IWebElement GetLinkForSearch(this MyFoldersPage page, string name)
        {
            return page.driver.FindElement(By.XPath(string.Concat("//a[contains(@id,'linkMySearch') and text()='", name, "']")));
        }
    }
}
