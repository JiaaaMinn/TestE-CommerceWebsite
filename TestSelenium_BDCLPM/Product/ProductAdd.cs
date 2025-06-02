using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OfficeOpenXml;

namespace TestSelenium_BDCLPM.Product
{
    public class ProductAdd
    {
        IWebDriver driver;
        private WebDriverWait wait;

        private string categoryTop, categoryMid, categoryEnd, productName, oldPrice, currentPrice, quantity, isActive, photoPath;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/login.php");

            // Login admin
            driver.FindElement(By.Name("email")).SendKeys("admin@mail.com");
            driver.FindElement(By.Name("password")).SendKeys("Password@123");
            driver.FindElement(By.Name("form1")).Click();
            wait.Until(driver => driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")));

            // Click Product Management
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[4]/a/span")).Click();
            wait.Until(driver => driver.FindElement(By.XPath("//a[contains(@href, 'product-add.php')]")));

            // Click Add Product
            driver.FindElement(By.XPath("//a[contains(@href, 'product-add.php')]")).Click();
            wait.Until(driver => driver.FindElement(By.Name("p_name")));
            Thread.Sleep(2000);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        public void AddProduct()
        {
            string filePath = @"D:\\BDCLPM\\TestData.xlsx"; // Đường dẫn tới file Excel của bạn
            var package = new ExcelPackage(new FileInfo(filePath));

            // Lấy worksheet "Add Product"
            var worksheet = package.Workbook.Worksheets["Add_Product"];

            // Duyệt qua tất cả các dòng test
            for (int row = 3; row <= worksheet.Dimension.End.Row; row++)  // Bắt đầu từ dòng 3
            {
                // Đọc dữ liệu từ Excel
                string testCaseId = worksheet.Cells[row, 1].Text;
                categoryTop = worksheet.Cells[row, 6].Text;
                categoryMid = worksheet.Cells[row, 7].Text;
                categoryEnd = worksheet.Cells[row, 8].Text;
                productName = worksheet.Cells[row, 9].Text;
                oldPrice = worksheet.Cells[row, 10].Text;
                currentPrice = worksheet.Cells[row, 11].Text;
                quantity = worksheet.Cells[row, 12].Text;
                isActive = worksheet.Cells[row, 13].Text;
                photoPath = worksheet.Cells[row, 14].Text;
                string element = worksheet.Cells[row, 15].Text; // XPath cần kiểm tra
                string expectedResult = worksheet.Cells[row, 18].Text;

                // Kiểm tra dữ liệu có trống không, nhưng không bỏ qua test, để có thể kiểm tra lỗi
                try
                {
                    // Chọn Top Level Category
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

                    // Điền thông tin vào các trường sản phẩm
                    if (!string.IsNullOrEmpty(productName))
                        driver.FindElement(By.Name("p_name")).SendKeys(productName);
                    driver.FindElement(By.Name("p_old_price")).SendKeys(oldPrice);
                    driver.FindElement(By.Name("p_current_price")).SendKeys(currentPrice);

                    if (!string.IsNullOrEmpty(quantity))
                        driver.FindElement(By.Name("p_qty")).SendKeys(quantity);

                    // Chọn trạng thái "is_active"
                    IWebElement dropdown4 = driver.FindElement(By.Name("p_is_active"));
                    SelectElement select = new SelectElement(dropdown4);
                    select.SelectByValue(isActive);
                    Thread.Sleep(1000);

                    // Tải ảnh sản phẩm
                    if (!string.IsNullOrEmpty(photoPath))
                    {
                        IWebElement uploadElement = driver.FindElement(By.Name("p_featured_photo"));
                        uploadElement.SendKeys(photoPath);
                        Thread.Sleep(1000);
                    }

                    // Bấm nút Add Item
                    IWebElement addButton = driver.FindElement(By.Name("form1"));
                    addButton.Click();
                    Thread.Sleep(3000);  // Đợi một chút để hệ thống xử lý

                    // Kiểm tra phần tử element có xuất hiện không
                    if (!string.IsNullOrEmpty(element))
                    {
                        try
                        {
                            // Kiểm tra sự tồn tại của phần tử dựa trên XPath
                            IWebElement elementAfterAdd = wait.Until(driver => driver.FindElement(By.XPath(element)));

                            // Kiểm tra xem phần tử có hiển thị không
                            if (elementAfterAdd.Displayed)
                            {
                                Console.WriteLine($"Test Case {testCaseId}: Passed");
                                worksheet.Cells[row, 18].Value = "Passed";
                            }
                            else
                            {
                                Console.WriteLine($"Test Case {testCaseId}: Failed. Element is not displayed.");
                                worksheet.Cells[row, 18].Value = "Failed";
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine($"Test Case {testCaseId}: Failed - Element not found.");
                            worksheet.Cells[row, 18].Value = "Failed";
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Test Case {testCaseId}: Failed - Element XPath is empty.");
                        worksheet.Cells[row, 18].Value = "Failed";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Test Case {testCaseId}: Failed - Error: {ex.Message}");
                    worksheet.Cells[row, 18].Value = "Failed";
                }
            }

            // Lưu lại file Excel sau khi đã cập nhật kết quả
            package.Save();
        }

    }
}
