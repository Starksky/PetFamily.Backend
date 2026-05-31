using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository, 
        ISpeciesRepository speciesRepository, 
        ILogger<AddPetHandler> logger)
    {
        _volunteerRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(AddPetDto dto, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetByIdAsync(dto.VolunteerId, cancellationToken);
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

        _logger.LogInformation("Volunteer with id {id} added a pet with id {petId}", volunteer.Id.Value, petId.Value);

        return petId.Value;
    }
}