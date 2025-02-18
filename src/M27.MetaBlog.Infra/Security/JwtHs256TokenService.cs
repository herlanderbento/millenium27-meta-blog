using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Domain.Entity;
using System.Text;


namespace M27.MetaBlog.Infra.Security;

public class JwtHs256TokenService: ITokenProvider
{
    private readonly string _secretKey;
    private readonly int _accessTokenExpirationMinutes = 60;
    
    public JwtHs256TokenService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"] 
                     ?? throw new ArgumentNullException("Jwt:SecretKey", "JWT Secret Key is missing in configuration.");
    }

    public string GenerateToken(User payload)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, payload.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Name, payload.Name.ToString()!),
            new Claim(JwtRegisteredClaimNames.Email, payload.Email.ToString()!),
            new Claim("role", payload.Role.ToString()!)
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
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