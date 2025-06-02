using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestSelenium_BDCLPM.Login.Integrated
{
    public class Order
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void CheckOrder()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //click login
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a")).Click();

            driver.FindElement(By.Name("cust_email")).SendKeys("funan1@gmail.com");
            driver.FindElement(By.Name("cust_password")).SendKeys("16052003");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(3000);

            //click men
            driver.FindElement(By.CssSelector("a[href*='product-category.php?id=1&type=top-category']")).Click();
            Thread.Sleep(1000);

            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[1]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //quantity
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).Clear();
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).SendKeys("2");
            Thread.Sleep(1000);

            //add to cart
            driver.FindElement(By.Name("form_add_to_cart")).Click();
            Thread.Sleep(1000);

            //cart
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[3]/a")).Click();
            Thread.Sleep(1000);

            //continue shopping
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[2]/ul/li[2]/a")).Click();
            Thread.Sleep(1000);

            //click women
            driver.FindElement(By.XPath("//a[text()='Women']")).Click();
            Thread.Sleep(1000);

            //click add to cart
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div[2]/div/div/div[3]/div/div[2]/p/a")).Click();
            Thread.Sleep(1000);

            //size
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[1]/div/div[1]/span/span[1]/span/span[2]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/span/span/span[1]/input")).SendKeys("M");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/span/span/span[1]/input")).SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            //color
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[1]/div/div[2]/span/span[1]/span/span[2]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/span/span/span[1]/input")).SendKeys("Navy");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/span/span/span[1]/input")).SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            //quantity
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).Clear();
            driver.FindElement(By.XPath("/html/body/div[5]/div/div/div/div[2]/div[1]/div[2]/form/div[3]/input")).SendKeys("3");
            Thread.Sleep(1000);

            //add to cart
            driver.FindElement(By.Name("form_add_to_cart")).Click();
            Thread.Sleep(1000);

            //cart
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[3]/a")).Click();
            Thread.Sleep(1000);

            //checkout
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[2]/ul/li[3]/a")).Click();
            Thread.Sleep(1000);

            

            try
            {
                //this link
                var element_thislink = driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div[5]/div/div/a"));
                element_thislink.Click();
                Thread.Sleep(1000);

                //fill in the form
                // Điền thông tin Billing Address
                driver.FindElement(By.Name("cust_b_name")).SendKeys("JiaaaMinn");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_b_cname")).SendKeys("CompanyName");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_b_phone")).SendKeys("123456789");
                Thread.Sleep(1000);

                // Chọn quốc gia từ dropdown
                var countryDropdown = driver.FindElement(By.Name("cust_b_country"));
                var selectCountry = new SelectElement(countryDropdown);
                selectCountry.SelectByText("Vietnam");
                Thread.Sleep(1000);

                driver.FindElement(By.Name("cust_b_address")).SendKeys("Tân Phú");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_b_city")).SendKeys("HCM");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_b_state")).SendKeys("Tân Phú");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_b_zip")).SendKeys("123");
                Thread.Sleep(1000);

                // Điền thông tin Shipping Address
                driver.FindElement(By.Name("cust_s_name")).SendKeys("JiaaaMinn");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_s_cname")).SendKeys("CompanyName");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_s_phone")).SendKeys("123456789");
                Thread.Sleep(1000);

                // Chọn quốc gia từ dropdown
                var countryDropdownShipping = driver.FindElement(By.Name("cust_s_country"));
                var selectCountryShipping = new SelectElement(countryDropdownShipping);
                selectCountryShipping.SelectByText("Vietnam");
                Thread.Sleep(1000);

                driver.FindElement(By.Name("cust_s_address")).SendKeys("Tân Phú");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_s_city")).SendKeys("HCM");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_s_state")).SendKeys("Tân Phú");
                Thread.Sleep(1000);
                driver.FindElement(By.Name("cust_s_zip")).SendKeys("123");
                Thread.Sleep(1000);

                // Cập nhật Shipping Address
                driver.FindElement(By.Name("form1")).Click();
                Thread.Sleep(1000);

                //cart
                driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[3]/a")).Click();
                Thread.Sleep(1000);

                //checkout
                driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/form/div[2]/ul/li[3]/a")).Click();
                Thread.Sleep(1000);
            }
            catch(NoSuchElementException)
            {
                Console.WriteLine("The element did not appear. Skipping this step.");
            }

            //payment
            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div[5]/div/div/div/span/span[1]/span/span[2]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/span/span/span[1]/input")).SendKeys("Bank Deposit");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/span/span/span[1]/input")).SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            //transaction 
            driver.FindElement(By.Name("transaction_info")).SendKeys("...");

            //pay now
            driver.FindElement(By.Name("form3")).Click();
            Thread.Sleep(1000);

            //admin
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/login.php");

            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(3000);

            //click order management
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[5]/a/span")).Click();
            Thread.Sleep(1000);

            //mark Complete
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/tbody/tr[1]/td[6]/a")).Click();
            Thread.Sleep(1000);

            //user
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //click dashboard
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[2]/a")).Click();
            Thread.Sleep(1000);

            //click order   
            driver.FindElement(By.XPath("/html/body/div[5]/div[1]/div/div[1]/div/ul/a[5]/button")).Click();
            Thread.Sleep(1000);

            //view
            driver.FindElement(By.XPath("/html/body/div[5]/div[1]/div/div[2]/div/div/table/tbody/tr[1]/td[2]"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
