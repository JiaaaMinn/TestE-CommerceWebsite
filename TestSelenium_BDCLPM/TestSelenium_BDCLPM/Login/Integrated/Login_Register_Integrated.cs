using System;
using System.Collections.Generic;
using NUnit.Framework;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TestSelenium_BDCLPM.Login;
using TestSelenium_BDCLPM.Login.Admin;
using TestSelenium_BDCLPM.Login.User;
using TestSelenium_BDCLPM.Register;

namespace TestSelenium_BDCLPM.Login.Integrated
{
    public class Integrated_Test
    {
        private IWebDriver driver;
        private ExcelPackage package;
        private ExcelWorksheet sheet;
        private int rowCount;

        [SetUp]
        public void Setup()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string filePath = "D:\\BDCLPM\\TestData.xlsx";

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File Excel '{filePath}' không tồn tại. Kiểm tra lại đường dẫn.");
            }

            package = new ExcelPackage(new FileInfo(filePath));
            sheet = package.Workbook.Worksheets["IntegratedTest"];

            if (sheet == null)
            {
                throw new Exception("Sheet 'IntegratedTest' không tồn tại trong file Excel.");
            }

            rowCount = sheet.Dimension.Rows;

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }



        [Test]
        public void TestLogin_Register()
        {
            for (int row = 3; row <= rowCount; row++)
            {
                string fullname = sheet.Cells[row, 6].Text;
                string company = sheet.Cells[row, 7].Text;
                string email = sheet.Cells[row, 8].Text;
                string phone = sheet.Cells[row, 9].Text;
                string address = sheet.Cells[row, 10].Text;
                string country = sheet.Cells[row, 11].Text;
                string city = sheet.Cells[row, 12].Text;
                string state = sheet.Cells[row, 13].Text;
                string zip = sheet.Cells[row, 14].Text;
                string password = sheet.Cells[row, 15].Text;
                string repassword = sheet.Cells[row, 16].Text;
                string email_admin = sheet.Cells[row, 17].Text;
                string password_admin = sheet.Cells[row, 18].Text;
                string action = sheet.Cells[row, 19].Text;
                string expectedElement = sheet.Cells[row, 20].Text;

                try
                {
                    RegisterUser(fullname, company, email, phone, address, country, city, state, zip, password, repassword);
                    LoginAdmin(email_admin, password_admin, action);
                    LoginUser(email, password);

                    bool result = VerifyElement(expectedElement);
                    sheet.Cells[row, 23].Value = result ? "Passed" : "Failed";
                }
                catch (Exception ex)
                {
                    sheet.Cells[row, 23].Value = "Failed: " + ex.Message;
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (sheet != null && package.Workbook.Worksheets.Count > 0)
                {
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lưu file Excel: " + ex.Message);
            }
            finally
            {
                package.Dispose();
                driver.Dispose();
            }
        }


        private void RegisterUser(string fullname, string company, string email, string phone, string address, string country, string city, string state, string zip, string password, string repassword)
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //click register
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[2]/a")).Click();

            driver.FindElement(By.Name("cust_name")).SendKeys(fullname);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cust_cname")).SendKeys(company);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cust_email")).SendKeys(email);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cust_phone")).SendKeys(phone);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cust_address")).SendKeys(address);
            Thread.Sleep(1000);


            driver.FindElement(By.XPath("/html/body/div[6]/div/div/div/div/form/div/div[2]/div[6]/span/span[1]/span/span[2]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/span/span/span[1]/input")).SendKeys(country);
            Thread.Sleep(500);
            driver.FindElement(By.XPath("/html/body/span/span/span[1]/input")).SendKeys(Keys.Enter);
            Thread.Sleep(1000);


            driver.FindElement(By.Name("cust_city")).SendKeys(city);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cust_state")).SendKeys(state);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cust_zip")).SendKeys(zip);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cust_password")).SendKeys(password);
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cust_re_password")).SendKeys(repassword);
            Thread.Sleep(1000);


            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(3000);
        }

        private void LoginAdmin(string email, string password, string action)
        {
            // Truy cập vào trang đăng nhập của Admin
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/admin/login.php");
            Thread.Sleep(2000);

            // Nhập email và mật khẩu vào các trường tương ứng
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(3000);

            // Click vào mục "Registered Customers" trong menu
            driver.FindElement(By.XPath("/html/body/div/aside/div/section/ul/li[9]/a/span")).Click();
            Thread.Sleep(2000);

            bool userFound = false;
            do
            {
                var userRows = driver.FindElements(By.XPath("//*[@id='example1']/tbody/tr"));
                foreach (var row in userRows)
                {
                    var emailCell = row.FindElement(By.XPath("./td[3]"));
                    if (emailCell.Text.Trim() == email)
                    {
                        userFound = true;

                        // Kiểm tra nếu action là "Change Status"
                        if (action == "Change Status")
                        {
                            var changeStatusButton = row.FindElement(By.CssSelector(".btn-success"));

                            // Cuộn đến phần tử nếu nó không hiển thị
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", changeStatusButton);
                            Thread.Sleep(1000);

                            // Đợi nút Change Status có thể click được
                            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            wait.Until(driver => changeStatusButton.Displayed && changeStatusButton.Enabled);

                            // Click vào nút Change Status
                            changeStatusButton.Click();
                            Thread.Sleep(2000);
                        }
                        else if (action == "Delete")
                        {
                            // Xác định nút "Delete" dựa trên email của khách hàng
                            var deleteButton = driver.FindElement(By.XPath($"//td[contains(text(), '{email}')]/following-sibling::td//button[contains(text(), 'Delete')]"));

                            // Cuộn đến nút Delete và click
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteButton);
                            Thread.Sleep(1000);

                            // Đợi nút Delete có thể click được
                            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            wait.Until(driver => deleteButton.Displayed && deleteButton.Enabled);

                            // Click vào nút Delete
                            deleteButton.Click();
                            Thread.Sleep(1000);

                            // Xác nhận việc xóa trong modal
                            var confirmDeleteButton = driver.FindElement(By.XPath(".//a[contains(@class, 'btn btn-danger btn-ok')]"));
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", confirmDeleteButton);
                            Thread.Sleep(2000);
                        }

                        break; // Thoát vòng lặp khi đã thực hiện thao tác thành công
                    }
                }

                // Nếu không tìm thấy người dùng trong trang hiện tại, tiếp tục sang trang sau
                if (!userFound)
                {
                    var nextButton = driver.FindElements(By.XPath("//*[@id=\"example1_paginate\"]/ul/li[3]/a"));
                    if (nextButton.Count > 0 && nextButton[0].Enabled)
                    {
                        nextButton[0].Click();
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        break; // Nếu không còn trang tiếp theo, thoát khỏi vòng lặp
                    }
                }
            } while (!userFound);
        }

        private void LoginUser(string email, string password)
        {
            driver.Navigate().GoToUrl("http://localhost/eCommerceSite-PHP/");
            Thread.Sleep(2000);

            //click login
            driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a")).Click();

            driver.FindElement(By.Name("cust_email")).SendKeys(email);
            driver.FindElement(By.Name("cust_password")).SendKeys(password);
            driver.FindElement(By.Name("form1")).Click();
            Thread.Sleep(3000);
        }

        private bool VerifyElement(string expectedElement)
        {
            try
            {
                return driver.FindElement(By.XPath(expectedElement)).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
