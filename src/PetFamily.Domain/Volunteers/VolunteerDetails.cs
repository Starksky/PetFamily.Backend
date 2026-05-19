namespace PetFamily.Domain.Volunteers;

public record VolunteerDetails
{
    private readonly List<SocialLink> _socialLinks = [];
    private readonly List<Requisite> _requisites = [];
    public IReadOnlyList<Requisite> Requisites => _requisites;
    public IReadOnlyList<SocialLink> SocialLinks => _socialLinks;
}