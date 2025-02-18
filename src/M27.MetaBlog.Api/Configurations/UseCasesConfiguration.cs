using FluentValidation;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Application.UseCases.Category.CreateCategory;
using M27.MetaBlog.Application.UseCases.User.Authenticate;
using M27.MetaBlog.Application.UseCases.User.CreateUser;
using M27.MetaBlog.Application.UseCases.User.UpdateUser;
using M27.MetaBlog.Domain.Repository;
using M27.MetaBlog.Infra.Cryptography;
using M27.MetaBlog.Infra.Data;
using M27.MetaBlog.Infra.Data.Repositories;
using M27.MetaBlog.Infra.Security;

namespace M27.MetaBlog.Api.Configurations;


public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUser).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Authenticate).Assembly));
        services.AddRepositories();
        services.AddValidators();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICryptography, BCryptHasher>();
       // services.AddSingleton<ITokenProvider, JwtTokenService>();
        services.AddSingleton<ITokenProvider, JwtHs256TokenService>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IPostRepository, PostRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddValidators(
        this IServiceCollection services
    )
    {
        services.AddValidatorsFromAssemblyContaining<CreateUserInputValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateUserInputValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCategoryInputValidator>();
        return services;
    }
    
}
