using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;

namespace PetFamily.Application.Species;

public interface ISpeciesRepository
{
    Task<Domain.Species.Species[]> GetAll(CancellationToken cancellationToken = default);
    Task<Guid> Create(Domain.Species.Species entity, CancellationToken cancellationToken = default);
    Task<Result<Domain.Species.Species, Error>> GetById(SpeciesId id, CancellationToken cancellationToken = default);
}