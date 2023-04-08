using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class NoteCategoryConfiguration : IEntityTypeConfiguration<NoteCategory>
    {
        public void Configure(EntityTypeBuilder<NoteCategory> builder)
        {
            // ID
            builder.HasKey(x => new { x.NoteId, x.CategoryId });

            // Relationships
            builder.HasOne<Note>(x => x.Note)
                .WithMany(x => x.NoteCategories)
                .HasForeignKey(x => x.NoteId);

            builder.HasOne<Category>(x => x.Category)
                .WithMany(x => x.NoteCategories)
                .HasForeignKey(x => x.CategoryId);
        }

    }
}
