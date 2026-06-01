using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateInfo;
using PetFamily.Application.Volunteers.UpdatePet;
using PetFamily.Application.Volunteers.UploadPetPhotos;

namespace PetFamily.Application;

public static class InjectExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<IValidator<CreateVolunteerRequest>, CreateVolunteerRequestValidator>();
        
        services.AddScoped<UpdateVolunteerInfoHandler>();
        services.AddScoped<IValidator<UpdateVolunteerInfoCommand>, UpdateVolunteerInfoDtoValidator>();
        services.AddScoped<IValidator<UpdateVolunteerInfoRequest>, UpdateVolunteerInfoRequestValidator>();
        
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<IValidator<DeleteVolunteerRequest>, DeleteVolunteerRequestValidator>();
        
        services.AddScoped<AddPetHandler>();
        services.AddScoped<IValidator<AddPetCommand>, AddPetDtoValidator>();
        services.AddScoped<IValidator<AddPetRequest>, AddPetRequestValidator>();
        
        services.AddScoped<UpdatePetHandler>();
        services.AddScoped<IValidator<UpdatePetCommand>, UpdatePetCommandValidator>();
        services.AddScoped<IValidator<UpdatePetRequest>, UpdatePetRequestValidator>();
        
        services.AddScoped<UploadPetPhotosHandler>();
        services.AddScoped<IValidator<UploadPetPhotosCommand>, UploadPetPhotosCommandValidator>();
        return services;
    }
}