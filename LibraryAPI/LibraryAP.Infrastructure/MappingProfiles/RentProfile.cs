using AutoMapper;
using LibraryAPI.Application.Models.Rents;
using LibraryAPI.Domain.Entities;

namespace LibraryAPI.Infrastructure.MappingProfiles
{
    public class RentProfile : Profile
    {
        public RentProfile()
        {
            CreateMap<Rent, RentDTO>()
                .ForMember(dto => dto.User, x => x.MapFrom(r => $"{r.User.Firstname} {r.User.Lastname}"));

            CreateMap<RentCreateDTO, Rent>();
        }
    }
}
