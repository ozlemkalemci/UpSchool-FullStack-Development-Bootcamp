using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            

            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserId).IsRequired();

            builder.Property(o => o.RequestedAmount)
                .IsRequired(false);

            builder.Property(o => o.TotalFoundAmount)
                .IsRequired();

            builder.Property(o => o.CrawlType)
                .IsRequired();

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // CreatedByUserId
            builder.Property(x => x.CreatedByUserId).IsRequired(false);
            builder.Property(x => x.CreatedByUserId).HasMaxLength(100);


            builder.ToTable("Orders");
        }
    }
}
