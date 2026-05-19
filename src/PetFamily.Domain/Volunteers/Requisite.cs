using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers;

public record Requisite
{
    private Requisite(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public static Result<Requisite> Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Requisite>("Name is required");
        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Requisite>("Description is required");

        return Result.Success(new Requisite(name, description));
    }
    
    public string Name { get; }
    public string Description { get; }
}