using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record Address
{
    public string PostalCode { get; }
    public string City { get; }
    public string Street { get; }
    public int BuildingNumber { get; }
    public int? BuildingNumberTwo { get; }
    public int? ApartmentNumber { get; }

    private Address(string postalCode, string city, string street, int buildingNumber, 
        int? buildingNumberTwo, int? apartmentNumber)
    {
        PostalCode = postalCode;
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
        BuildingNumberTwo = buildingNumberTwo;
        ApartmentNumber = apartmentNumber;
    }
    
    public static Result<Address, Error> Create(string postalCode, string city, string street, int buildingNumber, 
        int? buildingNumberTwo, int? apartmentNumber)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return Errors.General.Validation(nameof(PostalCode));
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.Validation(nameof(City));
        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.Validation(nameof(Street));
        
        if (buildingNumber > 1000 || buildingNumberTwo > 1000)
            return Errors.General.Validation(nameof(BuildingNumber));
        if (apartmentNumber > 1000)
            return Errors.General.Validation(nameof(ApartmentNumber));

        return new Address(postalCode, city, street, buildingNumber, 
            buildingNumberTwo, apartmentNumber);
    }
}