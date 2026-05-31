namespace PetFamily.Domain.Volunteers;

public record RequisitesContainer
{
    public IReadOnlyList<Requisite> Requisites { get; }

    private RequisitesContainer() { }
    public RequisitesContainer(IEnumerable<Requisite> requisites)
    {
        Requisites = requisites.ToList();
    }
}