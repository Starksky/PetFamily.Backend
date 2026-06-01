namespace PetFamily.Application.Volunteers.UpdatePet;

public record UpdatePetCommand(Guid VolunteerId, Guid PetId, UpdatePetRequest Request);
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