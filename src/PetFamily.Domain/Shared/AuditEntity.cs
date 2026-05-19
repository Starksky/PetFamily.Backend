using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared;

public abstract class AuditEntity<TId>(TId id) : Entity<TId>(id), IHasTimestamps
    where TId : IComparable<TId>
{
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}