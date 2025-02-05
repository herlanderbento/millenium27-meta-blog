using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.User.CreateUser;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Infra.Cryptography;
using M27.MetaBlog.Infra.Data;
using M27.MetaBlog.Infra.Data.Repositories;

namespace M27.MetaBlog.Api.Configurations;


public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUser).Assembly));
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICryptography, BCryptHasher>();

        return services;
    }
    
}
