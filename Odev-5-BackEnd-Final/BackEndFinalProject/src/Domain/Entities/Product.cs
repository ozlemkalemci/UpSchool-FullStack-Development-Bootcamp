using Domain.Common;

namespace Domain.Entities
{
    public class Product : EntityBase<Guid>
    {

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public bool IsOnSale { get; set; }

        public decimal Price { get; set; }

        public string SalePrice { get; set; }

    }
}
