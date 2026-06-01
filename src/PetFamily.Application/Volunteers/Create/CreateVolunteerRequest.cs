namespace PetFamily.Application.Volunteers.Create;

public record CreateVolunteerRequest(FioDto Fio, string Email, string Phone);