using LibraryAPI.Infrastructure.Context;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Infrastructure.Seeds.Seeds
{
    public class RoleSeeder : ISeed
    {
        private readonly LibraryContext _dbContext;
        public RoleSeeder(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SeedData()
        {
            if (_dbContext.Roles.Any()) 
                return true;

            var roles = GetData();
            _dbContext.Roles.AddRange(roles);
            await _dbContext.SaveChangesAsync();

            return false;
        }

        private IEnumerable<Role> GetData()
        {
            var roles = new List<Role>()
            {
                new Role() { Id = 1, Name = "Admin", Power = 255, IsImmutable = true },
                new Role() { Id = 2, Name = "Owner", Power = 200, IsImmutable = true },
                new Role() { Id = 3, Name = "Employee", Power = 5, IsImmutable = true },
                new Role() { Id = 4, Name = "User", Power = 1, IsImmutable = true }
            };

            return roles;
        }
    }
}
