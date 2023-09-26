using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Categories;

namespace LibraryAPI.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();

            CreateMap<CategoryDTO, Category>();
        }
    }
}
