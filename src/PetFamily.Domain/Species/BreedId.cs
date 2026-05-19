using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;

public sealed record BreedId : BaseEntityId<BreedId>
{
    private BreedId(Guid value) : base(value) { }
    public static BreedId Create(Guid id) => new (id);
}