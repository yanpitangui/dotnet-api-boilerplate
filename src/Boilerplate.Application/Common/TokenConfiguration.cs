namespace Boilerplate.Application.Common;

public class TokenConfiguration
{
    public string Secret { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }

}