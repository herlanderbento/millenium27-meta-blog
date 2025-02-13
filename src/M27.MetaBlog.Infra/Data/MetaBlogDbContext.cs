using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace M27.MetaBlog.Infra.Data;

public class MetaBlogDbContext: DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();


    public MetaBlogDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
    }
    
    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    // {
    //     var entries = ChangeTracker
    //         .Entries<User>()
    //         .Where(e => e.State == EntityState.Modified);
    //
    //     foreach (var entry in entries)
    //     {
    //         entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
    //     }
    //
    //     return base.SaveChangesAsync(cancellationToken);
    // }
}