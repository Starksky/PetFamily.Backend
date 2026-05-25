using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetFamily.API.Response;
using PetFamily.Domain.Shared;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace PetFamily.API.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public async Task<IActionResult?> CreateActionResult(ActionExecutingContext context, 
        ValidationProblemDetails validationProblemDetails,
        IDictionary<IValidationContext, ValidationResult> validationResults)
    {
        List<Error> errors = new List<Error>();
        foreach (var result in validationProblemDetails.Errors)
            errors.AddRange(result.Value.Select(Error.Deserialize));
        var envelope = Envelope.Error(errors);
        return new BadRequestObjectResult(envelope);
    }
}