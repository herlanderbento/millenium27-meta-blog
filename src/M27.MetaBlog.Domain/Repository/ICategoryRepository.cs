using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Shared;
using M27.MetaBlog.Domain.Shared.SearchableRepository;

namespace M27.MetaBlog.Domain.Repository;

public interface ICategoryRepository :  IRepository<Category>, ISearchableRepository<Category, string>
{
    public Task<Category> GetByName(string name, CancellationToken cancellationToken);
    
    public Task<IReadOnlyList<Category>> GetListByIds(
        List<Guid> ids,
        CancellationToken cancellationToken
    );
}