using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers;

public record Fio
{
    private Fio(string firstName, string lastName, string? patronymic)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }
    
    public string FirstName { get; }
    public string LastName { get; }
    public string? Patronymic { get; }


    public static Result<Fio> Create(string firstName, string lastName, string patronymic)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<Fio>("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<Fio>("Last name is required.");
        
        return Result.Success(new Fio(firstName, lastName, patronymic));
    }
}