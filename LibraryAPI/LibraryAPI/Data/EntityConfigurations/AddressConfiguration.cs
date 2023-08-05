using LibraryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Data.EntityConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.City).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Street).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Number).HasMaxLength(10).IsRequired();
            builder.Property(a => a.PostalCode).HasMaxLength(10).IsRequired();
        }
    }
}
