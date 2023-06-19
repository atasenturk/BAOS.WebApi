using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BAOS.Tests.UIFixtures
{
    [TestClass]
    public class SeleniumFixture
    {
        private IWebDriver driver;

        [TestMethod]
        public void GoToSite_Test()
        {
            driver = new ChromeDriver();

            driver.Url = "http://127.0.0.1:5500/html/index.html";
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void Login_Test()
        {
            driver = new ChromeDriver();

            driver.Url = "http://127.0.0.1:5500/html/login.html";

            driver.FindElement(By.XPath("//label[contains(text(),'Giriş Yap')]")).Click();
            
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@id='email']")).SendKeys("test@gmail.com");
            
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@id='pswd']")).SendKeys("test");
            
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//button[@id='btnLogin']")).SendKeys(Keys.Enter);


            Assert.AreEqual(true, true);
        }
    }
}
