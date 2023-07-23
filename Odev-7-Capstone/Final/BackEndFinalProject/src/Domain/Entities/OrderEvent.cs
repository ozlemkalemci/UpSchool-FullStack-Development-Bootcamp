using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class OrderEvent : EntityBase<Guid>

    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public OrderStatus Status { get; set; }


    }
}
