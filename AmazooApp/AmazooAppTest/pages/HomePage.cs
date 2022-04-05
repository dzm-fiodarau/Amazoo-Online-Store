using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazooAppTest.pages
{
    public class HomePage
    {
        public IWebDriver WebDriver { get; }

        public HomePage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebElement searchBtn => WebDriver.FindElement(By.Id("searchBtn_Id"));
        public IWebElement txtSearch => WebDriver.FindElement(By.Id("search_bar"));

        public IWebElement noSearcResultId => WebDriver.FindElement(By.Id("noProd_Id"));

        public void ClickSearchBtn()
        {
            searchBtn.Click();
        }

        public void Search(string searchTerm)
        {
            txtSearch.SendKeys(searchTerm);
        }

        public bool ProductsIds(string search)
        {
            var temp = WebDriver.FindElement(By.Id(search));
            return temp != null;
        }

        public string NoMatchingProduct()
        {
            return noSearcResultId.Text;
        }
    }
}
