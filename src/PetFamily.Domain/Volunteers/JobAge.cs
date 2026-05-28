using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record JobAge
{
    public int Value { get; }
    private JobAge(int value) => Value = value;
    public static Result<JobAge, Error> Create(int value)
    {
        if (value is < 0 or > 100)
            return Errors.General.Validation(nameof(JobAge));
        return new JobAge(value);
    }
}