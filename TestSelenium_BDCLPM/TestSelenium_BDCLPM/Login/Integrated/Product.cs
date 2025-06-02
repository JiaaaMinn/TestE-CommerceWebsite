using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium_BDCLPM.Login.Integrated
{
    public class Product
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void CURD_Product()
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/login.php");

            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(3000);

            //click product management
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //click add product
            driver.FindElement(By.XPath("//a[contains(@href, 'product-add.php')]")).Click();
            Thread.Sleep(1000);

            IWebElement dropdown = driver.FindElement(By.XPath("//span[contains(text(), 'Select Top Level Category')]"));
            dropdown.Click();


            // Chọn option "Electronic"
            IWebElement option = driver.FindElement(By.XPath("//li[contains(text(), 'Electronics')]"));
            option.Click();
            Thread.Sleep(1000);

            IWebElement dropdown2 = driver.FindElement(By.XPath("//span[contains(text(), 'Select Mid Level Category')]"));
            dropdown2.Click();
            Thread.Sleep(1000);

            // Chọn option "Electronic Items"
            IWebElement option2 = driver.FindElement(By.XPath("//li[contains(text(), 'Electronic Items')]"));
            option2.Click();
            Thread.Sleep(1000);

            IWebElement dropdown3 = driver.FindElement(By.XPath("//span[contains(text(), 'Select End Level Category')]"));
            dropdown3.Click();
            Thread.Sleep(1000);

            IWebElement option3 = driver.FindElement(By.XPath("//li[contains(text(), 'Cell Phone and Accessories')]"));
            option3.Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Name("p_name")).SendKeys("phone");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("p_old_price")).SendKeys("200");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("p_current_price")).SendKeys("150");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("p_qty")).SendKeys("50");
            Thread.Sleep(1000);

            IWebElement dropdown4 = driver.FindElement(By.Name("p_is_active"));
            Thread.Sleep(1000);

            SelectElement select = new SelectElement(dropdown4);
            select.SelectByValue("1"); // Chọn Yes (value="1")
            Thread.Sleep(1000);

            //photo
            IWebElement uploadElement = driver.FindElement(By.Name("p_featured_photo"));
            string filePath = @"D:/shopping.jpg";
            Thread.Sleep(1000);

            uploadElement.SendKeys(filePath);
            Thread.Sleep(1000);

            IWebElement addButton = driver.FindElement(By.Name("form1"));
            addButton.Click();
            Thread.Sleep(3000);

            //user
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/index.php");
            driver.FindElement(By.XPath("/html/body/div[4]/div/div/div/div/div/ul/li[5]/a")).Click();
            Thread.Sleep(1000);

            IWebElement searchBox = driver.FindElement(By.Name("search_text"));
            searchBox.Clear(); // Xóa nội dung cũ (nếu có)
            searchBox.SendKeys("phone"); // Nhập từ khóa
            searchBox.SendKeys(Keys.Enter);
            Thread.Sleep(1000);


            //edit
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/login.php");
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(3000);

            //click product management
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            Thread.Sleep(1000);

            //click edit product
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/tbody/tr[1]/td[10]/a[1]")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Name("p_qty")).Clear();
            driver.FindElement(By.Name("p_qty")).SendKeys("30");
            Thread.Sleep(1000);

            IWebElement edit = driver.FindElement(By.Name("form1"));
            edit.Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("/html/body/div[1]/div/section[1]/div[2]/a")).Click();
            Thread.Sleep(1000);

            IWebElement quantityElement = driver.FindElement(By.XPath("//*[@id=\"example1\"]/tbody/tr[1]/td[6]"));

            // Lấy giá trị của Quantity
            int quantity = int.Parse(quantityElement.Text.Trim());

            // Kiểm tra nếu quantity bằng 30
            if (quantity == 30)
            {
                Console.WriteLine("Quantity is 30");
            }
            else
            {
                Console.WriteLine("Quantity is not 30");
            }

            //delete
            driver.FindElement(By.XPath("//*[@id=\"example1\"]/tbody/tr[1]/td[10]/a[2]")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.XPath("//*[@id=\"confirm-delete\"]/div/div/div[3]/a")).Click();
            Thread.Sleep(1000);

            //user
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/index.php");
            driver.FindElement(By.XPath("/html/body/div[4]/div/div/div/div/div/ul/li[5]/a")).Click();
            Thread.Sleep(1000);

            IWebElement searchBox1 = driver.FindElement(By.Name("search_text"));
            searchBox1.Clear(); // Xóa nội dung cũ (nếu có)
            searchBox1.SendKeys("phone"); // Nhập từ khóa
            searchBox1.SendKeys(Keys.Enter);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
