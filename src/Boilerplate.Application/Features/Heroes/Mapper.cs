using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Domain.Entities;
using Riok.Mapperly.Abstractions;
using System.Linq;

namespace Boilerplate.Application.Features.Heroes;

[Mapper]
public static partial class Mapper
{
    public static partial Hero ToHeroEntity(CreateHeroRequest dto);
    public static partial GetHeroResponse ToHeroDto(Hero entity);
    
    public static partial IQueryable<GetHeroResponse> ProjectToResponse(this IQueryable<Hero> source);

}