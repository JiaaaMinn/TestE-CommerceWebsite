using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;

namespace TestSelenium_BDCLPM.Login
{
    public class LoginExcelHelper
    {
        public string excelFile;

        public LoginExcelHelper(string filePath)
        {
            excelFile = filePath;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// Đọc dữ liệu từ Excel theo sheet
        /// </summary>
        public List<(string email, string password, string expectedXPath)> ReadExcel(string sheetName)
        {
            List<(string email, string password, string expectedXPath)> data = new List<(string, string, string)>();
            FileInfo file = new FileInfo(excelFile);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                if (worksheet == null || worksheet.Dimension == null)
                {
                    throw new Exception($"Sheet '{sheetName}' không tồn tại hoặc không có dữ liệu!");
                }

                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 3; row <= rowCount; row++)
                {
                    string email = worksheet.Cells[row, 6].Text.Trim();
                    string password = worksheet.Cells[row, 7].Text.Trim();
                    string expectedXPath = worksheet.Cells[row, 8].Text.Trim();

                    // ✅ Bỏ qua dòng nếu cả email và XPath đều rỗng
                    if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(expectedXPath))
                    {
                        Console.WriteLine($"Dừng đọc tại dòng {row}: Dòng trống không có dữ liệu.");
                        break;
                    }

                    Console.WriteLine($"Đọc dòng {row}: Email='{email}', XPath='{expectedXPath}'");
                    data.Add((email, password, expectedXPath));
                }

            }

            return data;
        }

        /// <summary>
        /// Ghi kết quả kiểm thử vào sheet tương ứng
        /// </summary>
        public void WriteExcelResult(string sheetName, int row, string result)
        {
            FileInfo file = new FileInfo(excelFile);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                if (worksheet == null)
                {
                    Console.WriteLine($"Không tìm thấy sheet '{sheetName}'");
                    return;
                }

                worksheet.Cells[row, 11].Value = result;
                package.Save();

                Console.WriteLine($"Ghi kết quả dòng {row}: {result}");
            }
        }
    }
}
