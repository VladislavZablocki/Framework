using Pages;
using NUnit.Framework;
using System.Configuration;
using System.Reflection;
using System;
using OpenQA.Selenium;
using Logic;

namespace Tests
{
    [TestFixture,Category("Search")]
    class TestSearch
    {
        [TestFixtureSetUp]
        public void CreateDictionary()
        {
            Type type = typeof(TestSearch);
            Driver.SetDriver((AllDrivers)Enum.Parse(typeof(AllDrivers), ConfigurationManager.AppSettings["Driver"]));
            foreach (var method in type.GetMethods())
            {
                if (method.Name != "CreateDictionary" && method.ReturnType.Name == "Void")
                {
                    Driver.AddToDictionary(method.Name);
                }
            }
        }

        [Test]
        [Parallelizable]
        public void SearchWord_ResultsMoreThanHundred_True()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            SearchingPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
                .LoginAs(ConfigurationManager.AppSettings["ValidLogin"], ConfigurationManager.AppSettings["ValidPassword"])
                .ChooseBeginningSymbol(ConfigurationManager.AppSettings["BeginningSymbol"])
                .GoToJournal(ConfigurationManager.AppSettings["JournalPlasticAndReconstructiveSurgery"])
                .Search(ConfigurationManager.AppSettings["WordForSearch"]);
            bool actual = page.IsResultsCountMoreThanHundred();
            page.Close();
            Assert.AreEqual(true, actual);
        }

        [Test]
        [Parallelizable]
        public void SearchWord_ResultsCountOnSecondPageSixty_True()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            SearchingPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
               .LoginAs(ConfigurationManager.AppSettings["ValidLogin"], ConfigurationManager.AppSettings["ValidPassword"])
               .ChooseBeginningSymbol(ConfigurationManager.AppSettings["BeginningSymbol"])
               .GoToJournal(ConfigurationManager.AppSettings["JournalPlasticAndReconstructiveSurgery"])
               .Search(ConfigurationManager.AppSettings["WordForSearch"])
               .ChooseNumberOfPage(ConfigurationManager.AppSettings["NumberOfPage"]);
            bool actual = page.IsResultsCountOnPageSixty();
            page.Close();
            Assert.AreEqual(true, actual);
        }

        [Test]
        [Parallelizable]
        public void SearchArticle_ArticleInSearchResults_True()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            SearchingPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
               .LoginAs(ConfigurationManager.AppSettings["ValidLogin"], ConfigurationManager.AppSettings["ValidPassword"])
               .ChooseBeginningSymbol(ConfigurationManager.AppSettings["BeginningSymbol"])
               .GoToJournal(ConfigurationManager.AppSettings["JournalPlasticAndReconstructiveSurgery"])
               .Search(ConfigurationManager.AppSettings["ArticleForSearch"]);
            bool actual = page.IsArticleInSearchResults(ConfigurationManager.AppSettings["ArticleForSearch"]);
            page.Close();
            Assert.AreEqual(true, actual);
        }

        [Test]
        [Parallelizable]
        public void SearchRussianArticle_ArticleInSearchResults_False()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            SearchingPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
               .LoginAs(ConfigurationManager.AppSettings["ValidLogin"], ConfigurationManager.AppSettings["ValidPassword"])
               .ChooseBeginningSymbol(ConfigurationManager.AppSettings["BeginningSymbol"])
               .GoToJournal(ConfigurationManager.AppSettings["JournalPlasticAndReconstructiveSurgery"])
               .Search(ConfigurationManager.AppSettings["RussianArticleForSearch"]);
            bool actual = page.IsArticleInSearchResults(ConfigurationManager.AppSettings["RussianArticleForSearch"]);
            page.Close();
            Assert.AreEqual(false, actual);
        }

        [Test]
        [Parallelizable]
        public void SaveSearch()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            MyFoldersPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
                .LoginAs(ConfigurationManager.AppSettings["ValidLogin"], ConfigurationManager.AppSettings["ValidPassword"])
                .ChooseBeginningSymbol(ConfigurationManager.AppSettings["BeginningSymbol"])
                .GoToJournal(ConfigurationManager.AppSettings["JournalPlasticAndReconstructiveSurgery"])
                .Search(ConfigurationManager.AppSettings["WordForSearch"])
                .Save(ConfigurationManager.AppSettings["SearchNameForSave"])
                .GoToSavedSearchesResultsPage();
            bool actual = page.IsSearchSuccessful(ConfigurationManager.AppSettings["SearchNameForSave"], ConfigurationManager.AppSettings["WordForSearch"]);
            page.Close();
            Assert.AreEqual(true, actual);
        }
    }
}
