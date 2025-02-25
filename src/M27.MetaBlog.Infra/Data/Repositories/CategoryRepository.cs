using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace M27.MetaBlog.Infra.Data.Repositories;

public class CategoryRepository(MetaBlogDbContext context): ICategoryRepository
{
    
    private readonly MetaBlogDbContext _context = context;
    private DbSet<Category> Categories => _context.Set<Category>();
    
    public async Task Insert(Category aggregate, CancellationToken cancellationToken)
        => await Categories.AddAsync(aggregate, cancellationToken);
    
    public async Task<Category> GetById(Guid id, CancellationToken cancellationToken)
    {
        var model = await Categories.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id, cancellationToken);
        
        NotFoundException.ThrowIfNull(model, $"Category {id} not found");

        return model!;
    }
    
    public async  Task<Category> GetByName(string name, CancellationToken cancellationToken)
    {
        var model = await Categories.AsNoTracking().FirstOrDefaultAsync(
            x => x.Name == name, cancellationToken);
        return model!;
    }

    public async Task<IReadOnlyList<Category>> GetListByIds(List<Guid> ids, CancellationToken cancellationToken)
    {
        var models = await Categories.AsNoTracking()
            .Where(category => ids.Contains(category.Id))
            .ToListAsync();
        
        return models;
    }

    public Task Update(Category aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(Categories.Update(aggregate));
    }

    public Task Delete(Category aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(Categories.Remove(aggregate));
    }

    public async Task<SearchOutput<Category>> Search(SearchInput<string> input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = Categories.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if(!String.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.Contains(input.Search));
        var total = await query.CountAsync();
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        return new(input.Page, input.PerPage, total, items);
    }

    private IQueryable<Category> AddOrderToQuery(
        IQueryable<Category> query,
        string orderProperty,
        SearchOrder order
    )
    { 
        return (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name).ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdAt", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdAt", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name).ThenBy(x => x.Id)
        };
    }
}