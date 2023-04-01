using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Category;

namespace LibraryAPI.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        int Create(CategoryDTO dto);
        bool Update(int id, CategoryDTO dto);
        bool Delete(int id);
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

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = _dbContext
                .Categories
                .ToList();

            var dtos = _mapper.Map<List<CategoryDTO>>(categories);

            return dtos;
        }

        public int Create(CategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return category.Id;
        }

        public bool Update(int id, CategoryDTO dto)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null) return false;

            category.Name = dto.Name;
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if(category is null) return false;

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
