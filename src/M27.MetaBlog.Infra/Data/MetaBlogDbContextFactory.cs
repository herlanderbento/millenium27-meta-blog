using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace M27.MetaBlog.Infra.Data;

public class MetaBlogDbContextFactory : IDesignTimeDbContextFactory<MetaBlogDbContext>
{
    public MetaBlogDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MetaBlogDbContext>();
        
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../M27.MetaBlog.Api");
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) 
            .AddJsonFile("appsettings.Docker.json", optional: true, reloadOnChange: true) 
            .Build();
        
        var connectionString = configuration.GetConnectionString("MetaBlogDb");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string is null or empty!");
        }

        optionsBuilder.UseNpgsql(connectionString);
        
        return new MetaBlogDbContext(optionsBuilder.Options);
    }
}
