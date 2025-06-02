using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V131.CSS;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium_BDCLPM.Product
{
    public class ProductSort()
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        public void ProductPT()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/product.php");
            Thread.Sleep(2000);

            //Login admin//
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(1000);

            //Click Product Management//
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //Test Phân trang//
            driver.FindElement(By.Name("example1_length")).Click();
            driver.FindElement(By.XPath("//*[@id=\"example1_length\"]/label/select/option[1]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"example1_length\"]/label/select/option[2]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"example1_length\"]/label/select/option[3]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"example1_length\"]/label/select/option[4]")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void ProductGiamDan()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/product.php");
            Thread.Sleep(2000);

            //Login admin//
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(1000);

            //Click Product Management//
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //Test Sắp xếp ID giảm dần//
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[1]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[1]")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void ProductTangDan()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/product.php");
            Thread.Sleep(2000);

            //Login admin//
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(1000);

            //Click Product Management//
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //Test Sắp xếp ID tăng dần//
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[1]")).Click();          
            Thread.Sleep(1000);
        }

        [Test]
        public void ProductName()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/product.php");
            Thread.Sleep(2000);

            //Login admin//
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(1000);

            //Click Product Management//
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //Test ProductName a-z//
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[3]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[3]")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void ProductNameZ()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/product.php");
            Thread.Sleep(2000);

            //Login admin//
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(1000);

            //Click Product Management//
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //Test ProductName z-a//
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[3]")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void ProductOldPrice()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/product.php");
            Thread.Sleep(2000);

            //Login admin//
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(1000);

            //Click Product Management//
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //Click Old Price//
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[4]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[4]")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void ProductCPrice()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/product.php");
            Thread.Sleep(2000);

            //Login admin//
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(1000);

            //Click Product Management//
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //Click (C) Price//
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[5]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[5]")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void ProductQuantity()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/product.php");
            Thread.Sleep(2000);

            //Login admin//
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(1000);

            //Click Product Management//
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //Click Quantity//
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[6]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/thead/tr/th[6]")).Click();
            Thread.Sleep(1000);
        }
    }
}
