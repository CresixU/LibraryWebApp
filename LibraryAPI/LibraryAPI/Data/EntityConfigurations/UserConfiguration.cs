﻿using LibraryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LibraryAPI.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> buider)
        {
                buider.Property(u => u.Firstname).HasMaxLength(50).IsRequired();
                buider.Property(u => u.Lastname).HasMaxLength(50).IsRequired();
                buider.Property(u => u.Email).HasMaxLength(50).IsRequired();
                buider.Property(u => u.Password).IsRequired();
                buider.Property(u => u.RoleId).HasDefaultValue(4);

                buider.HasOne(u => u.Address)
                    .WithMany(a => a.Users)
                    .HasForeignKey(u => u.AddressId);
                buider.HasOne(u => u.Role)
                    .WithMany(ro => ro.Users)
                    .HasForeignKey(u => u.RoleId);
                buider.HasMany(u => u.Rents)
                    .WithOne(re => re.User)
                    .HasForeignKey(re => re.UserId);

                buider.HasIndex(u => new { u.Firstname, u.Lastname, u.Email });

        }
    }
}