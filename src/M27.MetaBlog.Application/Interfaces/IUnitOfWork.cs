namespace M27.MetaBlog.Application.Interfaces;

public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken);
    public Task Rollback(CancellationToken cancellationToken);
}