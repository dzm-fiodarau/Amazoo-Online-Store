using AmazooAppTest.pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AmazooAppTest.StepDefinitions
{
    [Binding]
    public class FilterStepDefinitions
    {
        IWebDriver webDriver = new ChromeDriver();
        HomePage? homePage = null;

        [Given(@"I am at the Home Page")]
        public void GivenIAmAtTheHomePage()
        {
            webDriver.Navigate().GoToUrl("https://amazooapp.azurewebsites.net/");
            homePage = new HomePage(webDriver);
        }

        [When(@"A user click on clothing")]
        public void WhenAUserClickOnClothing()
        {
            homePage.clothingChkbx.Click();
        }

        [When(@"i click on ApplyFIlters button")]
        public void WhenIClickOnApplyFIltersButton()
        {
            homePage.filterBtn.Click();
        }

        [Then(@"products should be filtered")]
        public void ThenProductsShouldBeFiltered()
        {
            //we know the number of clothing products is 3
            Assert.Equal(homePage.GetAllProductsCount(), 3);
            webDriver.Quit();
            webDriver.Dispose();
        }

        [When(@"A user click on Nintendo")]
        public void WhenAUserClickOnNintendo()
        {
            homePage.nintendoChkbx.Click();
        }

        [Then(@"products count will be (.*)")]
        public void ThenProductsCountWillBe(int p0)
        {
            Assert.Equal(homePage.GetAllProductsCount(), p0);
            webDriver.Quit();
            webDriver.Dispose();
        }


    }
}
