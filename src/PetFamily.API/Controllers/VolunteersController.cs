using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateInfo;
using PetFamily.Application.Volunteers.UpdatePet;

namespace PetFamily.API.Controllers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteerHandler handler,
        CancellationToken cancellationToken)
    {
        //using auto-validation

        var result = await handler.HandleAsync(request, cancellationToken);
        return result.ToOkResult();
    }

    [HttpPatch("{id:guid}/info")]
    public async Task<IActionResult> UpdateInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerInfoHandler handler,
        [FromBody] UpdateVolunteerInfoRequest request,
        [FromServices] IValidator<UpdateVolunteerInfoCommand> validator,
        CancellationToken cancellationToken)
    {
        //using auto-validation request

        var command = new UpdateVolunteerInfoCommand(id, request);

        //validation command
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.HasActionResult(out var actionResult))
            return actionResult;

        var result = await handler.HandleAsync(command, cancellationToken);
        return result.ToOkResult();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        [FromServices] IValidator<DeleteVolunteerRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerRequest(id);

        //validation request
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.HasActionResult(out var actionResult))
            return actionResult;

        var result = await handler.HandleAsync(request, cancellationToken);
        return result.ToOkResult();
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<IActionResult> AddPet(
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        [FromServices] IValidator<AddPetCommand> validator,
        CancellationToken cancellationToken)
    {
        //using auto-validation

        var command = new AddPetCommand(id, request);

        //validation command
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.HasActionResult(out var actionResult))
            return actionResult;

        var result = await handler.HandleAsync(command, cancellationToken);
        return result.ToOkResult();
    }

    [HttpPut("{id:guid}/pet/{idPet:guid}")]
    public async Task<IActionResult> UpdatePet(
        [FromRoute] Guid id,
        [FromRoute] Guid idPet,
        [FromBody] UpdatePetRequest request,
        [FromServices] UpdatePetHandler handler,
        [FromServices] IValidator<UpdatePetCommand> validator,
        CancellationToken cancellationToken)
    {
        //using auto-validation

        var command = new UpdatePetCommand(id, idPet, request);

        //validation command
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.HasActionResult(out var actionResult))
            return actionResult;

        var result = await handler.HandleAsync(command, cancellationToken);
        return result.ToOkResult();
    }
    
    /*[HttpPatch("{id:guid}/pet/{idPet:guid}/photos")]
    public async Task<IActionResult> UpdatePetPhotos(
        [FromRoute] Guid id,
        [FromRoute] Guid idPet,
        [FromForm] UpdatePetRequest request,
        [FromServices] UpdatePetHandler handler,
        [FromServices] IValidator<UpdatePetCommand> validator,
        CancellationToken cancellationToken)
    {
        //using auto-validation

        var command = new UpdatePetCommand(id, idPet, request);

        //validation command
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (validationResult.HasActionResult(out var actionResult))
            return actionResult;

        var result = await handler.HandleAsync(command, cancellationToken);
        return result.ToOkResult();
    }*/
}