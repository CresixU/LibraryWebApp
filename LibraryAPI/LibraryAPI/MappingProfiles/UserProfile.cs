﻿using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Users;

namespace LibraryAPI.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
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
        }    
    }
}
