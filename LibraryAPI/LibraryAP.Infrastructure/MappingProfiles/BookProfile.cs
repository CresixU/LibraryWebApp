﻿using AutoMapper;
using LibraryAPI.Application.Models.Books;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Infrastructure.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>()
            .ForMember(a => a.Category, b => b.MapFrom(c => c.Category.Name));

            CreateMap<Book, BookCreateDTO>()
                .ForMember(dto => dto.CategoryId, x => x.MapFrom(b => b.CategoryId))
                .ReverseMap();

            CreateMap<Book, BookUpdateDTO>()
                .ForMember(dto => dto.CategoryId, x => x.MapFrom(b => b.CategoryId))
                .ReverseMap();
        }
    }
}
