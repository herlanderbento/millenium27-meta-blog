using M27.MetaBlog.Application.UseCases.Category.Common;
using FluentValidation;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryInput : IRequest<CategoryOutput>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public CreateCategoryInput(
        string name,
        string? description = null,
        bool isActive = true)
    {
        Name = name;
        Description = description ?? "";
        IsActive = isActive;
    }
}

public class CreateCategoryInputValidator : AbstractValidator<CreateCategoryInput>
{
    public CreateCategoryInputValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(255).WithMessage("Category name must not exceed 255 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(10_000).WithMessage("Description must not exceed 10_000 characters.");
    }
}
