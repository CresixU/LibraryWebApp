﻿using AutoMapper;
using LibraryAPI.Application.Models.Roles;
using LibraryAPI.Infrastructure.Entities;

namespace LibraryAPI.Infrastructure.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDTO>();

            CreateMap<RoleDTO, Role>();
        }
    }
}