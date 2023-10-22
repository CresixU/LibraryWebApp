﻿using AutoMapper;
using LibraryAPI.Application.Models.Categories;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Infrastructure.MappingProfiles
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
