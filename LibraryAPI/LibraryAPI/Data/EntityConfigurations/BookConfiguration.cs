using LibraryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Data.EntityConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Author).HasMaxLength(50).IsRequired();
            builder.Property(b => b.Title).HasMaxLength(100).IsRequired();
            builder.Property(b => b.PublicationYear).HasMaxLength(4);
            builder.Property(b => b.IsAvailable).HasDefaultValue(true);

            builder.HasMany(b => b.Rents)
                .WithOne(re => re.Book);

            builder.HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            builder.HasIndex(b => new { b.Title, b.Author });
        }
    }
}
