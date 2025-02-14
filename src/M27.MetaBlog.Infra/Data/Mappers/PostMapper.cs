using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.ValueObject;
using M27.MetaBlog.Infra.Data.Models;

namespace M27.MetaBlog.Infra.Data.Mappers;
public static class PostMapper
{
    public static PostModel ToModel(Post entity)
    {
        return new PostModel(
            entity.Id,
            entity.AuthorId,
            entity.CategoryId,
            entity.Title,
            entity.Slug.Value,
            entity.Description,
            entity.Published,
            entity.Image?.Path,
            entity.CreatedAt,
            entity.UpdatedAt
        );
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
