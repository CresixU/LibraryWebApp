using LibraryAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Infrastructure.EntityConfigurations
{
    public class RentConfiguration : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.HasQueryFilter(r => !r.isDeleted);

            builder.Property(re => re.RentDate).ValueGeneratedOnAdd();
        }
    }
}
