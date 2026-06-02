using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Providers;
using PetFamily.Application.Shared;
using PetFamily.Application.Species;
using PetFamily.Application.Volunteers;
using PetFamily.Infrastructure.Interceptors;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Providers;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;

public static class InjectExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        
        services.AddSingleton<SoftDeleteInterceptor>();
        services.AddSingleton<AuditInterceptor>();

        services.AddMinio(configuration);
        
        return services;
    }
    
    public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.SectionName));
        
        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.SectionName).Get<MinioOptions>() 
                               ?? throw new ArgumentNullException(MinioOptions.SectionName);
            
            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey); 
            options.WithSSL(minioOptions.WithSsl);
        });
        
        services.AddScoped<IFileProvider, MinioProvider>();
        
        return services;
    }
}