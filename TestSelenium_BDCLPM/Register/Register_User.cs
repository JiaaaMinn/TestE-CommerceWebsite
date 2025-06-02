using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestSelenium_BDCLPM.Login;

namespace TestSelenium_BDCLPM.Register
{
    public class Register_User
    {
        private IWebDriver driver;
        private RegisterExcelHelper excelHelper;
        private RegisterUserHelper registerHelper;
        private string sheetName = "Register_User";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            excelHelper = new RegisterExcelHelper("D:\\BDCLPM\\TestData.xlsx");
            registerHelper = new RegisterUserHelper(driver, "http://localhost/eCommerceSite-PHP/index.php");
        }

        [Test]
        public void RegisterUser_TestFromExcel()
        {
            List<(string fullName, string companyName, string email, string phone, string address, string country, string city, string state, int zipCode, string password, string confirmPassword, string expectedXPath)> testData = excelHelper.ReadRegisterData(sheetName);

            if (testData.Count == 0)
            {
                Assert.Fail($"❌ Không có dữ liệu kiểm thử trong '{sheetName}'!");
            }

            int row = 3; // ✅ Bắt đầu từ dòng 3
            foreach (var (fullName, companyName, email, phone, address, country, city, state, zipCode, password, confirmPassword, expectedXPath) in testData)
            {
                Console.WriteLine($"🔍 Đang kiểm thử dòng {row}: Email = '{email}', XPath = '{expectedXPath}'");

                if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(expectedXPath))
                {
                    Console.WriteLine($"⚠ Dừng test tại dòng {row}: Không có dữ liệu.");
                    break;
                }

                // ✅ Thực hiện đăng ký
                string result = registerHelper.PerformRegister(fullName, companyName, email, phone, address, country, city, state, zipCode, password, confirmPassword, expectedXPath);

                // ✅ Ghi kết quả kiểm thử vào file Excel
                excelHelper.WriteRegisterResult(sheetName, row, result);

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
