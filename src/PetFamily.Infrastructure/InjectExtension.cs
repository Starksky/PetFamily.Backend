using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Species;
using PetFamily.Application.Volunteers;
using PetFamily.Infrastructure.Repositories;

namespace PetFamily.Infrastructure;

public static class InjectExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        return services;
    }
}