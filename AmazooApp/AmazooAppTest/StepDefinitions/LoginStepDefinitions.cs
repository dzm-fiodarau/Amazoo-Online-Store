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
            webDriver.Quit();
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

        [Given(@"I Click to the login page")]
        public void GivenIClickToTheLoginPage()
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
            webDriver.Quit();
            webDriver.Dispose();

        }

        [When(@"I enter <Email> and <Password>")]
        public void WhenIEnterEmailAndPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            loginPage.Login((string)data.Email, (string)data.Password);
        }

        [When(@"I enter '([^']*)' and '([^']*)'")]
        public void WhenIEnterAnd(string p0, string p1)
        {
            loginPage.Login(p0, p1);
        }


        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            loginPage.ClickLogin();
        }
        [When(@"I login")]
        public void WhenILogin()
        {
            loginPage.btnLogin.Click();
        }


        [Then(@"I should Navigate the Login Page")]
        public void ThenIShouldNavigateTheLoginPage()
        {
            Assert.Contains("https://amazooapp.azurewebsites.net/login/login", webDriver.Url.ToLower());
            webDriver.Quit();
            webDriver.Dispose();
        }

        [When(@"I click my account button")]
        public void WhenIClickMyAccountButton()
        {
            loginPage.accountBtn.Click();
        }

        [When(@"I click the logout button")]
        public void WhenIClickTheLogoutButton()
        {
            loginPage.btnLogout.Click();
        }


    }
}
