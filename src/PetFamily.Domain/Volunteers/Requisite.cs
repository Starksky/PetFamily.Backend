using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record Requisite
{
    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public static Result<Requisite, Error> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.Validation(nameof(Name));
        if (string.IsNullOrWhiteSpace(description))
            return Errors.General.Validation(nameof(Description));

        return new Requisite(name, description);
    }
    
    public string Name { get; }
    public string Description { get; }
}