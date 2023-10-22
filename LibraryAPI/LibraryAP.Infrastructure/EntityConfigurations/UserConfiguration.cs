using LibraryAPI.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(u => !u.isDeleted);

            builder.Property(u => u.Firstname).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Lastname).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.RoleId).HasDefaultValue(4);

            builder.OwnsOne(u => u.Address, onb =>
            {
                onb.Property(a => a.City).HasMaxLength(50).IsRequired();
                onb.Property(a => a.Street).HasMaxLength(50).IsRequired();
                onb.Property(a => a.Number).HasMaxLength(10).IsRequired();
                onb.Property(a => a.PostalCode).HasMaxLength(10).IsRequired();
            });

            builder.HasOne(u => u.Role)
                .WithMany(ro => ro.Users)
                .HasForeignKey(u => u.RoleId);
            builder.HasMany(u => u.Rents)
                .WithOne(re => re.User)
                .HasForeignKey(re => re.UserId);

            builder.HasIndex(u => new { u.Firstname, u.Lastname, u.Email });

        }
    }
}
