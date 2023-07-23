using Application.Common.Interfaces;
using Application.Common.Models.Product;
using ClosedXML.Excel;

namespace Infrastructure.Services
{
    public class ExcelManager : IExcelService
    {
        public async Task<byte[]> GenerateExcelFileAsync(List<ProductDto> products)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                var headers = new[] { "Name", "Price", "Sale Price", "Picture", "Is On Sale", "Created On" };
                var headerRow = worksheet.Row(1);
                for (int i = 0; i < headers.Length; i++)
                {
                    headerRow.Cell(i + 1).Value = headers[i];

                    worksheet.Column(i + 1).Width = 15;
                }

                for (int i = 0; i < products.Count; i++)
                {
                    var product = products[i];
                    var dataRow = worksheet.Row(i + 2);
                    dataRow.Cell(1).Value = product.Name;
                    dataRow.Cell(2).Value = product.Price;
                    dataRow.Cell(3).Value = product.SalePrice;
                    dataRow.Cell(4).Value = product.Picture;
                    dataRow.Cell(5).Value = product.IsOnSale;
                    dataRow.Cell(6).Value = product.CreatedOn.DateTime;
                }

                var tableRange = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(products.Count + 1, headers.Length));
                var excelTable = tableRange.CreateTable();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
