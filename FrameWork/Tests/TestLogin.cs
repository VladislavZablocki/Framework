using Pages;
using NUnit.Framework;
using System.Configuration;
using OpenQA.Selenium;
using System.Reflection;
using System;

namespace Tests
{
    [TestFixture]
    public class TestLogin
    {

        [TestFixtureSetUp]
        public void CreateDictionary()
        {
            Type type = typeof(TestLogin);
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
        public void Login_ValidLoginValidPassword_Enter()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            LoginPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
                .LoginAs(ConfigurationManager.AppSettings["ValidLogin"],
                ConfigurationManager.AppSettings["ValidPassword"]);
            bool actual = page.IsValidLogin();
            page.Close();
            Assert.AreEqual(true, actual);
        }

        [Test]
        [Parallelizable]
        public void Login_ValidLoginInvalidPassword_DontEnter()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            LoginPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
                .LoginAs(ConfigurationManager.AppSettings["ValidLogin"],
                ConfigurationManager.AppSettings["InvalidPassword"]);
            bool actual = page.IsValidLogin();
            page.Close();
            Assert.AreEqual(false, actual);
        }

        [Test]
        [Parallelizable]
        public void Login_InvalidLoginValidPassword_DontEnter()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            LoginPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
                .LoginAs(ConfigurationManager.AppSettings["InvalidLogin"],
                ConfigurationManager.AppSettings["ValidPassword"]);
            bool actual = page.IsValidLogin();
            page.Close();
            Assert.AreEqual(false, actual);
        }

        [Test]
        [Parallelizable]
        public void Login_InvalidLoginInvalidPassword_DontEnter()
        {
            IWebDriver driver = Driver.GetDriver(MethodBase.GetCurrentMethod().Name);
            LoginPage page = LogicSteps.NavigateToPage(driver, ConfigurationManager.AppSettings["url"])
               .LoginAs(ConfigurationManager.AppSettings["InvalidLogin"],
               ConfigurationManager.AppSettings["InvalidPassword"]);
            bool actual = page.IsValidLogin();
            page.Close();
            Assert.AreEqual(false, actual);
        }
    }
}
