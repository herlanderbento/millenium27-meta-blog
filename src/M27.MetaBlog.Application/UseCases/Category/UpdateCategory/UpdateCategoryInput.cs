using FluentValidation;
using M27.MetaBlog.Application.UseCases.Category.Common;
using MediatR;

namespace M27.MetaBlog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategoryInput : IRequest<CategoryOutput>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    
    public UpdateCategoryInput(
        Guid id, 
        string name, 
        string? description = null, 
        bool? isActive = null)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
    }
}


public class UpdateCategoryInputValidator : AbstractValidator<UpdateCategoryInput>
{
    public UpdateCategoryInputValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name)
            .MaximumLength(255).WithMessage("Category name must not exceed 255 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(10_000).WithMessage("Description must not exceed 10_000 characters.");
    }
}