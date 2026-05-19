using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public sealed record VolunteerId : BaseEntityId<VolunteerId>
{
    private VolunteerId(Guid value) : base(value) {}
    public static VolunteerId Create(Guid id) => new (id);
    public static implicit operator VolunteerId (Guid v) => new (v);
    public static implicit operator Guid (VolunteerId v)
    {
        ArgumentNullException.ThrowIfNull(v);
        return v.Value;
    }
}