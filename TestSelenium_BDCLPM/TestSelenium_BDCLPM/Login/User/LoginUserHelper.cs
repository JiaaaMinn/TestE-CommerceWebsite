using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium_BDCLPM.Login.User
{
    public class LoginUserHelper
    {
        private IWebDriver driver;
        private string loginUrl;
        private WebDriverWait wait;

        public LoginUserHelper(IWebDriver webDriver, string url)
        {
            driver = webDriver;
            loginUrl = url;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // ✅ Chờ tối đa 10 giây
        }

        /// <summary>
        /// Thực hiện đăng nhập từ file Excel và kiểm tra `expectedXPath`
        /// </summary>
        public string PerformLogin(string email, string password, string expectedXPath)
        {
            try
            {
                driver.Navigate().GoToUrl(loginUrl);
                Thread.Sleep(2000); // Chờ cho trang load hoàn tất

                // ✅ Click vào nút đăng nhập
                Thread.Sleep(2000);
                IWebElement loginButton = driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[1]/a"));
                loginButton.Click();
                Thread.Sleep(2000);

                // ✅ Điền email
                IWebElement emailInput = driver.FindElement(By.Name("cust_email"));
                emailInput.Clear();
                emailInput.SendKeys(email);
                Thread.Sleep(1000);

                // ✅ Điền password
                IWebElement passwordField = driver.FindElement(By.Name("cust_password"));
                passwordField.Clear();
                passwordField.SendKeys(password);
                Thread.Sleep(1000);

                // ✅ Click nút đăng nhập
                IWebElement btnSubmit = driver.FindElement(By.Name("form1"));
                btnSubmit.Click();
                Thread.Sleep(2000);

                // ✅ Kiểm tra nếu XPath bị trống
                if (string.IsNullOrWhiteSpace(expectedXPath))
                {
                    Console.WriteLine($"⚠ Lỗi: XPath bị trống cho tài khoản {email}");
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
                Thread.Sleep(1000); // Chờ một chút trước khi tìm phần tử
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
