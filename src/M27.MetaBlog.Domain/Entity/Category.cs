using M27.MetaBlog.Domain.Shared;
using M27.MetaBlog.Domain.Shared.Validation;
using M27.MetaBlog.Domain.Validator;

namespace M27.MetaBlog.Domain.Entity;

public class Category: AggregateRoot
{
    public string Name { get; private set; }
    public string Description {  get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Category(string name, string description, bool isActive = true): base()
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description ?? Description;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }
    
    private void Validate()
    {
        CategoryValidator.Validate(Name, Description);
    }
}