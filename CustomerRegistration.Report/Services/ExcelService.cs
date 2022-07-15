using CustomerRegistration.Report.Dtos;
using CustomerRegistration.Report.Services.Abstractions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Report.Services
{
    public class ExcelService<TDto> : IExcelService<TDto> where TDto : class
    {
        public ExcelService()
        {

        }
        public async Task ProcessExcel(List<TDto> data, string header)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var file = new FileInfo(@"D:\Desktop\Patika\LINK-BOOTCAMP\BitirmeProjesi\ExcelFile\Report.xlsx");
            var people = data;
            await SaveExcelFile(people, file, header);
        }


        private async Task SaveExcelFile(List<TDto> customerCountByCity, FileInfo file, string header)
        {
            DeleteIfExists(file);

            using var package = new ExcelPackage(file);

            var ws = package.Workbook.Worksheets.Add("MainReport");

            var range = ws.Cells["A2"].LoadFromCollection(customerCountByCity, true);
            range.AutoFitColumns();

            // Formats the header
            ws.Cells["A1"].Value = header;
            ws.Cells["A1:C1"].Merge = true;
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Row(1).Style.Font.Size = 24;
            ws.Row(1).Style.Font.Color.SetColor(Color.Blue);

            ws.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Row(2).Style.Font.Bold = true;
            ws.Column(3).Width = 20;

            await package.SaveAsync();
        }

        private void DeleteIfExists(FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }

    }
}
