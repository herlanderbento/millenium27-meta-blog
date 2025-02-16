using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.ValueObject;
using M27.MetaBlog.Infra.Data.Models;

namespace M27.MetaBlog.Infra.Data.Mappers;
public static class PostMapper
{
    public static PostModel ToModel(Post entity)
    {
        return new PostModel
        {
            Id = entity.Id,
            AuthorId = entity.AuthorId,
            CategoryId = entity.CategoryId,
            Title = entity.Title,
            Slug = entity.Slug.Value,
            Description = entity.Description,
            ImagePath = entity.Image.Path,
            Published = entity.Published,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt

        };
    }

    public static Post ToEntity(PostModel model)
    {
        return new Post(
            model.AuthorId,
            model.CategoryId,
            model.Title,
            model.Description,
            model.Published,
            model.ImagePath,
            Slug.Create(model.Title)
        );
    }
}
