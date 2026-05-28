namespace PetFamily.Domain.Shared;

public interface ISoftDelete
{
    void Delete();
    void Restore();
}