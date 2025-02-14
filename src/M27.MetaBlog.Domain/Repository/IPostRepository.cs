using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Shared;
using M27.MetaBlog.Domain.Shared.SearchableRepository;

namespace M27.MetaBlog.Domain.Repository;

public interface IPostRepository : IRepository<Post>, ISearchableRepository<Post>
{
    
}