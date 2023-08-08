using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models.Rents;

namespace LibraryAPI.MappingProfiles
{
    public class RentProfile : Profile
    {
        public RentProfile()
        {
            CreateMap<Rent, RentDTO>()
                .ForMember(dto => dto.User, x => x.MapFrom(r => ($"{r.User.Firstname} {r.User.Lastname}")));

            CreateMap<RentCreateDTO, Rent>();
        }
    }
}
