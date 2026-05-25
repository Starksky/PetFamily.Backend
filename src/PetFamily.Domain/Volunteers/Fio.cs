using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

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


    public static Result<Fio, Error> Create(string firstName, string lastName, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.Validation(nameof(FirstName));
        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.Validation(nameof(LastName));
        
        return new Fio(firstName, lastName, patronymic);
    }
}