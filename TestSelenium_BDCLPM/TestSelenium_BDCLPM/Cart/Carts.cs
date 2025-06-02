using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium_BDCLPM.Cart
{
    public class Carts
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void addProducts()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Click login//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a")).Click();
            driver.FindElement(By.Name("cust_email")).SendKeys("funan@gmail.com");
            driver.FindElement(By.Name("cust_password")).SendKeys("141216");
            driver.FindElement(By.Name("form1")).Click();

            //click men
            driver.FindElement(By.CssSelector("a[href*='product-category.php?id=1&type=top-category']")).Click();
            Thread.Sleep(1000);

            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[1]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //quantity
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).Clear();
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).SendKeys("1");
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[4]/input")).Click();
            Thread.Sleep(1000);

            //click cart//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[3]/a")).Click();
            Thread.Sleep(1000);

            
        }

        [Test]
        public void AddSimilarProducts()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Click login//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a")).Click();
            driver.FindElement(By.Name("cust_email")).SendKeys("funan@gmail.com");
            driver.FindElement(By.Name("cust_password")).SendKeys("141216");
            driver.FindElement(By.Name("form1")).Click();

            //click men
            driver.FindElement(By.CssSelector("a[href*='product-category.php?id=1&type=top-category']")).Click();
            Thread.Sleep(1000);

            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[1]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //quantity
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).Clear();
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).SendKeys("1");
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[4]/input")).Click();
            Thread.Sleep(1000);

            //click cart//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[3]/a")).Click();
            Thread.Sleep(1000);

            //Click Men//
            driver.FindElement(By.CssSelector("a[href*='product-category.php?id=1&type=top-category']")).Click();
            Thread.Sleep(1000);

            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[1]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //quantity
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).Clear();
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).SendKeys("1");
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[4]/input")).Click();
            Thread.Sleep(1000);

            //Hiện thông báo//
            try
            {
                IAlert alert = driver.SwitchTo().Alert();

                // In nội dung của alert ra console
                Console.WriteLine("Alert Text: " + alert.Text);

                // Nhấn nút "OK" trên alert
                alert.Accept();

                Console.WriteLine("This product is already added to the shopping cart.");
            }
            catch (NoAlertPresentException)
            {
                Console.WriteLine("No alert found.");
            }

        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }


        [Test]
        public void AddProducts0()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Click login//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a")).Click();
            driver.FindElement(By.Name("cust_email")).SendKeys("funan@gmail.com");
            driver.FindElement(By.Name("cust_password")).SendKeys("141216");
            driver.FindElement(By.Name("form1")).Click();

            //click men
            driver.FindElement(By.CssSelector("a[href*='product-category.php?id=1&type=top-category']")).Click();
            Thread.Sleep(1000);

            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[1]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //quantity
            var quantityInput = driver.FindElement(By.Name("p_qty"));
            quantityInput.Clear();
            quantityInput.SendKeys("0");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[4]/input")).Click();
            Thread.Sleep(1000);

            //Hiện thông báo//
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            bool isValid = (bool)js.ExecuteScript("return arguments[0].validity.valid;", quantityInput);

            if (!isValid)
            {
                Console.WriteLine("Thông báo lỗi xuất hiện: Giá trị phải lớn hơn hoặc bằng 1.");
            }
            else
            {
                Console.WriteLine("Không có lỗi.");
            }
        }

        [Test]
        public void AddProducts1() 
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Click login//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a")).Click();
            driver.FindElement(By.Name("cust_email")).SendKeys("funan@gmail.com");
            driver.FindElement(By.Name("cust_password")).SendKeys("141216");
            driver.FindElement(By.Name("form1")).Click();

            //click men
            driver.FindElement(By.CssSelector("a[href*='product-category.php?id=1&type=top-category']")).Click();
            Thread.Sleep(1000);

            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[1]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //quantity
            var quantityInput = driver.FindElement(By.Name("p_qty"));
            quantityInput.Clear();
            quantityInput.SendKeys("-1");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[4]/input")).Click();
            Thread.Sleep(1000);

            //Hiện thông báo//
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            bool isValid = (bool)js.ExecuteScript("return arguments[0].validity.valid;", quantityInput);

            if (!isValid)
            {
                Console.WriteLine("Thông báo lỗi xuất hiện: Giá trị phải lớn hơn hoặc bằng 1.");
            }
            else
            {
                Console.WriteLine("Không có lỗi.");
            }
        }

        [Test]
        public void AddProductsN()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Click login//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a")).Click();
            driver.FindElement(By.Name("cust_email")).SendKeys("funan@gmail.com");
            driver.FindElement(By.Name("cust_password")).SendKeys("141216");
            driver.FindElement(By.Name("form1")).Click();

            //click men
            driver.FindElement(By.CssSelector("a[href*='product-category.php?id=1&type=top-category']")).Click();
            Thread.Sleep(1000);

            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[1]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //quantity
            var quantityInput = driver.FindElement(By.Name("p_qty"));
            quantityInput.Clear();
            quantityInput.SendKeys("e");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[4]/input")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/div[1]/h2"));

            //Hiện thông báo//
            /*IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            bool isValid = (bool)js.ExecuteScript("return arguments[0].validity.valid;", quantityInput);

            if (!isValid)
            {
                Console.WriteLine("Thông báo lỗi xuất hiện: Vui lòng nhập một số.");
            }
            else
            {
                Console.WriteLine("Không có lỗi.");
            }*/
        }

        [Test]
        public void Products()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //Click login//
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a")).Click();
            driver.FindElement(By.Name("cust_email")).SendKeys("funan@gmail.com");
            driver.FindElement(By.Name("cust_password")).SendKeys("141216");
            driver.FindElement(By.Name("form1")).Click();

            //click men
            driver.FindElement(By.CssSelector("a[href*='product-category.php?id=1&type=top-category']")).Click();
            Thread.Sleep(1000);

            
            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[1]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //quantity
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).Clear();
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).Clear();
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[4]/input")).Click();
            Thread.Sleep(1000);

            //Hiện thông báo//
            try
            {
                IAlert alert = driver.SwitchTo().Alert();

                // In nội dung của alert ra console
                Console.WriteLine("Alert Text: " + alert.Text);

                // Nhấn nút "OK" trên alert
                alert.Accept();

                Console.WriteLine("This product is already added to the shopping cart.");
            }
            catch (NoAlertPresentException)
            {
                Console.WriteLine("No alert found.");
            }
        }
    }
}