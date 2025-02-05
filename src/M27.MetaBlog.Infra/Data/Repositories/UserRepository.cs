﻿using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Domain.Shared.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace M27.MetaBlog.Infra.Data.Repositories;

public class UserRepository
    : IUserRepository
{
    private readonly MetaBlogDbContext _context;
    private DbSet<User> Users => _context.Set<User>();

    public UserRepository(MetaBlogDbContext context)
         =>  _context = context;

    public async Task Insert(User aggregate, CancellationToken cancellationToken)
        => await Users.AddAsync(aggregate, cancellationToken);
    
    public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        var model = await Users.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id, 
            cancellationToken
            );
        
        NotFoundException.ThrowIfNull(model, $"User '{id}' not found.");

        return model!;
    }
    
    public async Task<User> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var model = await Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        
        return model!;
    }
    
    public async Task<SearchOutput<User>> Search(SearchInput input, CancellationToken cancellationToken)
    {
            var toSkip = (input.Page - 1) * input.PerPage;
            var query = Users.AsNoTracking();
            query = AddOrderToQuery(query, input.OrderBy, input.Order);
            if(!String.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));
            var total = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip(toSkip)
                .Take(input.PerPage)
                .ToListAsync(cancellationToken);
            return new SearchOutput<User>(input.Page, input.PerPage, total, items);
    }
    
    public Task Update(User aggregate, CancellationToken _)
        => Task.FromResult(Users.Update(aggregate));
    
    public Task Delete(User aggregate, CancellationToken _)
        => Task.FromResult(Users.Remove(aggregate));
    
    private IQueryable<User> AddOrderToQuery(
        IQueryable<User> query,
        string orderProperty,
        SearchOrder order
    )
    { 
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdAt", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdAt", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }


}