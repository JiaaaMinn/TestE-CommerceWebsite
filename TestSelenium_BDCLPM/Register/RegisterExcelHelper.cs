using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;

namespace TestSelenium_BDCLPM
{
    public class RegisterExcelHelper
    {
        public string excelFile;

        public RegisterExcelHelper(string filePath)
        {
            excelFile = filePath;
            OfficeOpenXml.ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// Đọc dữ liệu đăng ký từ Excel (Chỉ dành cho Register_User)
        /// </summary>
        public List<(string name, string company, string email, string phone, string address, string country, string city, string state, int zipcode, string password, string confirmPassword, string expectedXPath)> ReadRegisterData(string sheetName, int startRow = 3)
        {
            List<(string, string, string, string, string, string, string, string, int, string, string, string)> data = new List<(string, string, string, string, string, string, string, string, int, string, string, string)>();
            FileInfo file = new FileInfo(excelFile);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                if (worksheet == null || worksheet.Dimension == null)
                {
                    throw new Exception($"Sheet '{sheetName}' không tồn tại hoặc không có dữ liệu!");
                }

                int rowCount = worksheet.Dimension.End.Row;

                for (int row = startRow; row <= rowCount; row++)
                {
                    string name = worksheet.Cells[row, 6].Text.Trim();
                    string company = worksheet.Cells[row, 7].Text.Trim();
                    string email = worksheet.Cells[row, 8].Text.Trim();
                    string phone = worksheet.Cells[row, 9].Text.Trim();
                    string address = worksheet.Cells[row, 10].Text.Trim();
                    string country = worksheet.Cells[row, 11].Text.Trim();
                    string city = worksheet.Cells[row, 12].Text.Trim();
                    string state = worksheet.Cells[row, 13].Text.Trim();
                    int zipcode = int.TryParse(worksheet.Cells[row, 14].Text.Trim(), out int zip) ? zip : 0;
                    string password = worksheet.Cells[row, 15].Text.Trim();
                    string confirmPassword = worksheet.Cells[row, 16].Text.Trim();
                    string expectedXPath = worksheet.Cells[row, 17].Text.Trim();  // ✅ Đọc đúng cột expectedXPath

                    // Nếu email & expectedXPath rỗng thì dừng
                    if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(expectedXPath))
                    {
                        Console.WriteLine($"⚠ Dừng đọc tại dòng {row}: Không có dữ liệu.");
                        break;
                    }

                    Console.WriteLine($"📖 Đọc dòng {row}: Email='{email}', XPath='{expectedXPath}'");
                    data.Add((name, company, email, phone, address, country, city, state, zipcode, password, confirmPassword, expectedXPath));
                }
            }

            return data;
        }


        /// <summary>
        /// Ghi kết quả kiểm thử vào cột 'Actual Result' cho `Register_User`
        /// </summary>
        public void WriteRegisterResult(string sheetName, int row, string result)
        {
            FileInfo file = new FileInfo(excelFile);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                if (worksheet == null)
                {
                    Console.WriteLine($"⚠ Không tìm thấy sheet '{sheetName}'");
                    return;
                }

                worksheet.Cells[row, 20].Value = result;
                package.Save();

                Console.WriteLine($"✅ Ghi kết quả dòng {row}: {result}");
            }
        }
    }
}
