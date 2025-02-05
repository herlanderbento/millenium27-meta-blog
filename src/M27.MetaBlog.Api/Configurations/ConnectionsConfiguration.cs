using Microsoft.EntityFrameworkCore;
using M27.MetaBlog.Infra.Data;

namespace M27.MetaBlog.Api.Configurations;


public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConections(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);
        return services;
    }
    
    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration
            .GetConnectionString("MetaBlogDb");
        services.AddDbContext<MetaBlogDbContext>(
            options => options.UseNpgsql(
                connectionString)
        );
        return services;
    }
    
    public static WebApplication MigrateDatabase(
        this WebApplication app)
    {
        var environment = Environment
            .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment == "EndToEndTest") return app;
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<MetaBlogDbContext>();
        dbContext.Database.Migrate();
        return app;
    }
}