using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public sealed record PetId : BaseEntityId<PetId>
{
    private PetId(Guid value) : base(value) {}
    public static PetId NewId() => new (Guid.NewGuid());
    public static PetId Create(Guid value) => new (value);
    public static implicit operator PetId (Guid v) => new (v);
    public static implicit operator Guid (PetId v)
    {
        ArgumentNullException.ThrowIfNull(v);
        return v.Value;
    }
}