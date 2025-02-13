using FluentValidation;
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

public class UpdateUserInputValidator : AbstractValidator<UpdateUserInput>
{
    public UpdateUserInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(255).WithMessage("Name must not exceed 255 characters.")
            .When(x => x.Name is not null);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .When(x => x.Email is not null);

        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .When(x => !string.IsNullOrEmpty(x.Password));

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid user role.")
            .When(x => x.Role.HasValue);
    }
}