using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Repositories;

public class SpeciesRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public SpeciesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Species[]> GetAll(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Species.ToArrayAsync(cancellationToken);
    }

    public async Task<Guid> Create(Species entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task<Result<Species>> GetById(SpeciesId id, CancellationToken cancellationToken = default)
    {
        var find = await _dbContext.Species.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (find == null)
            return Result.Failure<Species>("Species not found");
        return Result.Success(find);
    }
}