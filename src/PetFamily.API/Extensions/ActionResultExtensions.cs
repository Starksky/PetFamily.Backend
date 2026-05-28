using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Response;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Extensions;

public static class ActionResultExtensions
{
    public static ActionResult ToActionResult(this Error error)
        => new ObjectResult(Envelope.Error(error))
        {
            StatusCode = error.ErrorType switch
            {
                EErrorType.Validation => StatusCodes.Status400BadRequest,
                EErrorType.NotFound => StatusCodes.Status404NotFound,
                EErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            }
        };
    
    public static bool HasActionResult(this ValidationResult result, out ActionResult actionResult)
    {
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => Error.Deserialize(e.ErrorMessage));
            var envelope = Envelope.Error(errors);
            actionResult = new ObjectResult(envelope){StatusCode = StatusCodes.Status400BadRequest};
            return true;
        }
        
        actionResult = null!;
        return false;
    }

    public static ActionResult ToOkResult(this UnitResult<Error> result)
    {
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return new OkResult();
    }
    
    public static ActionResult ToCreatedResult(this UnitResult<Error> result, string location)
    {
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return new CreatedResult(location, null);
    }
    
    public static ActionResult ToOkResult<T>(this Result<T, Error> result)
    {
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return new OkObjectResult(Envelope.Ok(result.Value));
    }
    
    public static ActionResult ToCreatedResult<T>(this Result<T, Error> result, string url)
    {
        if (result.IsFailure)
            return result.Error.ToActionResult();
        return new CreatedResult(url, Envelope.Ok(result.Value));
    }
}