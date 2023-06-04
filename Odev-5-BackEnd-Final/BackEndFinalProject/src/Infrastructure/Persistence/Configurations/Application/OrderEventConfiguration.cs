using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class OrderEventConfiguration : IEntityTypeConfiguration<OrderEvent>
    {
        public void Configure(EntityTypeBuilder<OrderEvent> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Status)
                .IsRequired();


            /* Common Fields */


            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            builder.HasOne(p => p.Order)
            .WithMany(o => o.OrderEvents)
            .HasForeignKey(p => p.OrderId);

            builder.ToTable("OrderEvents");
        }
    }
}
