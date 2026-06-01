namespace PetFamily.Application.Volunteers.UpdatePet;

public record AddressDto(
    string PostalCode,
    string City,
    string Street,
    int BuildingNumber,
    int? BuildingNumberTwo,
    int? ApartmentNumber);