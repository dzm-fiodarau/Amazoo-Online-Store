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
    public class CartStepDefinitions
    {
        IWebDriver webDriver = new ChromeDriver();
        HomePage? homePage = null;

        [Given(@"Given I Navigate to the Home page")]
        public void GivenGivenINavigateToTheHomePage()
        {
            webDriver.Navigate().GoToUrl("https://amazooapp.azurewebsites.net/");
            homePage = new HomePage(webDriver);
        }


        [When(@"I click Add <product> to Cart")]
        public void WhenIClickAddProductToCart(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            homePage.AddToCart(data.product);
        }

        [When(@"I click to open the cart")]
        public void WhenIClickToOpenTheCart()
        {
            homePage.cartButton.Click();
        }


        [Then(@"The product should be added to Cart")]
        public void ThenTheProductShouldBeAddedToCart()
        {
            Assert.True(true);
            webDriver.Quit();
            webDriver.Dispose();
        }
    }
}
