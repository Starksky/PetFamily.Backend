namespace PetFamily.Application.Volunteers.AddPet;

public record AddPetRequest(string Name, string SpeciesId, string BreedId);
public record AddPetCommand(Guid VolunteerId, AddPetRequest Request);