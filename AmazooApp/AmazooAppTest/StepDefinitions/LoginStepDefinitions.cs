using AmazooApp.Test.pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AmazooAppTest.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        IWebDriver webDriver = new ChromeDriver();
       // IWebDriver webDriver = new FirefoxDriver();

        LoginPage loginPage = null;
        [Given(@"I launch the Application")]
        public void GivenILaunchTheApplication()
        {
            webDriver.Navigate().GoToUrl("https://localhost:5001/");
            loginPage = new LoginPage(webDriver);
        }

        [Given(@"I click login link")]
        public void GivenIClickLoginLink()
        {
            loginPage.ClickLogin();
        }

        [Given(@"enter the following details")]
        public void GivenEnterTheFollowingDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            loginPage.Login((string)data.Email, (string)data.Password);
        }

        [Given(@"I click the login button")]
        public void GivenIClickTheLoginButton()
        {
            loginPage.ClickLoginButton();
        }

        [Then(@"I should see the home page")]
        public void ThenIShouldSeeTheHomePage()
        {
            Assert.True(webDriver.Url.ToLower().Contains("https://localhost:5001/"));
            webDriver.Dispose();
        }
    }
}
