using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Books;
using LibraryAPI.Models.Categories;
using LibraryAPI.Models.Roles;
using LibraryAPI.Models.Users;

namespace LibraryAPI
{
    public class LibraryMappingProfile : Profile
    {
        public LibraryMappingProfile()
        {
            CreateMap<User, UsersDTO>()
                .ForMember(a => a.City, b => b.MapFrom(c => c.Address.City))
                .ForMember(a => a.Street, b => b.MapFrom(c => c.Address.Street))
                .ForMember(a => a.Number, b => b.MapFrom(c => c.Address.Number))
                .ForMember(a => a.PostalCode, b => b.MapFrom(c => c.Address.PostalCode));
            
            CreateMap<UserCreateDTO, User>()
                .ForMember(a => a.Address, b => b.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    Number = dto.Number,
                    PostalCode = dto.PostalCode
                }));

            CreateMap<UserUpdateDTO, User>()
                .ForMember(a => a.Address, b => b.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    Number = dto.Number,
                    PostalCode = dto.PostalCode
                }));

            CreateMap<User, UserDTO>()
                .ForMember(a => a.City, b => b.MapFrom(c => c.Address.City))
                .ForMember(a => a.Street, b => b.MapFrom(c => c.Address.Street))
                .ForMember(a => a.Number, b => b.MapFrom(c => c.Address.Number))
                .ForMember(a => a.PostalCode, b => b.MapFrom(c => c.Address.PostalCode))
                .ForMember(a => a.Role, b => b.MapFrom(c => c.Role.Name));

            CreateMap<Book, BookDTO>()
                .ForMember(a => a.Category, b => b.MapFrom(c => c.Category.Name));

            CreateMap<BookCreateDTO, Book>()
                .ForMember(b => b.Category.Id, x => x.MapFrom(dto => dto.CategoryId));

            CreateMap<BookUpdateDTO, Book>()
                .ForMember(b => b.Category.Id, x => x.MapFrom(dto => dto.CategoryId));

            CreateMap<Category, CategoryDTO>();

            CreateMap<Role, RoleDTO>();
        }
    }
}
