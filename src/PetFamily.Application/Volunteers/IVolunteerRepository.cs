using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Volunteer[]> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByIdAsync(VolunteerId id, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByEmailOrPhoneAsync(Email? email, Phone? phone, CancellationToken cancellationToken = default);
}