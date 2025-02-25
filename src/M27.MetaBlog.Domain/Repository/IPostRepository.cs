using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Shared;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using M27.MetaBlog.Domain.ValueObject;

namespace M27.MetaBlog.Domain.Repository;

public class PostSearch
{
    public string? Title { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? AuthorId { get; set; }
}


public interface IPostRepository : IRepository<Post>, ISearchableRepository<Post, PostSearch>
{
    Task<Post> GetBySlug(Slug slug, CancellationToken cancellationToken = default);
}
