using Amazon;
using Amazon.S3;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Infra.Storage.Configuration;
using M27.MetaBlog.Infra.Storage.Services;

namespace M27.MetaBlog.Api.Configurations;

public static class StorageConfiguration
{
    public static IServiceCollection AddStorage(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<StorageServiceOptions>(
            configuration.GetSection(StorageServiceOptions.ConfigurationSection));

        var options = configuration
            .GetSection(StorageServiceOptions.ConfigurationSection)
            .Get<StorageServiceOptions>();

        services.AddSingleton<IAmazonS3>(provider =>
        {
            
            var awsOptions = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(options?.Region ?? "us-east-1"),
                
            };
            
            return new AmazonS3Client(options.AccessKey, options.SecretKey, awsOptions);
        });

        services.AddTransient<IStorageService, StorageService>();

        return services;
    }
}
