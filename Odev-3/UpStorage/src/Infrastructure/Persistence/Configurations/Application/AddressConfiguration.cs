using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // AddressType
            builder.Property(x => x.AddressType).IsRequired();
            builder.Property(x => x.AddressType).HasConversion<int>();

            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Name
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(100);

            // District
            builder.Property(c => c.District).IsRequired();
            builder.Property(c => c.District).HasMaxLength(100);

            // PostCode
            builder.Property(c => c.PostCode).IsRequired();
            builder.Property(c => c.PostCode).HasMaxLength(100);

            // AddressLine1
            builder.Property(c => c.AddressLine1).IsRequired();
            builder.Property(c => c.AddressLine1).HasMaxLength(500);

            // AddressLine2
            builder.Property(c => c.AddressLine2).IsRequired();
            builder.Property(c => c.AddressLine2).HasMaxLength(500);


            /* Common Fields */

            // CreatedByUserId
            builder.Property(x => x.CreatedByUserId).IsRequired(false);
            builder.Property(x => x.CreatedByUserId).HasMaxLength(100);

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // ModifiedByUserId
            builder.Property(x => x.ModifiedByUserId).IsRequired(false);
            builder.Property(x => x.ModifiedByUserId).HasMaxLength(100);

            // LastModifiedOn
            builder.Property(x => x.ModifiedOn).IsRequired(false);

            // DeletedByUserId
            builder.Property(x => x.DeletedByUserId).IsRequired(false);
            builder.Property(x => x.DeletedByUserId).HasMaxLength(100);

            // DeletedOn
            builder.Property(x => x.DeletedOn).IsRequired(false);

            // IsDeleted
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");

            //// Relationships
            //builder.HasOne<User>().WithMany()
            //    .HasForeignKey(x => x.UserId);

            //builder.HasOne<User>(x => x.Users)
            //    .WithMany(x => x.Address)
            //    .HasForeignKey(x => x.UserId);

            builder.ToTable("Addresses");







        }
    }
}
