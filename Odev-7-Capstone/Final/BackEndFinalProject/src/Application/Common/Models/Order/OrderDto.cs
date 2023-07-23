using Application.Common.Models.OrderEvent;
using Application.Common.Models.Product;
using Domain.Enums;
using Domain.Identity;

namespace Application.Common.Models.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int? RequestedAmount { get; set; }
        public int TotalFoundAmount { get; set; }
        public CrawlType CrawlType { get; set; }
        public List<ProductDto> Products { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public List<OrderEventDto> OrderEvents { get; set; }

        public static OrderDto MapFromOrder(Domain.Entities.Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                UserId = order.UserId,
                RequestedAmount = order.RequestedAmount,
                TotalFoundAmount = order.TotalFoundAmount,
                CrawlType = order.CrawlType,
                CreatedOn = order.CreatedOn,
                OrderEvents = order.OrderEvents.Select(OrderEventDto.MapFromOrderEvents).ToList(),
                Products = order.Products.Select(ProductDto.MapFromProducts).ToList()

            };
        }
    }
}
