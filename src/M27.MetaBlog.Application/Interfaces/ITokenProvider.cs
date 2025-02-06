namespace M27.MetaBlog.Application.Interfaces;

public interface ITokenProvider
{
    string GenerateToken(object payload);
    string GenerateRefreshToken();
    bool ValidateToken(string token);
}