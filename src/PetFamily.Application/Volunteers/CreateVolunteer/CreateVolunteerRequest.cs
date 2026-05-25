namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(string FirstName, string LastName, string? Patronymic, string Email, string Phone);