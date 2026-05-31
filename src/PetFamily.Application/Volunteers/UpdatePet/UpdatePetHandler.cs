using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
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

    public async Task<Result<Guid, Error>> HandleAsync(UpdatePetDto dto, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteersRepository.GetByIdAsync(dto.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var volunteer = volunteerResult.Value;
        
        var petId = PetId.Create(dto.PetId);
        var petResult = volunteer.GetPet(petId);
        if (petResult.IsFailure)
            return petResult.Error;
        
        var pet = petResult.Value;

        SpeciesId? speciesId = null;
        Domain.Species.Species? species = null;
        if (dto.Request.SpeciesId != null)
        {
            speciesId = SpeciesId.Create(Guid.Parse(dto.Request.SpeciesId));
            var speciesResult = await _speciesRepository.GetById(speciesId, cancellationToken);
            if (speciesResult.IsFailure)
                return speciesResult.Error.WithPropertyName(nameof(dto.Request.SpeciesId));
            
            species = speciesResult.Value;
        }
        
        
        BreedId? breedId = null;
        if (dto.Request.BreedId != null)
        {
            if (species == null)
                species = (await _speciesRepository.GetById(pet.SpeciesId, cancellationToken)).Value;
            
            breedId = BreedId.Create(Guid.Parse(dto.Request.BreedId));
            var resultHasBreed = species.HasBreed(breedId);
            if (resultHasBreed.IsFailure)
                return resultHasBreed.Error.WithPropertyName(nameof(dto.Request.BreedId));
        } 
        else if (dto.Request.SpeciesId != null)
            return Errors.General.NotFound(null, nameof(dto.Request.BreedId));
        

        Address? address = null;
        if (dto.Request.Address != null)
        {
            var dtoAddress = dto.Request.Address;
            var addressResult = Address.Create(
                dtoAddress.PostalCode, 
                dtoAddress.City, 
                dtoAddress.Street, 
                dtoAddress.BuildingNumber, 
                dtoAddress.BuildingNumberTwo, 
                dtoAddress.ApartmentNumber);
            
            if (addressResult.IsFailure)
                return addressResult.Error.WithPropertyName(nameof(dto.Request.Address));
            
            address = addressResult.Value;
        }
        
        Description? description = null;
        if (!string.IsNullOrWhiteSpace(dto.Request.Description))
        {
            var descriptionResult = Description.Create(dto.Request.Description);
            if (descriptionResult.IsFailure)
                return descriptionResult.Error.WithPropertyName(nameof(dto.Request.Description));
            
            description = descriptionResult.Value;
        }
        
        HealthStatus? healthStatus = null;
        if (!string.IsNullOrWhiteSpace(dto.Request.HealthStatus))
        {
            if (!Enum.TryParse<HealthStatus>(dto.Request.HealthStatus, true, out var status))
                return Errors.General.Validation(nameof(dto.Request.HealthStatus));
            
            healthStatus = status;
        }
        
        HelpStatus? helpStatus = null;
        if (!string.IsNullOrWhiteSpace(dto.Request.HelpStatus))
        {
            if (!Enum.TryParse<HelpStatus>(dto.Request.HelpStatus, true, out var status))
                return Errors.General.Validation(nameof(dto.Request.HelpStatus));
            
            helpStatus = status;
        }
        
        var petUpdateData = new UpdatePetData(
            dto.Request.Name,
            speciesId,
            breedId,
            address,
            description,
            dto.Request.Color,
            dto.Request.ContactPhone,
            dto.Request.Height,
            dto.Request.Width,
            dto.Request.IsVaccinated,
            dto.Request.IsNeutered,
            healthStatus,
            helpStatus);
        
        pet.Update(petUpdateData);
        
        await _volunteersRepository.SaveAsync(volunteer, cancellationToken);
        
        _logger.LogInformation("Volunteer with id {id} updated a pet with id {petId}", volunteer.Id.Value, petId.Value);
        
        return dto.PetId;
    }
}

public record UpdatePetDto(Guid VolunteerId, Guid PetId, UpdatePetRequest Request);
public record UpdatePetRequest(
    string? Name, 
    string? SpeciesId, 
    string? BreedId,
    AddressDto? Address,
    string? Description,
    string? Color,
    string? ContactPhone,
    double? Height,
    double? Width,
    bool? IsVaccinated,
    bool? IsNeutered,
    string? HealthStatus,
    string? HelpStatus);

public record AddressDto(
    string PostalCode,
    string City,
    string Street,
    int BuildingNumber,
    int? BuildingNumberTwo,
    int? ApartmentNumber);

/*public record PhotoDto(Stream stream);
public record PhotosContainerDto(IEnumerable<PhotoDto> Photos);*/

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

public class UpdatePetDtoValidator : AbstractValidator<UpdatePetDto>
{
    public UpdatePetDtoValidator()
    {
        RuleFor(d => d.VolunteerId).NotEmpty();
        RuleFor(d => d.PetId).NotEmpty();
    }
}