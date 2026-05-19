namespace PetFamily.Domain.Shared;

public interface IHasTimestamps
{
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    
    public void Update();
}