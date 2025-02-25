using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Shared;
using M27.MetaBlog.Domain.Shared.SearchableRepository;

namespace M27.MetaBlog.Domain.Repository;

public interface IUserRepository: IRepository<User>, ISearchableRepository<User, string>
{
    public Task<User> GetByEmail(string email, CancellationToken cancellationToken);
    
    public Task<IReadOnlyList<User>> GetListByIds(
        List<Guid> ids,
        CancellationToken cancellationToken
    );
}