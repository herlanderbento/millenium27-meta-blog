using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using M27.MetaBlog.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace M27.MetaBlog.Infra.Data.Repositories;

public class PostRepository(MetaBlogDbContext context) : IPostRepository
{
    
    private readonly MetaBlogDbContext _context = context;
    private DbSet<Post> PostModels => _context.Set<Post>();
    
    public async Task Insert(Post aggregate, CancellationToken cancellationToken)
    {
        await PostModels.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Post> GetById(Guid id, CancellationToken cancellationToken)
    {
        var model = await PostModels
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    
        return model!;
    }
    
    public async Task<Post> GetBySlug(Slug slug, CancellationToken cancellationToken)
    {
        var model = await PostModels
            .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
        return model!;
    }
    
    public async Task Update(Post aggregate, CancellationToken cancellationToken)
    {
        await Task.FromResult(PostModels.Update(aggregate));
    }

    public async Task Delete(Post aggregate, CancellationToken cancellationToken)
    {
        await Task.FromResult(PostModels.Update(aggregate));
    }

    public async Task<SearchOutput<Post>> Search(SearchInput<PostSearch> input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = PostModels.AsNoTracking();
        
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if (!String.IsNullOrWhiteSpace(input.Search?.Title))
            query = query.Where(x => x.Title.Contains(input.Search.Title!));
        
        if (!String.IsNullOrWhiteSpace(input.Search?.CategoryId.ToString()))
            query = query.Where(x => x.CategoryId == input.Search.CategoryId!);
        
        var total = await query.CountAsync();
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        
        return new(
            input.Page, 
            input.PerPage, 
            total, 
            items
            );
    }

    private IQueryable<Post> AddOrderToQuery(
        IQueryable<Post> query,
        string orderProperty,
        SearchOrder order
    )
    { 
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Title)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Title)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdAt", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdAt", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Title)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}