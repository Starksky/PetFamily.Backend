using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species;

public class Species : Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    
    private Species(){}
    private Species(SpeciesId id, string name) : base(id)
    {
        Name = name;
    }
    
    public static Result<Species> Create(SpeciesId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Species>("Species name cannot be empty");
        return Result.Success(new Species(id, name));
    }
    
    //Required
    public string Name { get; private set; } = null!;
    public IReadOnlyList<Breed> Breeds => _breeds;

    
    public Result AddBreed(Breed breed)
    {
        _breeds.Add(breed);
        return Result.Success();
    }
}