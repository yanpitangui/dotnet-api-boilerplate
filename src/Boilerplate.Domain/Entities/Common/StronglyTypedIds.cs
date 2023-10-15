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
public partial struct HeroId : IGuid, IParsable<HeroId>
{
    public static implicit operator HeroId(Guid guid)
    {
        return new HeroId(guid);
    }

    public static HeroId Parse(string s, IFormatProvider? provider)
    {
        return Guid.Parse(s, provider);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out HeroId result)
    {
        var parsed = Guid.TryParse(s, provider, out var guid);
        result = guid;
        return parsed;
    }
}

[StronglyTypedId]
public partial struct UserId : IGuid, IParsable<UserId>
{
    public static implicit operator UserId(Guid guid)
    {
        return new UserId(guid);
    }

    public static UserId Parse(string s, IFormatProvider? provider)
    {
        return Guid.Parse(s, provider);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out UserId result)
    {
        var parsed = Guid.TryParse(s, provider, out var guid);
        result = guid;
        return parsed;
    }
}