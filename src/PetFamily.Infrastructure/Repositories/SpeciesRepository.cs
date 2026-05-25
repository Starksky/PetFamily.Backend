using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Species;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
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

    public async Task<Result<Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default)
    {
        var find = await _dbContext.Species.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (find == null)
            return Errors.General.NotFound(id);
        return find;
    }
}