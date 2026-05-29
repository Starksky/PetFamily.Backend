using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Extensions;

public static class ErrorExtensions
{
    public static Error WithPropertyName(this Error error, string propertyName)
    {
        var result = new Error(error.Code, error.Message, error.ErrorType, propertyName);
        return result;
    }
}