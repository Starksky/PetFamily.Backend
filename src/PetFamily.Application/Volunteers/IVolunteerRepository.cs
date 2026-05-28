using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers;

public interface IVolunteersRepository
{
    public Task<Guid> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
    public Task<UnitResult<Error>> AddPetsAsync(VolunteerId id, IEnumerable<Pet> pets,
        CancellationToken cancellationToken = default);
    public Task<Guid> SaveAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
    public Task<Guid> DeleteAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
    public Task<Volunteer[]> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<Volunteer, Error>> GetByIdAsync(VolunteerId id, CancellationToken cancellationToken = default);
    public Task<Result<Volunteer, Error>> GetByEmailOrPhoneAsync(Email? email, Phone? phone, CancellationToken cancellationToken = default);

}