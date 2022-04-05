using AmazooApp.Test.pages;
using AmazooAppTest.pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;


namespace AmazooAppTest.StepDefinitions
{
    [Binding]
    public class SearchStepDefinitions
    {
        IWebDriver webDriver = new ChromeDriver();
        HomePage homePage = null;

        [Given(@"I Navigate at the Home page")]
        public void GivenINavigateAtTheHomePage()
        {
            webDriver.Navigate().GoToUrl("https://amazooapp.azurewebsites.net/");
             homePage = new HomePage(webDriver);
        }

     
        [When(@"I enter a valid <ProductName> in the search bar")]
        public void WhenIEnterAValidProductNameInTheSearchBar(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            homePage.Search((string)data.ProductName);
        }

        [When(@"I click the Search button")]
        public void WhenIClickTheSearchButton()
        {
            homePage.searchBtn.Click();
        }

        [Then(@"I should see a Page with the matching products")]
        public void ThenIShouldSeeAPageWithTheMatchingProducts()
        {
            Assert.True(homePage.ProductsIds("Jeans"));
            webDriver.Quit();
            webDriver.Dispose();
        }

        [Then(@"I should get ""([^""]*)""")]
        public void ThenIShouldGet(string p0)
        {
            Assert.Equal(homePage.NoMatchingProduct(), p0);
            webDriver.Quit();
            webDriver.Dispose();
        }

    }
}
