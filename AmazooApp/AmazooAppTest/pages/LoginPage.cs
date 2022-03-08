using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazooApp.Test.pages
{
    public class LoginPage
    {
        public IWebDriver WebDriver { get; }

        public LoginPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebElement menulnk => WebDriver.FindElement(By.Id("menu_but"));
        public IWebElement loginlnk => WebDriver.FindElement(By.Id("menu_item_1"));
        public IWebElement txtEmail => WebDriver.FindElement(By.Name("Email"));
        public IWebElement txtPassword => WebDriver.FindElement(By.Name("Password"));
        public IWebElement btnLogin => WebDriver.FindElement(By.Id("loginBtn_Id"));

        public void ClickLogin()
        {
            menulnk.Click();
            loginlnk.Click();
        }

        public void Login(string Email, string Password)
        {
            txtEmail.SendKeys(Email);
            txtPassword.SendKeys(Password);
        }
        public void ClickLoginButton() => btnLogin.Click();

        //public bool IsHomePageAvailable() => HomeLnk.Displayed;


    }
}
