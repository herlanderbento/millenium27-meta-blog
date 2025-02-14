using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using M27.MetaBlog.Infra.Data.Mappers;
using M27.MetaBlog.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace M27.MetaBlog.Infra.Data.Repositories;

public class PostRepository(MetaBlogDbContext context) : IPostRepository
{
    
    private readonly MetaBlogDbContext _context = context;
    private DbSet<PostModel> PostModels => _context.Set<PostModel>();
    
    public async Task Insert(Post aggregate, CancellationToken cancellationToken)
    {
        var modelProps = PostMapper.ToModel(aggregate);
        Console.WriteLine(modelProps);
        await PostModels.AddAsync(modelProps, cancellationToken);
    }

    public Task<Post> GetById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Post aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Post aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SearchOutput<Post>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}