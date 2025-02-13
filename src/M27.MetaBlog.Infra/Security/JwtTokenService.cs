using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using M27.MetaBlog.Application.Interfaces;

namespace M27.MetaBlog.Infra.Security;

public class JwtTokenService : ITokenProvider
{
    private readonly RSA _rsa;
    private readonly int _accessTokenExpirationMinutes = 60;
    private readonly string _publicKey;
/*
    public JwtTokenService(IConfiguration configuration)
    {
        var privateKeyBase64 = configuration["Jwt:PrivateKey"] ?? throw new ArgumentNullException("JWT Private Key is missing in configuration.");
        _publicKey = configuration["Jwt:PublicKey"] ?? throw new ArgumentNullException("JWT Public Key is missing in configuration.");

        Console.WriteLine($"Loaded Private Key: {privateKeyBase64.Substring(0, 20)}..."); // Apenas um pedaço da chave para verificação
        _rsa = RSA.Create();
        _rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyBase64), out _);
    }
*/

    public JwtTokenService(IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration), "IConfiguration não foi injetado corretamente.");
        }

        var privateKeyBase64 = configuration["Jwt:PrivateKey"];
        var publicKeyBase64 = configuration["Jwt:PublicKey"];

        if (string.IsNullOrEmpty(privateKeyBase64))
        {
            throw new ArgumentNullException("Jwt:PrivateKey", "JWT Private Key is missing in configuration.");
        }

        if (string.IsNullOrEmpty(publicKeyBase64))
        {
            throw new ArgumentNullException("Jwt:PublicKey", "JWT Public Key is missing in configuration.");
        }

        Console.WriteLine($"Loaded Private Key: {privateKeyBase64.Substring(0, 20)}..."); // Apenas um pedaço da chave para verificação
        _rsa = RSA.Create();
        _rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyBase64), out _);
        _publicKey = publicKeyBase64;
    }

    public string GenerateToken(object payload)
    {
        var credentials = new SigningCredentials(new RsaSecurityKey(_rsa), SecurityAlgorithms.RsaSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, payload.ToString()!)
        };

        var token = new JwtSecurityToken(
            issuer: "M27.MetaBlog",
            audience: "M27.MetaBlog",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            using var rsaPublic = RSA.Create();
            rsaPublic.ImportRSAPublicKey(Convert.FromBase64String(_publicKey), out _);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(rsaPublic),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "M27.MetaBlog",
                ValidAudience = "M27.MetaBlog",
                ClockSkew = TimeSpan.Zero
            }, out _);

            return true;
        }
        catch
        {
            return false;
        }
    }
}

/*

public class JwtTokenService : ITokenProvider
{
    private readonly RSA _rsa;
    private readonly int _accessTokenExpirationMinutes = 60;
    private readonly string _publicKey;

    public JwtTokenService(IConfiguration configuration)
    {
        var privateKeyBase64 = RemovePemHeaders(configuration["Jwt:PrivateKey"] ?? throw new ArgumentNullException("JWT Private Key is missing in configuration."));
        _publicKey = RemovePemHeaders(configuration["Jwt:PublicKey"] ?? throw new ArgumentNullException("JWT Public Key is missing in configuration."));

        Console.WriteLine($"Loaded Private Key: {privateKeyBase64.Substring(0, 20)}..."); // Apenas um pedaço da chave para verificação
        
        _rsa = RSA.Create();
        _rsa.ImportFromPem(configuration["Jwt:PrivateKey"]);
    }

    private string RemovePemHeaders(string key)
    {
        return key.Replace("-----BEGIN PUBLIC KEY-----", "")
                  .Replace("-----END PUBLIC KEY-----", "")
                  .Replace("-----BEGIN PRIVATE KEY-----", "")
                  .Replace("-----END PRIVATE KEY-----", "")
                  .Replace("\n", "")
                  .Replace("\r", "")
                  .Trim();
    }

    public string GenerateToken(object payload)
    {
        var credentials = new SigningCredentials(new RsaSecurityKey(_rsa), SecurityAlgorithms.RsaSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, payload.ToString()!)
        };

        var token = new JwtSecurityToken(
            issuer: "M27.MetaBlog",
            audience: "M27.MetaBlog",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        Console.WriteLine($"Generated Token: {tokenString}");
        return tokenString;
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            using var rsaPublic = RSA.Create();
            rsaPublic.ImportFromPem(_publicKey);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(rsaPublic),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "M27.MetaBlog",
                ValidAudience = "M27.MetaBlog",
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, parameters, out _);
            
            Console.WriteLine("Token is valid.");
            Console.WriteLine($"Token Claims: {string.Join(", ", principal.Claims.Select(c => c.Type + ": " + c.Value))}");
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token validation failed: {ex.Message}");
            return false;
        }
    }
}
*/