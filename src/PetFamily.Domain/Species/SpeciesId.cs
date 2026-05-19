using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Species;

public record SpeciesId : BaseEntityId<SpeciesId>
{
    private SpeciesId(Guid value) : base(value) {}
    public static SpeciesId Create(Guid value) => new (value);
    public static implicit operator Guid(SpeciesId id) => id.Value;
}