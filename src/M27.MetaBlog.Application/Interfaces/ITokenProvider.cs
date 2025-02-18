using M27.MetaBlog.Domain.Entity;

namespace M27.MetaBlog.Application.Interfaces;

public interface ITokenProvider
{
    //string GenerateToken(object payload);
    string GenerateToken(User payload);

    string GenerateRefreshToken();
    bool ValidateToken(string token);
}