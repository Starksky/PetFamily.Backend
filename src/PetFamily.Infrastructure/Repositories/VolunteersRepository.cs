using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Repositories;


public class VolunteersRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public VolunteersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }
    
    public async Task<UnitResult<Error>> AddPetsAsync(VolunteerId id, IEnumerable<Pet> pets, CancellationToken cancellationToken = default)
    {
        var volunteerResult = await GetByIdAsync(id, cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var volunteer = volunteerResult.Value;
        
        var addResult = volunteer.AddPets(pets);
        
        if (addResult.IsFailure)
            return addResult.Error;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<Error>();
    }
    
    public async Task<Guid> SaveAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Attach(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return volunteer.Id;
    }
    
    public async Task<Guid> DeleteAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Volunteers.Remove(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return volunteer.Id;
    }

    public async Task<Volunteer[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Volunteers.ToArrayAsync(cancellationToken);
    }
    
    public async Task<Result<Volunteer, Error>> GetByIdAsync(VolunteerId id, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: cancellationToken);
        
        if (volunteer == null)
            return Errors.General.NotFound(id);
        
        return volunteer;
    }
    
    public async Task<Result<Volunteer, Error>> GetByEmailOrPhoneAsync(Email? email, Phone? phone, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Email == email 
                                      || v.Phone == phone, cancellationToken: cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound();

        return volunteer;
    }
    
    public async Task<Result<Volunteer, Error>> GetByPhoneAsync(Phone phone, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Phone == phone, cancellationToken: cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound();

        return volunteer;
    }
}