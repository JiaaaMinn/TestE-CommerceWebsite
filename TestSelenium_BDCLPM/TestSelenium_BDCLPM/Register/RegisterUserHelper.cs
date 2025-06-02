using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium_BDCLPM.Register
{
    public class RegisterUserHelper
    {
        private IWebDriver driver;
        private string registerUrl;

        public RegisterUserHelper(IWebDriver webDriver, string url)
        {
            driver = webDriver;
            registerUrl = url;
        }

        /// <summary>
        /// Thực hiện đăng ký tài khoản từ file Excel
        /// </summary>
        public string PerformRegister(string fullName, string companyName, string email, string phone, string address, string country, string city, string state, int zipCode, string password, string confirmPassword, string expectedXPath)
        {
            try
            {
                driver.Navigate().GoToUrl(registerUrl);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                driver.Navigate().GoToUrl(registerUrl);
                Thread.Sleep(2000); // Chờ cho trang load hoàn tất

                // ✅ Click vào nút đăng nhập
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/ul/li[2]/a")).Click();
                Thread.Sleep(2000);

                // ✅ Điền thông tin đăng ký đầy đủ
                FillInputField("cust_name", fullName, wait);
                FillInputField("cust_cname", companyName, wait);
                FillInputField("cust_email", email, wait);
                FillInputField("cust_phone", phone, wait);
                FillInputField("cust_address", address, wait);

                // ✅ Chọn quốc gia từ dropdown (Custom Select2)
                SelectCustomDropdownValue(
                    "//span[@class='select2-selection select2-selection--single']",  // ✅ XPath dropdown
                    "//input[@class='select2-search__field']",  // ✅ XPath ô tìm kiếm
                    country,
                    wait
                );

                FillInputField("cust_city", city, wait);
                FillInputField("cust_state", state, wait);
                FillInputField("cust_zip", zipCode.ToString(), wait);
                FillInputField("cust_password", password, wait);
                FillInputField("cust_re_password", confirmPassword, wait);

                // ✅ Click nút đăng ký
                IWebElement registerButton = wait.Until(d => d.FindElement(By.Name("form1")));
                registerButton.Click();

                // ✅ Chờ phản hồi sau khi đăng ký
                Thread.Sleep(3000);

                // ✅ Kiểm tra nếu XPath bị trống
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

                // ✅ Kiểm tra nếu element xác nhận tồn tại
                return CheckElementExists(By.XPath(expectedXPath), wait) ? "Passed" : "Failed";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Điền dữ liệu vào trường input một cách an toàn
        /// </summary>
        private void FillInputField(string fieldName, string value, WebDriverWait wait)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    IWebElement inputField = wait.Until(d => d.FindElement(By.Name(fieldName)));
                    inputField.Clear();
                    inputField.SendKeys(value);
                    Thread.Sleep(300); // Giảm thời gian chờ giữa các thao tác
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"⚠ Không tìm thấy trường nhập liệu: {fieldName}");
            }
        }

        /// <summary>
        /// Kiểm tra xem element có tồn tại không
        /// </summary>
        private bool CheckElementExists(By by, WebDriverWait wait)
        {
            try
            {
                return wait.Until(d => d.FindElement(by)).Displayed;
            }
            catch (WebDriverTimeoutException)
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

        private void SelectCustomDropdownValue(string dropdownXPath, string searchBoxXPath, string value, WebDriverWait wait)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    // ✅ Click vào dropdown để mở danh sách
                    IWebElement dropdown = wait.Until(d => d.FindElement(By.XPath(dropdownXPath)));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", dropdown); // Cuộn đến dropdown
                    dropdown.Click();
                    Console.WriteLine("✅ Đã click vào dropdown Country");
                    Thread.Sleep(1000); // Chờ dropdown mở hoàn toàn

                    // ✅ Kiểm tra ô tìm kiếm có tồn tại không
                    IWebElement searchBox = wait.Until(d => d.FindElement(By.XPath(searchBoxXPath)));
                    searchBox.Clear();
                    searchBox.SendKeys(value);
                    Console.WriteLine($"🔍 Đang nhập: {value}");
                    Thread.Sleep(2000); // Chờ danh sách cập nhật

                    // ✅ Kiểm tra danh sách có dữ liệu hay không trước khi chọn
                    IList<IWebElement> options = driver.FindElements(By.XPath("//li[contains(@class, 'select2-results__option')]"));
                    if (options.Count > 0)
                    {
                        options[0].Click(); // Chọn kết quả đầu tiên
                        Console.WriteLine("✅ Đã chọn quốc gia");
                    }
                    else
                    {
                        Console.WriteLine("❌ Không tìm thấy quốc gia trong danh sách!");
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Lỗi khi chọn dropdown: {ex.Message}");
            }
        }



    }
}
