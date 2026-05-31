namespace PetFamily.Domain.Volunteers;

public record PhotosContainer
{
    public IReadOnlyList<Photo> Photos { get; }

    private PhotosContainer() { }
    public PhotosContainer(IEnumerable<Photo> photos)
    {
        Photos = photos.ToList();
    }
}