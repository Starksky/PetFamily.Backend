using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record Description
{
    public string Value { get; }
    private Description(string value) => Value = value;
    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > Constants.MaxLengthHighText)
            return Errors.General.Validation(nameof(Description));
        return new Description(value);
    }
}