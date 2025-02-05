using M27.MetaBlog.Application.UseCases.User.Common;
using M27.MetaBlog.Domain.Enum;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.User.UpdateUser;

public class UpdateUserInput
    :IRequest<UserOutput>
{
    public Guid Id { get; set; }
    public string? Name { get;  set; }
    public string? Email { get;  set; }
    public string? Password { get;  set; }
    public UserRole? Role { get;  set; }
    public bool? IsActive { get;  set; }

    public UpdateUserInput(
        Guid id,
        string? name, 
        string? email, 
        string? password, 
        UserRole? role = UserRole.User, 
        bool? isActive = null)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        IsActive = isActive;
    }
}