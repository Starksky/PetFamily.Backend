namespace PetFamily.Application.Volunteers.Create;

public record CreateVolunteerRequest(FioDto Fio, string Email, string Phone);
public record FioDto(string FirstName, string LastName, string? Patronymic);