using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using Pages;

namespace Logic
{
    static public class LogicSteps
    {
        private static string invalidLoginXpath = @"//li/a[text()='Login']";
        private static string validLoginXpath = @"//li/a[text()='Log Out']";
        private static string loginPageMainContainer = @"//div[@id='main-container-content']";

        public static LoginPage NavigateToPage(IWebDriver driver,string url)
        {
            LoginPage page = new LoginPage(driver);
            page.driver.Navigate().GoToUrl(url);
            return new LoginPage(driver);
        }

        public static LoginPage LoginAs(this LoginPage page, string login, string password)
        {
            page.UserLogin.SendKeys(login);
            page.UserPassword.SendKeys(password);
            page.LoginButton.Click();
            return page;
        }

        public static bool IsValidLogin(this LoginPage page)
        {
            bool result = false;
            try
            {
                page.Wait.Until(ExpectedConditions.ElementExists(By.XPath(invalidLoginXpath)));
            }
            catch (WebDriverTimeoutException)
            {
                result = true;
            }
            return result;
        }

        public static LoginPage ChooseBeginningSymbol(this LoginPage page, string symbol)
        {
            page.Wait.Until(ExpectedConditions.ElementExists(By.XPath(loginPageMainContainer)));
            page.GetBeginningSymbolButtonBySymbol(symbol).Click();
            return page;
        }

        public static JournalPage GoToJournal(this LoginPage page, string name)
        {
            page.Wait.Until(ExpectedConditions.ElementExists(By.XPath(validLoginXpath)));
            page.GetJournalByName(name).Click();
            page.driver.SwitchTo().Window(page.driver.WindowHandles.Last());
            JournalPage journalPage = new JournalPage(page.driver);
            return journalPage;
        }
        
        public static MyFoldersPage AddFirstArticleToFavoritesFromList(this JournalPage page,string articleName, string folderName)
        {
            page.GetAddToFavoritesLinkFromListByArticleName(articleName).Click();
            page.WindowAddToFolder = new WindowAddToFolder(page.driver);
            CreateNewFolder(page.WindowAddToFolder, folderName);
            page.WindowAddToFolder.AddItemButton.Click();
            page.Wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@value='Go to My Favorites']")));
            page.WindowOperationStatus = new WindowOperationStatus(page.driver);
            page.WindowOperationStatus.GoToFavoritesButton.Click();
            return new MyFoldersPage(page.driver);
        }

        public static MyFoldersPage AddFirstArticleToFavoritesFolderFromArticle(this JournalPage page, string articleName, string folderName)
        {
            page.GetArticleLinkByName(articleName).Click();
            ArticlePage firstArticlePage = new ArticlePage(page.driver);
            firstArticlePage.AddToFavorites.Click();
            firstArticlePage.WindowAddToFolder = new WindowAddToFolder(page.driver);
            CreateNewFolder(firstArticlePage.WindowAddToFolder, folderName);
            firstArticlePage.WindowAddToFolder.AddItemButton.Click();
            firstArticlePage.Wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@value='Go to My Favorites']")));
            firstArticlePage.WindowOperationStatus = new WindowOperationStatus(page.driver);
            firstArticlePage.WindowOperationStatus.GoToFavoritesButton.Click();
            return new MyFoldersPage(firstArticlePage.driver);
        }

        public static MyFoldersPage GoFolder(this MyFoldersPage page, string folderName)
        {
            page.GetFolder(folderName).Click();
            return page;
        }

        public static bool IsArticleInFavorites(this MyFoldersPage page, string articleName)
        {
            bool result = true;
            try
            {
                page.Wait.Until(ExpectedConditions.ElementExists(By.XPath(string.Concat("//a[@title='", articleName,"']"))));
            }
            catch (WebDriverException)
            {
                result = false;
            }
            return result;
        }

        public static void DeleteFolder(this MyFoldersPage page)
        {
            page.DeleteFolderLink.Click();
            page.WindowDeleteFolder = new WindowDeleteFolder(page.driver);
            page.WindowDeleteFolder.DeleteButton.Click();
        }

        public static void CreateNewFolder(WindowAddToFolder window, string folderName)
        {
            window.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//span[contains(@id,'lblTitle') and text()='Add Item(s) to:']")));
            window.NewFolderRadioButton.Click();
            window.NewFolderLabel.Click();
            window.InputFolderNameTextbox.SendKeys(folderName);
        }

        public static SearchingPage Search(this JournalPage page, string word)
        {
            page.Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//button[@id='btnGlobalSearchMagnifier']")));
            page.SearchBox.Clear();
            page.SearchBox.SendKeys(word);
            page.SearchButton.Click();
            return new SearchingPage(page.driver);
        }

        public static SearchingPage Save(this SearchingPage page,string nameOfSave)
        {
            page.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//div[@class='searchFacets']")));
            page.SaveButton.Click();
            page.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"(//div[@class='wk-modal-body'])[1]")));
            WindowSaveSearch window = new WindowSaveSearch(page.driver);
            window.SearchNameTextBox.SendKeys(nameOfSave);
            window.SaveSearchButton.Click();
            page.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//span[contains(text(),'has been successfully saved')]")));
            window.CloseWindowButton.Click();
            return page;
        }

        public static MyFoldersPage GoToSavedSearchesResultsPage(this SearchingPage page)
        {
            page.Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(@"//span[contains(@id,'UserActionsToolbar_lblAccount')]")));
            page.UserActionToolBar.Click();
            page.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//a[contains(@id,'ucUserActionsToolbar_lnkSavedSearches')]")));
            page.SaveFolderToolBarButton.Click();
            return new MyFoldersPage(page.driver);
        }

        public static SearchingPage GoToSearchpage(this MyFoldersPage page, string name)
        {
            page.GetLinkForSearch(name).Click();
            return new SearchingPage(page.driver);
        }

        public static bool IsSearchSuccessful(this MyFoldersPage page,string nameSearch,string wordSearch)
        {
            SearchingPage searchingPage = page.GoToSearchpage(nameSearch);
            searchingPage.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//div[@id='main-container-content']")));
            bool result = searchingPage.driver.FindElement(By.XPath(@"//div[@class='searchUserKeywords']")).Text.Contains(wordSearch);
            MyFoldersPage folderPage = searchingPage.GoToSavedSearchesResultsPage();
            folderPage.DeleteSearch(nameSearch);
            return result; 
        }

        public static void DeleteSearch(this MyFoldersPage page, string searchName)
        {
            page.GetDeleteLinkForSearch(searchName).Click();
            page.DeleteButton.Click();
            page.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//span[contains(@id,'ucDeleteSavedSearchItem_searchNameMessage')]")));
            page.ClosePopUp.Click();
        }

        public static bool IsResultsCountMoreThanHundred(this SearchingPage page)
        {
            page.Wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='resultCount']")));
            return int.Parse(page.SearchingResultsCount.Text.Replace("results", string.Empty)) > 100;
        }

        public static bool IsResultsCountOnPageSixty(this SearchingPage page)
        {
            return page.SearchingResults.Count == 60;
        }

        public static bool IsArticleInSearchResults(this SearchingPage page, string articleName)
        {
            page.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(@"//div[@class='searchContent']")));
            return page.driver.FindElements(By.XPath(string.Concat("//article//a[text()='", articleName, "']"))).Count > 0;
        }

        public static SearchingPage ChooseNumberOfPage(this SearchingPage page,string pageNumber)
        {
            page.Wait.Until(ExpectedConditions.ElementExists(By.XPath(@"//section[@id='wpPagingControl']")));
            page.GetNumberPageOfSearch(pageNumber).Click(); 
            return page;
        }

        public static void Close(this BasePage page)
        {
            page.driver.Quit();
        }
    }
}