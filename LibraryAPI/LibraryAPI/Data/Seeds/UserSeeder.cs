using LibraryAPI.Data.Context;
using LibraryAPI.Entities;

namespace LibraryAPI.Data.Seeds
{
    public class UserSeeder : ISeed
    {
        private readonly LibraryContext _dbContext;
        public UserSeeder(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SeedData()
        {
            if (_dbContext.Users.Any()) 
                return true;

            var users = GetData();
            _dbContext.Users.AddRange(users);
            await _dbContext.SaveChangesAsync();

            return false;
        }

        private IEnumerable<User> GetData()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Firstname = "Admin",
                    Lastname = "Library",
                    Email = "admin@mail.com",
                    Password = "AQAAAAEAACcQAAAAEH4IFyCqZ7OAcnidPUEqi4tGdr0l1Y685DHfAbxV6oHEQPFS0n+Xk3t1Uzpi+IrPKw==", //password
                    Address = new Address()
                    {
                        City = "Example", Street = "Example", Number = "Example", PostalCode = "Example"
                    },
                    RoleId = 1
                },
                new User()
                {
                    Firstname = "Owner",
                    Lastname = "Library",
                    Email = "owner@mail.com",
                    Password = "AQAAAAEAACcQAAAAEH4IFyCqZ7OAcnidPUEqi4tGdr0l1Y685DHfAbxV6oHEQPFS0n+Xk3t1Uzpi+IrPKw==",
                    Address = new Address()
                    {
                        City = "Example", Street = "Example", Number = "Example", PostalCode = "Example"
                    },
                    RoleId = 2
                },
                new User()
                {
                    Firstname = "Employee",
                    Lastname = "Library",
                    Email = "employee@mail.com",
                    Password = "AQAAAAEAACcQAAAAEH4IFyCqZ7OAcnidPUEqi4tGdr0l1Y685DHfAbxV6oHEQPFS0n+Xk3t1Uzpi+IrPKw==",
                    Address = new Address()
                    {
                        City = "Example", Street = "Example", Number = "Example", PostalCode = "Example"
                    },
                    RoleId = 3
                },
                new User()
                {
                    Firstname = "User",
                    Lastname = "Library",
                    Email = "user@mail.com",
                    Password = "AQAAAAEAACcQAAAAEH4IFyCqZ7OAcnidPUEqi4tGdr0l1Y685DHfAbxV6oHEQPFS0n+Xk3t1Uzpi+IrPKw==",
                    Address = new Address()
                    {
                        City = "Example", Street = "Example", Number = "Example", PostalCode = "Example"
                    },
                    RoleId = 4
                },
            };
            return users;
        }
    }
}
