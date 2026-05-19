using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers;

public record Photo
{

    private Photo(string pathToStorage )
    {
        PathToStorage = pathToStorage ;
    }
    
    public static Result<Photo> Create(string path)
    {
        if(string.IsNullOrWhiteSpace(path))
            return Result.Failure<Photo>("Path cannot be null or empty.");

        return Result.Success(new Photo(path));
    }
    
    public string PathToStorage { get; }
}