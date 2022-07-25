using StronglyTypedIds;
using System;

[assembly: StronglyTypedIdDefaults(
    backingType: StronglyTypedIdBackingType.Guid,
    converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter |
                StronglyTypedIdConverter.Default | StronglyTypedIdConverter.TypeConverter,
    implementations: StronglyTypedIdImplementations.IEquatable | StronglyTypedIdImplementations.Default)]

namespace Boilerplate.Domain.Entities.Common;


public interface IGuid {}

[StronglyTypedId]
public partial struct HeroId : IGuid
{
    public static implicit operator HeroId(Guid guid)
    {
        return new HeroId(guid);
    }
}

[StronglyTypedId]
public partial struct UserId : IGuid
{
    public static implicit operator UserId(Guid guid)
    {
        return new UserId(guid);
    }
}