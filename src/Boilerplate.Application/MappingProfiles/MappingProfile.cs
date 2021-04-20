using AutoMapper;
using Boilerplate.Application.DTOs.Hero;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Hero Map
            CreateMap<Hero, GetHeroDto>().ReverseMap();
            CreateMap<InsertHeroDto, Hero>();
            CreateMap<UpdateHeroDto, Hero>();
        }
    }
}
