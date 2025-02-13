namespace M27.MetaBlog.Application.UseCases.Category.Common;

public class CategoryOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public CategoryOutput(
        Guid id, 
        string name, 
        string description, 
        bool isActive, 
        DateTime createdAt,
        DateTime updatedAt
        )
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static CategoryOutput FromCategory(Domain.Entity.Category category)
        => new (  
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt, 
            category.UpdatedAt
            );
}