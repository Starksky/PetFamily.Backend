namespace PetFamily.Application.Volunteers.AddPet;

public record AddPetRequest(string Name, string SpeciesId, string BreedId);
public record AddPetDto(Guid VolunteerId, AddPetRequest Request);