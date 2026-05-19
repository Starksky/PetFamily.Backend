using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Volunteers;

public record SocialLink
{
    private SocialLink(string name, string url)
    {
        Name = name;
        Url = url;
    }
    
    public static Result<SocialLink> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<SocialLink>("Name is required");
        if (string.IsNullOrWhiteSpace(url))
            return Result.Failure<SocialLink>("Url is required");

        return Result.Success(new SocialLink(name, url));
    }
    
    public string Name { get; }
    public string Url { get; }
}