using CSharpFunctionalExtensions;
using PetFamily.Domain.Contracts;
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

public class Pet : AuditEntity<PetId>, ISoftDelete
{
    private bool _isDeleted = false;
    private bool _isPublished = false;
    
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

    public bool IsPublished => _isPublished;
    
    
    //Required
    public string Name { get; private set; }
    public SpeciesId SpeciesId { get; private set; }
    public BreedId BreedId { get; private set; }
    

    //NotRequired
    public Address? Address { get; private set; }
    public PhotosContainer? PhotosContainer { get; private set; }
    public RequisitesContainer? RequisitesContainer { get; private set; }
    public Description? Description { get; private set; }
    public string? Color { get; private set; }
    
    public string? ContactPhone { get; private set; }
    public double? Height { get; private set; }
    public double? Weight { get; private set; }
    public bool? IsVaccinated { get; private set; }
    public bool? IsNeutered { get; private set; }
    
    public HealthStatus? HealthStatus { get; private set; }
    public HelpStatus? HelpStatus { get; private set; }
    
    
    public void Delete() => _isDeleted = true;
    public void Restore() => _isDeleted = false;
    public void Publish() => _isPublished = true;
    public void Unpublish() => _isPublished = false;

    public void Update(UpdatePetData data)
    {
        Name = data.Name ?? Name;
        SpeciesId = data.SpeciesId ?? SpeciesId;
        BreedId = data.BreedId ?? BreedId;
        Address = data.Address;
        Description = data.Description;
        Color = data.Color;
        ContactPhone = data.ContactPhone;
        Height = data.Height;
        Weight = data.Width;
        IsVaccinated = data.IsVaccinated;
        IsNeutered = data.IsNeutered;
        HealthStatus = data.HealthStatus;
        HelpStatus = data.HelpStatus;
    }
}