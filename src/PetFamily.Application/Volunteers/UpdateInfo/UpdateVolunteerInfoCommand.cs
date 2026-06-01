namespace PetFamily.Application.Volunteers.UpdateInfo;


public record UpdateVolunteerInfoRequest(string Description, int JobAge);
public record UpdateVolunteerInfoCommand(Guid VolunteerId, UpdateVolunteerInfoRequest Request);