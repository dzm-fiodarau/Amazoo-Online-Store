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

        LoginPage loginPage = null;

        [Given(@"I am at the Home page")]
        public void GivenIAmAtTheHomePage()
        {
            webDriver.Navigate().GoToUrl("https://amazooapp.azurewebsites.net/");
            loginPage = new LoginPage(webDriver);

        }
        [Given(@"I Navigate to the Login page")]
        public void GivenINavigateToTheLoginPage()
        {
            loginPage.ClickLogin();
        }


        [Then(@"I should navigate the home page")]
        public void ThenIShouldNavigateTheHomePage()
        {
             Assert.Contains("https://amazooapp.azurewebsites.net/", webDriver.Url.ToLower());
            webDriver.Dispose();
        }



        [Given(@"I am the Home Page")]
        public void GivenIAmTheHomePage()
        {
            webDriver.Navigate().GoToUrl("https://amazooapp.azurewebsites.net/");
            loginPage = new LoginPage(webDriver);
        }

        [Given(@"I Go to the Login page")]
        public void GivenIGoToTheLoginPage()
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
            loginPage.ClickLogin();
        }
        [Then(@"I should see the Login Page")]
        public void ThenIShouldSeeTheLoginPage()
        {
            Assert.Contains("https://amazooapp.azurewebsites.net/", webDriver.Url.ToLower());
            webDriver.Dispose();

        }

        [When(@"I enter <Email> and <Password>")]
        public void WhenIEnterEmailAndPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            loginPage.Login((string)data.Email, (string)data.Password);
        }

        //[When(@"I enter Email and Password")]
        //public void WhenIEnterEmailAndPassword(Table table)
        //{
        //    dynamic data = table.CreateDynamicInstance();
        //    loginPage.Login((string)data.Email, (string)data.Password);
        //}

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            loginPage.ClickLogin();
        }


        [Then(@"I should Navigate the Login Page")]
        public void ThenIShouldNavigateTheLoginPage()
        {
            Assert.Contains("https://amazooapp.azurewebsites.net/login/login", webDriver.Url.ToLower());
            webDriver.Dispose();
        }


    }
}
