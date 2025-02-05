using MediatR;
using M27.MetaBlog.Domain.Enum;
using M27.MetaBlog.Application.UseCases.User.Common;

namespace M27.MetaBlog.Application.UseCases.User.CreateUser;

public class CreateUserInput: IRequest<UserOutput>
{
    public string Name { get;  set; }
    public string Email { get;  set; }
    public string Password { get;  set; }
    public UserRole Role { get;  set; }
    public bool IsActive { get;  set; }

    public CreateUserInput(
        string name, 
        string email, 
        string password, 
        UserRole role = UserRole.User, 
        bool isActive = false)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        IsActive = isActive;
    }
}