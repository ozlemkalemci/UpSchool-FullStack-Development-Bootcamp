using Domain.Entities;

namespace Application.Common.Models.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Picture { get; set; }

        public bool IsOnSale { get; set; }

        public decimal Price { get; set; }

        public string SalePrice { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public static ProductDto MapFromProducts(Domain.Entities.Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Picture = product.Picture,
                Price = product.Price,
                SalePrice = product.SalePrice,
                CreatedOn = product.CreatedOn,
                IsOnSale = product.IsOnSale
            };
        }
    }
}
