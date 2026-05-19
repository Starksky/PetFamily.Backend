using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public sealed record PetId : BaseEntityId<PetId>
{
    private PetId(Guid value) : base(value) {}
    public static PetId Create(Guid value) => new (value);
}