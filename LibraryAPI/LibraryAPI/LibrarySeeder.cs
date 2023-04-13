using LibraryAPI.Entities;
using System.Reflection.Metadata.Ecma335;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryAPI
{
    public class LibrarySeeder
    {
        private readonly LibraryContext _dbContext;

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
                    _dbContext.SaveChanges();
                }
                if(!_dbContext.Categories.Any())
                {
                    var categories = GetCategories();
                    _dbContext.Categories.AddRange(categories);
                    _dbContext.SaveChanges();
                }
                if(!_dbContext.Books.Any())
                {
                    var books = GetBooks();
                    _dbContext.Books.AddRange(books);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Firstname = "Admin",
                    Lastname = "Admin",
                    Email = "cresixu@gmail.com",
                    Password = "password",
                    Address = new Address()
                    {
                        City = "Example", Street = "Example", Number = "Example", PostalCode = "Example"
                    },
                    RoleId = 1
                }
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
