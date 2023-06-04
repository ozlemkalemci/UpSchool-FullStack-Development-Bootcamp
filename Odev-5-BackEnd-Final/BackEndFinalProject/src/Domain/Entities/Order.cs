using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        public int? RequestedAmount { get; set; }
        public int TotalFoundAmount { get; set; }
        public CrawlType CrawlType { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<OrderEvent> OrderEvents { get; set; }
    }
}