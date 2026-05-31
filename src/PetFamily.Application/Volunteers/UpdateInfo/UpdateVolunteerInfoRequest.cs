namespace PetFamily.Application.Volunteers.UpdateInfo;


public record UpdateVolunteerInfoRequest(string Description, int JobAge);
public record UpdateVolunteerInfoDto(Guid VolunteerId, UpdateVolunteerInfoRequest Request);