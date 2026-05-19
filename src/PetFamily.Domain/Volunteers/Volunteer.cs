using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers;

public sealed class Volunteer : Entity<VolunteerId>
{
    private readonly List<Pet> _pets = [];
    
    private Volunteer(VolunteerId id) : base(id) { }
    
    private Volunteer(VolunteerId id, Fio fio, string email, string phone) : base(id)
    {
        Fio = fio;
        Email = email;
        Phone = phone;
    }
    
    public static Result<Volunteer> Create(VolunteerId id, Fio fio, string email, string phone)
    {
         if (string.IsNullOrWhiteSpace(email))
             return Result.Failure<Volunteer>("Email is required");
         if (string.IsNullOrWhiteSpace(phone))
             return Result.Failure<Volunteer>("Phone is required");

         var result = new Volunteer(id, fio, email, phone);
         return Result.Success(result);
    }
    
    //Required
    public Fio Fio { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public IReadOnlyList<Pet> Pets => _pets;
    
    
    //NotRequired
    public string? Description { get; private set; }
    public int? JobAge { get; private set; }
    public VolunteerDetails? VolunteerDetails { get; private set; }
    

    public int GetCountSuccessPets() => _pets.Count(p => p.HelpStatus == HelpStatus.Success);
    public int GetCountNeedHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedHome);
    public int GetCountNeedHelpPets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedHelp);
}