namespace PetFamily.Domain.Volunteers;

public record PetDetails
{
    private readonly List<Photo> _photos = [];
    private readonly List<Requisite> _requisites = [];
    public IReadOnlyList<Requisite> Requisites => _requisites;
    public IReadOnlyList<Photo> Photos => _photos;
}