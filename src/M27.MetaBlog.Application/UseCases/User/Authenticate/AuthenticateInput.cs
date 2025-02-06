using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.Authenticate;

public class AuthenticateInput : IRequest<AuthenticateOutput>
{
    public string Email { get;  set; }
    public string Password { get;  set; }
    
    public AuthenticateInput(string email, string password)
    {
        Email = email;
        Password = password;
    }
}