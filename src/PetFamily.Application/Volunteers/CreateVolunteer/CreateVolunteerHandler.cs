using CSharpFunctionalExtensions;
using PetFamily.Application.Shared;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler : IExecuteTaskHandler<CreateVolunteerRequest, Result<Guid, Error>>
{
    private readonly IVolunteersRepository _volunteersRepository;
    
    public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
    {
        _volunteersRepository = volunteersRepository;
    }
    
    public async Task<Result<Guid, Error>> ExecuteAsync(CreateVolunteerRequest createVolunteerRequest, CancellationToken cancellationToken = default)
    {
        var phone = Phone.Create(createVolunteerRequest.Phone).Value;
        var email = Email.Create(createVolunteerRequest.Email).Value;
        
        var findVolunteer = await _volunteersRepository.GetByEmailOrPhoneAsync(email, 
            phone, 
            cancellationToken);

        if (findVolunteer.IsSuccess)
            return Errors.General.IsAlreadyExists(nameof(Volunteer));
        
        var volunteerId = VolunteerId.NewId();
        
        var fio = Fio.Create(createVolunteerRequest.FirstName, 
                                        createVolunteerRequest.LastName, 
                                        createVolunteerRequest.Patronymic).Value;

        var volunteer = new Volunteer(volunteerId, fio, email, phone);
        var result = await _volunteersRepository.AddAsync(volunteer, cancellationToken);
        
        return result;
    }
}