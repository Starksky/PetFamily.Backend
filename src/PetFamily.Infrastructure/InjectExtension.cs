using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetFamily.Application.Species;
using PetFamily.Application.Volunteers;
using PetFamily.Infrastructure.Interceptors;
using PetFamily.Infrastructure.Options;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;

public static class InjectExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        services.AddScoped<SoftDeleteInterceptor>();
        services.AddScoped<AuditInterceptor>();

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
        
        return services;
    }
}