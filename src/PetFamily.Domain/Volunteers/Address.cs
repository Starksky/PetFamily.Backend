using CSharpFunctionalExtensions;

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
    
    public static Result<Address> Create(string postalCode, string city, string street, int buildingNumber, 
        int? buildingNumberTwo, int? apartmentNumber)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return Result.Failure<Address>("Postal code is required.");
        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Address>("City is required.");
        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Address>("Street is required.");
        if (buildingNumber > 1000 || buildingNumberTwo > 1000)
            return Result.Failure<Address>("Building number is greater than 1000");
        if (apartmentNumber > 1000)
            return Result.Failure<Address>("Apartment number is greater than 1000");

        return Result.Success(new Address(postalCode, city, street, buildingNumber, 
            buildingNumberTwo, apartmentNumber));
    }
}