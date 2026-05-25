using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Volunteers.CreateVolunteer;

namespace PetFamily.API.Controllers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVolunteerRequest request, 
        [FromServices] CreateVolunteerHandler handler,
        /*[FromServices] CreateVolunteerRequestValidator validator,*/
        CancellationToken cancellationToken = default)
    {
        /*
        if ((await validator.ValidateAsync(request, cancellationToken))
            .HasValidationResult(out var validationResult))
            return validationResult;
            */
        
        var result = await handler.ExecuteAsync(request, cancellationToken);
        return result.ToOkResult();
    }
}