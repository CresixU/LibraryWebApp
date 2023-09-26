using LibraryAPI.Data.Context;
using LibraryAPI.Entities;

namespace LibraryAPI.Data.Seeds
{
    public class CategorySeeder : ISeed
    {
        private readonly LibraryContext _dbContext;
        public CategorySeeder(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SeedData()
        {
            if (_dbContext.Categories.Any())
                return true;

            var categories = GetData();
            _dbContext.Categories.AddRange(categories);
            await _dbContext.SaveChangesAsync();
            
            return false;
        }

        private IEnumerable<Category> GetData()
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
    }
}
