using BCrypt.Net;
using M27.MetaBlog.Application.Interfaces;
namespace M27.MetaBlog.Infra.Cryptography;

public class BCryptHasher: ICryptography
{
    private const int HashSaltLength  = 8;

    public Task<string> HashPassword(string password, CancellationToken cancellationToken)
    {
        return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password, HashSaltLength));
    }

    public Task Verify(string password, string passwordHash, CancellationToken cancellationToken)
    {
        return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, passwordHash));
    }
}