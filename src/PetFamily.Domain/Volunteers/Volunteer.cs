using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public sealed class Volunteer : Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    
    private Volunteer(VolunteerId id) : base(id) { }
    
    public Volunteer(VolunteerId id, Fio fio, Email email, Phone phone) : base(id)
    {
        Fio = fio;
        Email = email;
        Phone = phone;
    }
    
    //Required
    public Fio Fio { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public IReadOnlyList<Pet> Pets => _pets;
    
    
    //NotRequired
    public string? Description { get; private set; }
    public int? JobAge { get; private set; }
    public VolunteerDetails? VolunteerDetails { get; private set; }
    

    public int GetCountSuccessPets() => _pets.Count(p => p.HelpStatus == HelpStatus.Success);
    public int GetCountNeedHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedHome);
    public int GetCountNeedHelpPets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedHelp);
}