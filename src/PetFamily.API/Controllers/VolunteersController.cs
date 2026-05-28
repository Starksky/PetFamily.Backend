using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.Application.Volunteers.UpdateInfo;

namespace PetFamily.API.Controllers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVolunteerRequest request, 
        [FromServices] CreateVolunteerHandler handler,
        CancellationToken cancellationToken)
    {
        //using auto-validation
        
        var result = await handler.HandleAsync(request, cancellationToken);
        return result.ToOkResult();
    }

    [HttpPatch("{id:guid}/info")]
    public async Task<IActionResult> UpdateInfo([FromRoute] Guid id, 
        [FromServices] UpdateVolunteerInfoHandler handler,
        [FromBody] UpdateVolunteerInfoRequest request,
        [FromServices] IValidator<UpdateVolunteerInfoDto> validator,
        CancellationToken cancellationToken)
    {
        //using auto-validation request
        
        var dto = new UpdateVolunteerInfoDto(id, request);
        
        //validation dto
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);
        if (validationResult.HasActionResult(out var actionResult))
            return actionResult;
        
        var result = await handler.HandleAsync(dto, cancellationToken);
        return result.ToOkResult();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, 
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
}