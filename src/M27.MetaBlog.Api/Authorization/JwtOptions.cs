namespace M27.MetaBlog.Api.Authorization;
public class JwtOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
}

