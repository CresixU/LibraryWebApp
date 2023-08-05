using LibraryAPI.Entities;
using System.Reflection.Metadata.Ecma335;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryAPI
{
    public class LibrarySeeder
    {
        private readonly LibraryContext _dbContext;
        private bool _hasUpdated = false;

        public LibrarySeeder(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
                    _hasUpdated = true;
                }
                if(!_dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    _dbContext.Categories.AddRange(categories);
                    _hasUpdated = true;
                }
                if(!_dbContext.Books.Any())
                {
                    var books = GetBooks();
                    _dbContext.Books.AddRange(books);
                    _hasUpdated = true;
                }

                if( _hasUpdated) _dbContext.SaveChanges();
            }
        }

        private IEnumerable<User> GetUsers()
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

        private IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Test"
                }
            };
            return categories;
        }

        private IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>()
            {
                new Book()
                {
                    Title = "Testowy tytuł",
                    Author = "Adam Tester",
                    IsAvailable = true,
                    PublicationYear = 2023,
                    CategoryId = 1
                }
            };
            return books;
        }
        
    }
}
