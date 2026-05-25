namespace PetFamily.Domain.Volunteers;

public record VolunteerDetails
{
    public IReadOnlyList<Requisite> Requisites { get; }
    public IReadOnlyList<SocialLink> SocialLinks { get; }

    private VolunteerDetails() { }
    public VolunteerDetails(IEnumerable<Requisite> requisites, IEnumerable<SocialLink> socialLinks)
    {
        Requisites = requisites.ToList();
        SocialLinks = socialLinks.ToList();
    }
}