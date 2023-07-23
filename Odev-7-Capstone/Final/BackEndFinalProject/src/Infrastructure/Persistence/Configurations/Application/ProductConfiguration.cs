using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Name
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(150);

            builder.Property(x => x.Picture).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(200);

            builder.Property(x => x.IsOnSale).IsRequired();
            builder.Property(x => x.IsOnSale).HasDefaultValueSql("0");

            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(15, 4)");

            builder.Property(x => x.SalePrice).IsRequired();
            builder.Property(x => x.Picture).HasMaxLength(200);

            // CreatedByUserId
            builder.Property(x => x.CreatedByUserId).IsRequired(false);
            builder.Property(x => x.CreatedByUserId).HasMaxLength(100);

            builder.HasOne(p => p.Order)
            .WithMany(o => o.Products)
            .HasForeignKey(p => p.OrderId);

            builder.ToTable("Products");
        }
    }
}
