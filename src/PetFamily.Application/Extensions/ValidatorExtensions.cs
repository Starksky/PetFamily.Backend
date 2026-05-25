using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((element, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(element);
            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }
}