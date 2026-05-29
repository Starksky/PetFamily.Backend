using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateInfo;

namespace PetFamily.Application;

public static class InjectExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<IValidator<CreateVolunteerRequest>, CreateVolunteerRequestValidator>();
        
        services.AddScoped<UpdateVolunteerInfoHandler>();
        services.AddScoped<IValidator<UpdateVolunteerInfoDto>, UpdateVolunteerInfoDtoValidator>();
        services.AddScoped<IValidator<UpdateVolunteerInfoRequest>, UpdateVolunteerInfoRequestValidator>();
        
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<IValidator<DeleteVolunteerRequest>, DeleteVolunteerRequestValidator>();
        
        services.AddScoped<AddPetHandler>();
        services.AddScoped<IValidator<AddPetDto>, AddPetDtoValidator>();
        services.AddScoped<IValidator<AddPetRequest>, AddPetRequestValidator>();
        return services;
    }
}