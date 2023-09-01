using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Data.Context;
using LibraryAPI.Entities;
using LibraryAPI.Models.Categories;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<int> Create(CategoryDTO dto);
        Task<bool> Update(int id, CategoryDTO dto);
        Task<bool> Delete(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly LibraryContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(LibraryContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = await _dbContext
                .Categories
                .ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return categories;
        }

        public async Task<int> Create(CategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> Update(int id, CategoryDTO dto)
        {
            var category = await _dbContext
                .Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null) return false;

            category.Name = dto.Name;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _dbContext
                .Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(category is null) return false;
            if(category.Books.Any()) return false;

            category.isDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
