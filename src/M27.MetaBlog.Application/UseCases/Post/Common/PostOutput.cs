using DomainEntities = M27.MetaBlog.Domain.Entity;

namespace M27.MetaBlog.Application.UseCases.Post.Common;

public record PostOutput
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Slug { get; init; }
    public string Description { get; init; }
    public bool Published { get; init; }
    public string? Image { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    
    // Alternative between AuthorId/CategoryId and Author/Category
    public Guid? AuthorId { get; init; }
    public Guid? CategoryId { get; init; }
    public PostOutputRelatedAggregate? Author { get; init; }
    public PostOutputRelatedAggregate? Category { get; init; }

    // Constructor for the first FromPost (returns IDs only)
    private PostOutput(
        Guid id,
        string title,
        string slug,
        string description,
        bool published,
        string? image,
        DateTime createdAt,
        DateTime updatedAt,
        Guid authorId,
        Guid categoryId)
    {
        Id = id;
        Title = title;
        Slug = slug;
        Description = description;
        Published = published;
        Image = image;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        AuthorId = authorId;
        CategoryId = categoryId;
    }

    // Constructor for the second FromPost (returns Author and Category with name)
    private PostOutput(
        Guid id,
        string title,
        string slug,
        string description,
        bool published,
        string? image,
        DateTime createdAt,
        DateTime updatedAt,
        PostOutputRelatedAggregate author,
        PostOutputRelatedAggregate category)
    {
        Id = id;
        Title = title;
        Slug = slug;
        Description = description;
        Published = published;
        Image = image;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Author = author;
        Category = category;
    }

    // First method FromPost -> returns IDs only
    public static PostOutput FromPost(DomainEntities.Post post) => new(
        post.Id,
        post.Title,
        post.Slug.Value,
        post.Description,
        post.Published,
        post.Image?.Path,
        post.CreatedAt,
        post.UpdatedAt,
        post.AuthorId,
        post.CategoryId
    );

    // Second method FromPost -> returns the names of Author and Category
    public static PostOutput FromPost(
        DomainEntities.Post post,
        DomainEntities.User? author = null,
        DomainEntities.Category? category = null
    ) => new(
        post.Id,
        post.Title,
        post.Slug.Value,
        post.Description,
        post.Published,
        post.Image?.Path,
        post.CreatedAt,
        post.UpdatedAt,
        new PostOutputRelatedAggregate(post.AuthorId, author?.Name),
        new PostOutputRelatedAggregate(post.CategoryId, category?.Name)
    );
}

public record PostOutputRelatedAggregate(Guid Id, string? Name = null);
