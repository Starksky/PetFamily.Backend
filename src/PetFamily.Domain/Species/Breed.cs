using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;

public class Breed : Entity<BreedId>
{
    private Breed(){}
    
    private Breed(BreedId id, string name) : base(id)
    {
        Name = name;
    }
    
    public static Result<Breed, Error> Create(BreedId id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.Validation(nameof(Name));

        return new Breed(id, name);
    }
    
    public string Name { get; private set; } = null!;
}