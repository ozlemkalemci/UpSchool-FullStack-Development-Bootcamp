using Application.Common.Models.Product;

namespace Application.Common.Interfaces
{
    public interface IExcelService
	{
		Task<byte[]> GenerateExcelFileAsync(List<ProductDto> products);

	}
}
