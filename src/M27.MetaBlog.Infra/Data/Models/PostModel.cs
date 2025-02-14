using M27.MetaBlog.Domain.Entity;

namespace M27.MetaBlog.Infra.Data.Models;

public class PostModel(
    Guid id,
    Guid authorId,
    Guid categoryId,
    string title,
    string slug,
    string description,
    bool published,
    string? imagePath,
    DateTime createdAt,
    DateTime updatedAt
)
{
    public Guid Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string Slug { get; set; } = slug;
    public string Description { get; set; } = description;
    public bool Published { get; set; } = published;
    public string? ImagePath { get; set; } = imagePath;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;

    public Guid AuthorId { get; set; } = authorId;
    public Guid CategoryId { get; set; } = categoryId;

    public User Author { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
