using FluentValidation;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.UpdatePet;

public class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
    }
}

public class UpdatePetRequestValidator : AbstractValidator<UpdatePetRequest>
{
    public UpdatePetRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .When(r => !string.IsNullOrEmpty(r.Name));
        
        RuleFor(x => x.SpeciesId)
            .NotEmpty()
            .WithMessage("Species is required")
            .Must(x => Guid.TryParse(x, out var guid))
            .WithMessage("Species must be a valid guid")
            .When(r => !string.IsNullOrEmpty(r.SpeciesId));
        
        RuleFor(x => x.BreedId)
            .NotEmpty()
            .WithMessage("Breed is required")
            .Must(x => Guid.TryParse(x, out var guid))
            .WithMessage("Breed must be a valid guid")
            .When(r => !string.IsNullOrEmpty(r.BreedId));
        
        RuleFor(r => r.Address).MustBeValueObject(r =>
            Address.Create(r.PostalCode, r.City, r.Street, r.BuildingNumber, r.BuildingNumberTwo, r.ApartmentNumber))
            .When(r => r.Address != null);
        
        RuleFor(r => r.Description)
            .MustBeValueObject(Description.Create)
            .When(r => !string.IsNullOrEmpty(r.Description));
        
        RuleFor(r => r.Color)
            .MaximumLength(Constants.MaxLengthLowValueText)
            .WithMessage($"Color must not greater than {Constants.MaxLengthLowValueText} character")
            .When(r => !string.IsNullOrEmpty(r.Color));
        
        RuleFor(r => r.ContactPhone)
            .MaximumLength(Constants.MaxLengthLowValueText)
            .WithMessage($"ContactPhone must not greater than {Constants.MaxLengthLowValueText} character")
            .When(r => !string.IsNullOrEmpty(r.ContactPhone));
        
        RuleFor(r => r.HealthStatus)
            .MaximumLength(Constants.MaxLengthLowValueText)
            .WithMessage($"HealthStatus must not greater than {Constants.MaxLengthLowValueText} character")
            .Must(x => Enum.TryParse<HealthStatus>(x, true, out var status))
            .WithMessage($"Is not a valid health status")
            .When(r => !string.IsNullOrEmpty(r.HealthStatus));
        
        RuleFor(r => r.HelpStatus)
            .MaximumLength(Constants.MaxLengthLowValueText)
            .WithMessage($"HelpStatus must not greater than {Constants.MaxLengthLowValueText} character")
            .Must(x => Enum.TryParse<HelpStatus>(x, true, out var status))
            .WithMessage($"Is not a valid help status")
            .When(r => !string.IsNullOrEmpty(r.HelpStatus));
    }
}