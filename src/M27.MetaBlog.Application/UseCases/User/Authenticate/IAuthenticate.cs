using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.Authenticate;

public interface IAuthenticate : IRequestHandler<AuthenticateInput, AuthenticateOutput>
{
    
}