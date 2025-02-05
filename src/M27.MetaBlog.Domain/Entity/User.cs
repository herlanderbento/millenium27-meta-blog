using M27.MetaBlog.Domain.Enum;
using M27.MetaBlog.Domain.Shared;
using M27.MetaBlog.Domain.Shared.Validation;
using M27.MetaBlog.Domain.Validator;

namespace M27.MetaBlog.Domain.Entity;

public class User: AggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public User(
        string name, 
        string email,
        string password, 
        UserRole role = UserRole.User,
        bool isActive = false)
        : base()
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        
    }

    public void Update(string? name, string? email, UserRole? role, bool? isActive)
    {
        Name = name ?? Name;
        Email = email ?? Email;
        IsActive = isActive ?? IsActive;
        Role = role ?? Role;

        UpdatedAt = DateTime.Now;

    }

    public void ChangePassword(string? password)
    {
        Password = password;
    }
    
    public void Activate()
    {
        IsActive = true;
    }
    
    public void Deactivate()
    {
        IsActive = false;
    }

    private void Validate(ValidationHandler handler)
        => new UserValidator(this, handler).Validate();
}