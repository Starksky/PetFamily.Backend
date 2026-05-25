namespace PetFamily.Domain.Volunteers;

public record PetDetails
{
    public IReadOnlyList<Requisite> Requisites { get; }
    public IReadOnlyList<Photo> Photos { get; }

    private PetDetails() { }
    public PetDetails(IEnumerable<Requisite> requisites, IEnumerable<Photo> photos)
    {
        Requisites = requisites.ToList();
        Photos = photos.ToList();
    }
}