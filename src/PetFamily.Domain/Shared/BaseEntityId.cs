namespace PetFamily.Domain.Shared;

public abstract record BaseEntityId<T>(Guid Value) : IComparable<T>
    where T : BaseEntityId<T>
{
    public int CompareTo(T? other)
        => other is null ? 1 : Value.CompareTo(other.Value);
}