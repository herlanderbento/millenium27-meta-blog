using M27.MetaBlog.Domain.Enum;
namespace M27.MetaBlog.Application.UseCases.User.Common;

public class UserOutput
{
    public Guid Id { get; set; }
    public string Name { get;  set; }
    public string Email { get;  set; }
    public UserRole Role { get;  set; }
    public bool IsActive { get;  set; }
    public DateTime CreatedAt { get;  set; }
    public DateTime UpdatedAt { get;  set; }

    public UserOutput(
        Guid id, 
        string name, 
        string email, 
        UserRole role, 
        bool isActive, 
        DateTime createdAt, 
        DateTime updatedAt)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
        IsActive = isActive;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static UserOutput FromUser(Domain.Entity.User user)
        => new(
            user.Id, 
            user.Name,
            user.Email, 
            user.Role, 
            user.IsActive, 
            user.CreatedAt, 
            user.UpdatedAt
            );
}