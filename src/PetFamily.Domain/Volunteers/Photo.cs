using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record Photo
{

    [JsonConstructor]
    private Photo(string pathToStorage )
    {
        PathToStorage = pathToStorage ;
    }
    
    public static Result<Photo, Error> Create(string path)
    {
        if(string.IsNullOrWhiteSpace(path))
            return Errors.General.Validation(nameof(path));

        return new Photo(path);
    }
    
    public string PathToStorage { get; }
}