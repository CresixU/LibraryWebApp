using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Roles;

namespace LibraryAPI.MappingProfiles
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
