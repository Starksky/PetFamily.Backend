using FluentValidation;

namespace PetFamily.Application.Volunteers.AddPet;


public class AddPetDtoValidator : AbstractValidator<AddPetDto>
{
    public AddPetDtoValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty();
    }
}

public class AddPetRequestValidator : AbstractValidator<AddPetRequest>
{
    public AddPetRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.SpeciesId)
            .NotEmpty()
            .WithMessage("Species is required")
            .Must(x => Guid.TryParse(x, out var guid))
            .WithMessage("Species must be a valid guid");
        RuleFor(x => x.BreedId)
            .NotEmpty()
            .WithMessage("Breed is required")
            .Must(x => Guid.TryParse(x, out var guid))
            .WithMessage("Breed must be a valid guid");
    }
}