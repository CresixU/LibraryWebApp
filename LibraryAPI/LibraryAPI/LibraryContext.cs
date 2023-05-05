using LibraryAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(eb =>
            {
                eb.Property(a => a.City).HasMaxLength(50).IsRequired();
                eb.Property(a => a.Street).HasMaxLength(50).IsRequired();
                eb.Property(a => a.Number).HasMaxLength(10).IsRequired();
                eb.Property(a => a.PostalCode).HasMaxLength(10).IsRequired();
            });


            modelBuilder.Entity<Book>(eb =>
            {
                eb.Property(b => b.Author).HasMaxLength(50).IsRequired();
                eb.Property(b => b.Title).HasMaxLength(100).IsRequired();
                eb.Property(b => b.PublicationYear).HasMaxLength(4);
                eb.Property(b => b.IsAvailable).HasDefaultValue(true);

                eb.HasMany(b => b.Rents)
                    .WithMany(re => re.Books);

                eb.HasOne(b => b.Category)
                    .WithMany(c => c.Books)
                    .HasForeignKey(b => b.CategoryId);
            });

            modelBuilder.Entity<Category>(eb =>
            {
                eb.Property(c => c.Name).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Rent>(eb =>
            {
                eb.Property(re => re.RentDate).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Role>(eb =>
            {
                eb.Property(ro => ro.Name).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<User>(eb =>
            {
                eb.Property(u => u.Firstname).HasMaxLength(50).IsRequired();
                eb.Property(u => u.Lastname).HasMaxLength(50).IsRequired();
                eb.Property(u => u.Email).HasMaxLength(50).IsRequired();
                eb.Property(u => u.Password).IsRequired();
                eb.Property(u => u.RoleId).HasDefaultValue(4);

                eb.HasOne(u => u.Address)
                    .WithMany(a => a.Users)
                    .HasForeignKey(u => u.AddressId);
                eb.HasOne(u => u.Role)
                    .WithMany(ro => ro.Users)
                    .HasForeignKey(u => u.RoleId);
                eb.HasMany(u => u.Rents)
                    .WithOne(re => re.User)
                    .HasForeignKey(re => re.UserId);
            });

            //Seeding data
            modelBuilder.Entity<Role>()
                .HasData(
                    new Role() { Id = 1, Name = "Admin" },
                    new Role() { Id = 2, Name = "Owner" },
                    new Role() { Id = 3, Name = "Employee" },
                    new Role() { Id = 4, Name = "User" }
                );
        }



    }

}
