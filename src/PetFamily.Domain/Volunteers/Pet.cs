using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Domain.Volunteers;

public enum HelpStatus
{
    None,
    NeedHelp,
    NeedHome,
    Success
}

public enum HealthStatus
{
    None,
    Healthy,
    Sick,
    UnderTreatment
}

public class Pet : AuditEntity<PetId>
{
    private Pet(PetId id, string name, SpeciesId speciesId, BreedId breedId) : base(id)
    {
        Name = name;
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public static Result<Pet, Error> Create(PetId id, string name, SpeciesId speciesId, BreedId breedId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.Validation(nameof(Name));

        return new Pet(id, name, speciesId, breedId);
    }
    
    //Required
    public string Name { get; private set; }
    public SpeciesId SpeciesId { get; private set; }
    public BreedId BreedId { get; private set; }


    //NotRequired
    public Address? Address { get; private set; }
    public PetDetails? PetDetails { get; private set; }
    public string? Description { get; private set; }
    public string? Color { get; private set; }
    
    public string? ContactPhone { get; private set; }
    public double? Height { get; private set; }
    public double? Weight { get; private set; }
    public bool? IsVaccinated { get; private set; }
    public bool? IsNeutered { get; private set; }
    
    public HealthStatus? HealthStatus { get; private set; }
    public HelpStatus? HelpStatus { get; private set; }
}