using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.CreateVolunteer;

namespace PetFamily.Application;

public static class InjectExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<IValidator<CreateVolunteerRequest>, CreateVolunteerRequestValidator>();
        return services;
    }
}