using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptionsConditions<T, TElement?> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement?> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((element, context) =>
        {
            if (element == null)
                return;
            
            Result<TValueObject, Error> result = factoryMethod(element);
            if (result.IsSuccess)
                return;

            var key = context.DisplayName;
            var error = result.Error;
            
            if (key?.ToLower() != error.PropertyName?.ToLower())
                error = Error.Validation(error.Code, error.Message, $"{key}.{error.PropertyName}");
            
            context.AddFailure(error.Serialize());
        });
    }
    
    /*public static IRuleBuilderOptions<T, TElement> WithError<T, TElement>(this IRuleBuilderOptions<T, TElement> ruleBuilder, string fieldName = "field", string? errorMessage = null)
        => ruleBuilder.WithMessage(Errors.General.Validation(fieldName, errorMessage).Serialize());*/
}