using PetFamily.Domain.Species;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Domain.Contracts;

public record UpdatePetData(
    string? Name, 
    SpeciesId? SpeciesId, 
    BreedId? BreedId,
    Address? Address,
    Description? Description,
    string? Color,
    string? ContactPhone,
    double? Height,
    double? Width,
    bool? IsVaccinated,
    bool? IsNeutered,
    HealthStatus? HealthStatus,
    HelpStatus? HelpStatus);