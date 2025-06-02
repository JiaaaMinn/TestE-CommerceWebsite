using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSelenium_BDCLPM.Category
{
    public class AddMid
    {
        IWebDriver driver;
        private WebDriverWait wait;

        private string categoryTop, categoryMid;

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

            // Click Shop Settings
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[3]/a")).Click();
            Thread.Sleep(1000);

            // Click Mid Cate
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[3]/ul/li[6]/a")).Click();
            Thread.Sleep(2000);

            //Click Add
            driver.FindElement(By.XPath("/html/body/div/div/section[1]/div[2]/a")).Click();
            Thread.Sleep(2000);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        public void AddCate()
        {
            string filePath = @"D:\\BDCLPM\\TestData.xlsx"; // Đường dẫn tới file Excel của bạn
            var package = new ExcelPackage(new FileInfo(filePath));

            // Lấy worksheet "Add Product"
            var worksheet = package.Workbook.Worksheets["Add_MidCate"];

            // Duyệt qua tất cả các dòng test
            for (int row = 3; row <= worksheet.Dimension.End.Row; row++)  // Bắt đầu từ dòng 3
            {
                // Đọc dữ liệu từ Excel
                string testCaseId = worksheet.Cells[row, 1].Text;
                categoryTop = worksheet.Cells[row, 6].Text;
                categoryMid = worksheet.Cells[row, 7].Text;
                string element = worksheet.Cells[row, 8].Text; // XPath cần kiểm tra
                string expectedResult = worksheet.Cells[row, 11].Text;

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

                    if (!string.IsNullOrEmpty(categoryMid))
                        driver.FindElement(By.Name("mcat_name")).SendKeys(categoryMid);
                    Thread.Sleep(2000);

                    // Bấm nút Add Item
                    IWebElement addButton = driver.FindElement(By.Name("form1"));
                    addButton.Click();
                    Thread.Sleep(3000);

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
                                worksheet.Cells[row, 11].Value = "Passed";
                            }
                            else
                            {
                                Console.WriteLine($"Test Case {testCaseId}: Failed. Element is not displayed.");
                                worksheet.Cells[row, 11].Value = "Failed";
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine($"Test Case {testCaseId}: Failed - Element not found.");
                            worksheet.Cells[row, 11].Value = "Failed";
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Test Case {testCaseId}: Failed - Element XPath is empty.");
                        worksheet.Cells[row, 11].Value = "Failed";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Test Case {testCaseId}: Failed - Error: {ex.Message}");
                    worksheet.Cells[row, 11].Value = "Failed";
                }
            }

            // Lưu lại file Excel sau khi đã cập nhật kết quả
            package.Save();
        }
    }
}
