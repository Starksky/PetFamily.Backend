using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species;

public class Breed : Entity<BreedId>
{
    private Breed(){}
    
    private Breed(BreedId id, string name) : base(id)
    {
        Name = name;
    }
    
    public static Result<Breed> Create(BreedId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Breed>("Breed name cannot be empty");

        return Result.Success(new Breed(id, name));
    }
    
    public string Name { get; private set; } = null!;
}