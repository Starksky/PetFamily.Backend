using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record Email
{
    public string Value { get; }
    private Email(string value) => Value = value;
    public static Result<Email, Error> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Errors.General.Validation(nameof(email));
        return new Email(email);
    }
}