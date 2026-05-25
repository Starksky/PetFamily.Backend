using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record SocialLink
{
    private SocialLink(string name, string url)
    {
        Name = name;
        Url = url;
    }
    
    public static Result<SocialLink, Error> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.Validation(nameof(Name));
        if (string.IsNullOrWhiteSpace(url))
            return Errors.General.Validation(nameof(Url));

        return new SocialLink(name, url);
    }
    
    public string Name { get; }
    public string Url { get; }
}