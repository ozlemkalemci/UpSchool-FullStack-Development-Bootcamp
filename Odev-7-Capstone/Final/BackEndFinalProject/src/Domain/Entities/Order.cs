using Domain.Common;
using Domain.Enums;
using Domain.Identity;

namespace Domain.Entities
{
    public class Order : EntityBase<Guid>
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int? RequestedAmount { get; set; }
        public int TotalFoundAmount { get; set; }
        public CrawlType CrawlType { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<OrderEvent> OrderEvents { get; set; }
    }
}