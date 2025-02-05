using Microsoft.Extensions.Logging;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Domain.Shared;

namespace M27.MetaBlog.Infra.Data;

public class UnitOfWork
    : IUnitOfWork
{
    private readonly MetaBlogDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;


    public UnitOfWork(MetaBlogDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        var aggregateRoots = _context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity);

        _logger.LogInformation(
            "Commit: {AggregatesCount} aggregate roots with events.",
            aggregateRoots.Count());
        

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task Rollback(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}