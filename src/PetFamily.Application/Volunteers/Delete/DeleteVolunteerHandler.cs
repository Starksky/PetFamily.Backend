using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers.Delete;

public class DeleteVolunteerHandler
{
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    private readonly IVolunteersRepository _volunteerRepository;
    
    public DeleteVolunteerHandler(IVolunteersRepository volunteersRepository, ILogger<DeleteVolunteerHandler> logger)
    {
        _logger =  logger;
        _volunteerRepository =  volunteersRepository;
    }
    
    public async Task<Result<Guid, Error>> HandleAsync(DeleteVolunteerRequest request, CancellationToken token)
    {
        var volunteerResult = await _volunteerRepository.GetByIdAsync(request.Id, token);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        var volunteer = volunteerResult.Value;
        
        await _volunteerRepository.DeleteAsync(volunteer, token);
        
        _logger.LogInformation("Deleted Volunteer with Id {Id}", request.Id);
        return request.Id;
    }
}

public record DeleteVolunteerRequest(Guid Id);

public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
{
    public DeleteVolunteerRequestValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
} 