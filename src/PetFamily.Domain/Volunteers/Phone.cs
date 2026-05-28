using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record Phone
{
    public string Value { get; }
    private Phone(string value) => Value = value;
    public static Result<Phone, Error> Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return Errors.General.Validation(nameof(Phone));
        return new Phone(phone);
    }
}