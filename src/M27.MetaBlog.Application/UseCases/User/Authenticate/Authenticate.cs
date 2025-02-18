using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.User.Common;
using M27.MetaBlog.Domain.Repository;

namespace M27.MetaBlog.Application.UseCases.User.Authenticate;

public class Authenticate : IAuthenticate
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptography _cryptography;
    private readonly ITokenProvider _tokenProvider;
    
    public Authenticate(
        IUserRepository userRepository, 
        ICryptography cryptography, 
        ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _cryptography = cryptography;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthenticateOutput> Handle(
        AuthenticateInput input, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(input.Email, cancellationToken);

        NotFoundException.ThrowIfNull(user, "Email or password incorrect");
        
        var isPasswordValid = await _cryptography.Verify(input.Password, user.Password, cancellationToken);

        if (!isPasswordValid)
        {
            NotFoundException.ThrowIfNull(user, "Email or password incorrect");
        }

        var accessToken = _tokenProvider.GenerateToken(user);

        var refreshToken = _tokenProvider.GenerateRefreshToken();

        return new AuthenticateOutput(
            accessToken, 
            refreshToken, 
            user: UserOutput.FromUser(user)
            );
    }
}