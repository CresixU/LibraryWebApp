using LibraryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Data.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasQueryFilter(r => !r.isDeleted);

            builder.Property(ro => ro.Name).HasMaxLength(50).IsRequired();
            builder.Property(ro => ro.Power).HasDefaultValue(1);
            builder.Property(ro => ro.IsImmutable).HasDefaultValue(false);
        }
    }
}
