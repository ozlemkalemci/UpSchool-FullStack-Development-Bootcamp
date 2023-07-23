using Domain.Enums;

namespace Application.Common.Models.OrderEvent
{
    public class OrderEventDto
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public static OrderEventDto MapFromOrderEvents(Domain.Entities.OrderEvent orderEvent)
        {
            return new OrderEventDto()
            {
                OrderId = orderEvent.OrderId,
                Status = orderEvent.Status,
                CreatedOn = orderEvent.CreatedOn,
            };
        }
    }
}
