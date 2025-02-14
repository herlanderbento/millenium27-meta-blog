using DomainEntities = M27.MetaBlog.Domain.Entity;

namespace M27.MetaBlog.Application.UseCases.Post.Common;

public record PostOutput(
    Guid Id,
    string Title,
    string Slug,
    string Description,
    bool Published,
    string? Image,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    PostOutputRelatedAggregate Author,
    PostOutputRelatedAggregate Category)
{
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
        post.Image is not null ? post.Image.Path : null,
        post.CreatedAt,
        post.UpdatedAt,
        new PostOutputRelatedAggregate(post.AuthorId, author?.Name ?? "Unknown Author"),
        new PostOutputRelatedAggregate(post.CategoryId, category?.Name ?? "Unknown Category")
    );
}

public record PostOutputRelatedAggregate(Guid Id, string? Name = null);