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

                // Sütun başlıklarını ayarla
                worksheet.Cell(1, 1).Value = "Name";
                worksheet.Cell(1, 2).Value = "Price";
                worksheet.Cell(1, 3).Value = "Sale Price";
                worksheet.Cell(1, 4).Value = "Picture";
                worksheet.Cell(1, 5).Value = "Is On Sale";
                worksheet.Cell(1, 6).Value = "Created On";

                // Verileri doldur
                for (int i = 0; i < products.Count; i++)
                {
                    var product = products[i];
                    worksheet.Cell(i + 2, 1).Value = product.Name;
                    worksheet.Cell(i + 2, 2).Value = product.Price;
                    worksheet.Cell(i + 2, 3).Value = product.SalePrice;
                    worksheet.Cell(i + 2, 4).Value = product.Picture;
                    worksheet.Cell(i + 2, 5).Value = product.IsOnSale;
                    worksheet.Cell(i + 2, 6).Value = product.CreatedOn.DateTime;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}