using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium_BDCLPM.Product
{
    public class Search()
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
        public void SearchV()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Search vào thanh tìm kiếm//
            driver.FindElement(By.Name("search_text")).SendKeys("Amazfit GTS 3 Smart Watch for Android iPhone");
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[3]/form/button")).Click();
        }

        [Test]
        public void Search101()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Search vào thanh tìm kiếm//
            driver.FindElement(By.Name("search_text")).SendKeys("101");
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[3]/form/button")).Click();
            Thread.Sleep(1000);

            //Kiểm tra xem thông báo//
            try
            {
                var resultTest = driver.FindElement(By.XPath("//*[contains(text(), 'No result found')]"));
                if (resultTest.Displayed)
                {
                    Console.WriteLine("Không tìm thấy kết quả");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Tìm thấy kết quả '101')");
            }
        }

        [Test]
        public void Search2()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Search vào thanh tìm kiếm//
            driver.FindElement(By.Name("search_text")).SendKeys("@@");
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[3]/form/button")).Click();
            Thread.Sleep(1000);

            //Kiểm tra xem thông báo//
            try
            {
                var resultTest = driver.FindElement(By.XPath("//*[contains(text(), 'No result found')]"));
                if (resultTest.Displayed)
                {
                    Console.WriteLine("Không tìm thấy kết quả");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Tìm thấy kết quả '@@')");
            }
        }

        [Test]
        public void SearchJM()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Search vào thanh tìm kiếm//
            driver.FindElement(By.Name("search_text")).SendKeys("Gia Mẫn");
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[3]/form/button")).Click();
            Thread.Sleep(1000);

            //Kiểm tra xem thông báo//
            driver.FindElement(By.XPath("//*[contains(text(), 'Fatal error')]"));

            try
            {
                var errorMessage = driver.FindElement(By.XPath("//*[contains(text(), 'Fatal error')]"));
                if (errorMessage.Displayed)
                {
                    Console.WriteLine("Có lỗi Fatal xuất hiện trên trang.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Không có lỗi Fatal nào trên trang.");
            }
        }

        [Test]
        public void Search0()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Search vào thanh tìm kiếm//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[3]/form/button")).Click();
            Thread.Sleep(1000);

        }
    }
}
