using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestSelenium_BDCLPM.Login.Admin
{
    public class Login_Admin
    {
        private IWebDriver driver;
        private LoginExcelHelper excelHelper;
        private LoginAdminHelper loginHelper;
        private string sheetName = "Login_Admin";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            excelHelper = new LoginExcelHelper("D:\\BDCLPM\\TestData.xlsx");
            loginHelper = new LoginAdminHelper(driver, "http://localhost/eCommerceSite-PHP/admin/login.php");
        }

        [Test]
        public void LoginAdmin_TestFromExcel()
        {
            List<(string email, string password, string expectedXPath)> testData = excelHelper.ReadExcel(sheetName);

            if (testData.Count == 0)
            {
                Assert.Fail($"Không có dữ liệu để kiểm thử trong '{sheetName}'!");
            }

            int row = 3; // ✅ Bắt đầu từ dòng 3
            foreach (var (email, password, expectedXPath) in testData)
            {
                // ✅ Nếu cả email và XPath rỗng, dừng vòng lặp
                if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(expectedXPath))
                {
                    Console.WriteLine($"⚠ Dừng test tại dòng {row}: Không có dữ liệu.");
                    break; // Thoát khỏi vòng lặp khi hết dữ liệu
                }

                Console.WriteLine($"🔍 Đang kiểm thử dòng {row}: Email = '{email}', XPath = '{expectedXPath}'");

                string result = loginHelper.PerformLogin(email, password, expectedXPath);
                excelHelper.WriteExcelResult(sheetName, row, result);

                Console.WriteLine($"✅ Kết quả dòng {row}: {result}");

                row++;
            }

        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
