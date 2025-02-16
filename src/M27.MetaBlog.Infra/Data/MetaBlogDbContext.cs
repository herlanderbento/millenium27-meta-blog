using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace M27.MetaBlog.Infra.Data;

public class MetaBlogDbContext: DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Post> Posts => Set<Post>();


    public MetaBlogDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new PostConfiguration());
    }
}