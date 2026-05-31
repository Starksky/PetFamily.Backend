using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.UpdateInfo;

public class UpdateVolunteerInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateVolunteerInfoHandler> _logger;

    public UpdateVolunteerInfoHandler(IVolunteersRepository volunteersRepository, 
        ILogger<UpdateVolunteerInfoHandler> logger)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
    }
    
    public async Task<Result<Guid, Error>> HandleAsync(UpdateVolunteerInfoDto dto, 
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteersRepository.GetByIdAsync(dto.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var volunteer = volunteerResult.Value;
        var description = Description.Create(dto.Request.Description).Value;
        var jobAge = JobAge.Create(dto.Request.JobAge).Value;
        
        volunteer.UpdateInfo(description, jobAge);
        
        await _volunteersRepository.SaveAsync(volunteer, cancellationToken);
        
        _logger.LogInformation("Volunteer with id {request.VolunteerId} has been updated.",  dto.VolunteerId);

        return volunteer.Id.Value;
    }
}