using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.UpdateInfo;

public class UpdateVolunteerInfoHandler
{
    private readonly IVolunteersRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerInfoHandler> _logger;

    public UpdateVolunteerInfoHandler(IVolunteersRepository volunteersRepository, 
        ILogger<UpdateVolunteerInfoHandler> logger)
    {
        _logger = logger;
        _volunteerRepository = volunteersRepository;
    }
    
    public async Task<Result<Guid, Error>> HandleAsync(UpdateVolunteerInfoDto dto, 
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var volunteer = volunteerResult.Value;
        var description = Description.Create(dto.Request.Description).Value;
        var jobAge = JobAge.Create(dto.Request.JobAge).Value;
        
        volunteer.UpdateInfo(description, jobAge);
        
        await _volunteerRepository.SaveAsync(volunteer, cancellationToken);
        
        _logger.LogInformation("Volunteer with id {request.VolunteerId} has been updated.",  dto.Id);

        return volunteer.Id.Value;
    }
}