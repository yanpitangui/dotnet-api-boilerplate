using AutoMapper;
using Boilerplate.Application.DTOs.Hero;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /// Hero Map
            CreateMap<Hero, GetHeroDTO>().ReverseMap();
            CreateMap<InsertHeroDTO, Hero>();
            CreateMap<UpdateHeroDTO, Hero>();
            ///
        }
    }
}
