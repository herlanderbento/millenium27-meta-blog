using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Shared;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using M27.MetaBlog.Domain.ValueObject;

namespace M27.MetaBlog.Domain.Repository;

public interface IPostRepository : IRepository<Post>, ISearchableRepository<Post>
{
    public Task<Post> GetBySlug(Slug slug, CancellationToken cancellationToken = default);
}