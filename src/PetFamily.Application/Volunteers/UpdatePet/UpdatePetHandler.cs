using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Species;
using PetFamily.Domain.Contracts;
using PetFamily.Domain.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.UpdatePet;

public class UpdatePetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<UpdatePetHandler> _logger;

    public UpdatePetHandler(
        IVolunteersRepository volunteersRepository, 
        ISpeciesRepository speciesRepository, 
        ILogger<UpdatePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(UpdatePetCommand command, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var volunteer = volunteerResult.Value;
        
        var petId = PetId.Create(command.PetId);
        var petResult = volunteer.GetPet(petId);
        if (petResult.IsFailure)
            return petResult.Error;
        
        var pet = petResult.Value;

        SpeciesId? speciesId = null;
        Domain.Species.Species? species = null;
        if (command.Request.SpeciesId != null)
        {
            speciesId = SpeciesId.Create(Guid.Parse(command.Request.SpeciesId));
            var speciesResult = await _speciesRepository.GetById(speciesId, cancellationToken);
            if (speciesResult.IsFailure)
                return speciesResult.Error.WithPropertyName(nameof(command.Request.SpeciesId));
            
            species = speciesResult.Value;
        }
        
        
        BreedId? breedId = null;
        if (command.Request.BreedId != null)
        {
            if (species == null)
                species = (await _speciesRepository.GetById(pet.SpeciesId, cancellationToken)).Value;
            
            breedId = BreedId.Create(Guid.Parse(command.Request.BreedId));
            var resultHasBreed = species.HasBreed(breedId);
            if (resultHasBreed.IsFailure)
                return resultHasBreed.Error.WithPropertyName(nameof(command.Request.BreedId));
        } 
        else if (command.Request.SpeciesId != null)
            return Errors.General.NotFound(null, nameof(command.Request.BreedId));
        

        Address? address = null;
        if (command.Request.Address != null)
        {
            var dtoAddress = command.Request.Address;
            var addressResult = Address.Create(
                dtoAddress.PostalCode, 
                dtoAddress.City, 
                dtoAddress.Street, 
                dtoAddress.BuildingNumber, 
                dtoAddress.BuildingNumberTwo, 
                dtoAddress.ApartmentNumber);
            
            if (addressResult.IsFailure)
                return addressResult.Error.WithPropertyName(nameof(command.Request.Address));
            
            address = addressResult.Value;
        }
        
        Description? description = null;
        if (!string.IsNullOrWhiteSpace(command.Request.Description))
        {
            var descriptionResult = Description.Create(command.Request.Description);
            if (descriptionResult.IsFailure)
                return descriptionResult.Error.WithPropertyName(nameof(command.Request.Description));
            
            description = descriptionResult.Value;
        }
        
        HealthStatus? healthStatus = null;
        if (!string.IsNullOrWhiteSpace(command.Request.HealthStatus))
        {
            if (!Enum.TryParse<HealthStatus>(command.Request.HealthStatus, true, out var status))
                return Errors.General.Validation(nameof(command.Request.HealthStatus));
            
            healthStatus = status;
        }
        
        HelpStatus? helpStatus = null;
        if (!string.IsNullOrWhiteSpace(command.Request.HelpStatus))
        {
            if (!Enum.TryParse<HelpStatus>(command.Request.HelpStatus, true, out var status))
                return Errors.General.Validation(nameof(command.Request.HelpStatus));
            
            helpStatus = status;
        }
        
        var petUpdateData = new UpdatePetData(
            command.Request.Name,
            speciesId,
            breedId,
            address,
            description,
            command.Request.Color,
            command.Request.ContactPhone,
            command.Request.Height,
            command.Request.Width,
            command.Request.IsVaccinated,
            command.Request.IsNeutered,
            healthStatus,
            helpStatus);
        
        pet.Update(petUpdateData);
        
        await _volunteersRepository.SaveAsync(volunteer, cancellationToken);
        
        _logger.LogInformation("Volunteer with id {id} updated a pet with id {petId}", volunteer.Id.Value, petId.Value);
        
        return command.PetId;
    }
}