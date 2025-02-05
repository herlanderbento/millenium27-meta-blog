using M27.MetaBlog.Domain.Enum;

namespace M27.MetaBlog.Api.ApiModels.User;
using System.Text.Json.Serialization;

public class UpdateUserApiInput
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public UserRole? Role { get; set; }
    public bool? IsActive { get; set; }

    [JsonConstructor]
    public UpdateUserApiInput(
        string? name, 
        string? email, 
        string? password, 
        UserRole? role = UserRole.User, 
        bool? isActive = null)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        IsActive = isActive;
    }
}
