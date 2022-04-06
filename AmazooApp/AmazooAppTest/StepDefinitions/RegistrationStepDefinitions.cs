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
    public class RegistrationStepDefinitions
    {
        IWebDriver webDriver = new ChromeDriver();

        LoginPage loginPage = null;

        [Given(@"I Navigate to the Home page")]
        public void GivenINavigateToTheHomePage()
        {
            webDriver.Navigate().GoToUrl("https://amazooapp.azurewebsites.net/");
            loginPage = new LoginPage(webDriver);
        }


        [When(@"I click to open the menu")]
        public void WhenIClickToOpenTheMenu()
        {
           
            loginPage.menulnk.Click();
        }

        [When(@"i click on Signup")]
        public void WhenIClickOnSignup()
        {
            loginPage.signUplnk.Click();
        }

        [Then(@"I should navigate to the Signup page")]
        public void ThenIShouldNavigateToTheSignupPage()
        {
            Assert.Contains("https://amazooapp.azurewebsites.net/register/register", webDriver.Url.ToLower());
            webDriver.Quit();
            webDriver.Dispose();
        }
    }
}
