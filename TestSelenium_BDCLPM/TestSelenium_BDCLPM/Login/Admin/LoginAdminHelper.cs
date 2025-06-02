using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace TestSelenium_BDCLPM.Login.Admin
{
    public class LoginAdminHelper
    {
        private IWebDriver driver;
        private string loginUrl;

        public LoginAdminHelper(IWebDriver webDriver, string url)
        {
            driver = webDriver;
            loginUrl = url;
        }

        /// <summary>
        /// Thực hiện đăng nhập và kiểm tra nhiều `expectedXPath`
        /// </summary>
        public string PerformLogin(string email, string password, string expectedXPath)
        {
            try
            {
                driver.Navigate().GoToUrl(loginUrl);
                Thread.Sleep(1000);

                // Điền email nếu có
                IWebElement emailInput = driver.FindElement(By.Name("email"));
                emailInput.Clear();
                emailInput.SendKeys(email);
                Thread.Sleep(500);

                // Điền password nếu có
                IWebElement passwordField = driver.FindElement(By.Name("password"));
                passwordField.Clear();
                passwordField.SendKeys(password);
                Thread.Sleep(500);

                // Click nút đăng nhập
                IWebElement loginButton = driver.FindElement(By.Name("form1"));
                loginButton.Click();
                Thread.Sleep(2000);

                // ✅ Nếu XPath rỗng hoặc không hợp lệ → Ghi "Failed"
                if (string.IsNullOrWhiteSpace(expectedXPath))
                {
                    Console.WriteLine($"⚠ Lỗi: XPath bị trống cho tài khoản {email}");
                    return "Failed";
                }

                if (!IsValidXPath(expectedXPath))
                {
                    Console.WriteLine($"❌ Lỗi: XPath không hợp lệ [{expectedXPath}]");
                    return "Failed";
                }

                // ✅ Kiểm tra nếu element tồn tại
                return CheckElementExists(By.XPath(expectedXPath)) ? "Passed" : "Failed";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }



        /// <summary>
        /// Kiểm tra xem element có tồn tại không
        /// </summary>
        private bool CheckElementExists(By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra nếu XPath hợp lệ trước khi dùng với Selenium
        /// </summary>
        private bool IsValidXPath(string xpath)
        {
            try
            {
                var _ = By.XPath(xpath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
