using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace M27.MetaBlog.Api.Configurations;

public static class SecurityConfiguration
{
    public static IServiceCollection AddSecurity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddJwtAuthentication(configuration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
                policy.RequireRole("Admin"));

            options.AddPolicy("Authenticated", policy =>
                policy.RequireAuthenticatedUser());
        });

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var algorithm = configuration["Jwt:Algorithm"] ?? "HS256";

        if (algorithm == "HS256")
        {
            var secretKey = configuration["Jwt:SecretKey"]
                            ?? throw new ArgumentNullException("Jwt:SecretKey is missing in configuration.");

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = key
                    };
                });
        }
        else if (algorithm == "RS256")
        {
            var privateKeyBase64 = configuration["Jwt:PrivateKey"]
                                   ?? throw new ArgumentNullException("Jwt:PrivateKey is missing in configuration.");
            var publicKeyBase64 = configuration["Jwt:PublicKey"]
                                  ?? throw new ArgumentNullException("Jwt:PublicKey is missing in configuration.");

            using var rsaPublic = RSA.Create();
            rsaPublic.ImportRSAPublicKey(Convert.FromBase64String(publicKeyBase64), out _);

            using var rsaPrivate = RSA.Create();
            rsaPrivate.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyBase64), out _);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new RsaSecurityKey(rsaPublic)
                    };
                });
        }
        else
        {
            throw new NotSupportedException($"JWT Algorithm '{algorithm}' is not supported.");
        }

        return services;
    }
}
