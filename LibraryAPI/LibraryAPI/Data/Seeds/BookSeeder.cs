using LibraryAPI.Data.Context;
using LibraryAPI.Entities;

namespace LibraryAPI.Data.Seeds
{
    public class BookSeeder : ISeed
    {
        private readonly LibraryContext _dbContext;
        public BookSeeder(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SeedData()
        {
            if (_dbContext.Books.Any()) 
                return true;

            var books = GetData();
            _dbContext.Books.AddRange(books);
            await _dbContext.SaveChangesAsync();

            return false;
        }

        private IEnumerable<Book> GetData()
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
