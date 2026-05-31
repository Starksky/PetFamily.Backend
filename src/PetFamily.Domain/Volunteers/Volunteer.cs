using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public sealed class Volunteer : AuditEntity<VolunteerId>, ISoftDelete
{
    private bool _isDeleted = false;
    
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
    public Description? Description { get; private set; }
    public JobAge? JobAge { get; private set; }
    public VolunteerDetails? VolunteerDetails { get; private set; }
    

    public int GetCountSuccessPets() => _pets.Count(p => p.HelpStatus == HelpStatus.Success);
    public int GetCountNeedHomePets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedHome);
    public int GetCountNeedHelpPets() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedHelp);

    
    public void Delete() 
    {
        if (_isDeleted)
            return;
        
        //auto delete ef
        //_pets.ForEach(p => p.Delete());
        _isDeleted = true;
    }

    public void Restore()
    {
        if (!_isDeleted)
            return;
        
        //auto restore ef
        //_pets.ForEach(p => p.Restore());
        _isDeleted = false;
    }
    
    public void UpdateInfo(Description description, JobAge jobAge)
    {
        Description = description;
        JobAge = jobAge;
    }
    
    public UnitResult<Error> AddPet(Pet pet)
    {
        _pets.Add(pet);
        return Result.Success<Error>();
    }

    public Result<Pet, Error> GetPet(PetId id)
    {
        var find = _pets.FirstOrDefault(p => p.Id == id);
        if (find == null)
            return Errors.General.NotFound(id);
        return find;
    }
}