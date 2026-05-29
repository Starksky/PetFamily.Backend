using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.Species;
using PetFamily.Domain.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IVolunteersRepository _volunteerRepository;
    private readonly ISpeciesRepository _speciesRepository;

    public AddPetHandler(IVolunteersRepository volunteersRepository, ISpeciesRepository speciesRepository)
    {
        _volunteerRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, Error>> HandleAsync(AddPetDto dto, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var volunteer = volunteerResult.Value;
        
        var speciesId = SpeciesId.Create(Guid.Parse(dto.Request.SpeciesId));
        var speciesResult = await _speciesRepository.GetById(speciesId, cancellationToken);
        if (speciesResult.IsFailure)
            return speciesResult.Error.WithPropertyName(nameof(dto.Request.SpeciesId));
            
        var species = speciesResult.Value;
        var breedId = BreedId.Create(Guid.Parse(dto.Request.BreedId));
        var resultHasBreed = species.HasBreed(breedId);
        if (resultHasBreed.IsFailure)
            return resultHasBreed.Error.WithPropertyName(nameof(dto.Request.BreedId));
            
        var petId = PetId.NewId();
        var petResult = Pet.Create(petId, dto.Request.Name, speciesId, breedId);
        if (petResult.IsFailure)
            return petResult.Error;
        
        var addPetsResult = await _volunteerRepository.AddPetAsync(volunteer, petResult.Value, cancellationToken);
        if (addPetsResult.IsFailure)
            return addPetsResult.Error;

        return petId.Value;
    }
}

public record AddPetRequest(string Name, string SpeciesId, string BreedId);
public record AddPetDto(Guid Id, AddPetRequest Request);

public class AddPetDtoValidator : AbstractValidator<AddPetDto>
{
    public AddPetDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
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