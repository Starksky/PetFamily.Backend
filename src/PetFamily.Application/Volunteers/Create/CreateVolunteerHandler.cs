using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.Create;

public class CreateVolunteerHandler : IExecuteTaskHandler<CreateVolunteerRequest, Result<Guid, Error>>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;

    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository, 
        ILogger<CreateVolunteerHandler> logger)
    {
        _logger = logger;
        _volunteersRepository = volunteersRepository;
    }
    
    public async Task<Result<Guid, Error>> HandleAsync(CreateVolunteerRequest createVolunteerRequest, CancellationToken cancellationToken = default)
    {
        var phone = Phone.Create(createVolunteerRequest.Phone).Value;
        var email = Email.Create(createVolunteerRequest.Email).Value;
        
        var findVolunteer = await _volunteersRepository.GetByEmailOrPhoneAsync(email, 
            phone, 
            cancellationToken);

        if (findVolunteer.IsSuccess)
            return Errors.General.IsAlreadyExists(nameof(Volunteer));
        
        var volunteerId = VolunteerId.NewId();
        
        var fio = Fio.Create(createVolunteerRequest.Fio.FirstName, 
                                        createVolunteerRequest.Fio.LastName, 
                                        createVolunteerRequest.Fio.Patronymic).Value;

        var volunteer = new Volunteer(volunteerId, fio, email, phone);
        var result = await _volunteersRepository.AddAsync(volunteer, cancellationToken);
        
        _logger.LogInformation("Created Volunteer {@id}", volunteer.Id.Value);
        
        return result;
    }
}