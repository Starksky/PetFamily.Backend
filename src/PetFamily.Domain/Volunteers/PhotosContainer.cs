using System.Text.Json;
using System.Text.Json.Serialization;

namespace PetFamily.Domain.Volunteers;

public record PhotosContainer
{
    public List<Photo> Photos { get; }
    
    [JsonConstructor]
    public PhotosContainer(List<Photo> photos)
    {
        Photos = photos;
    }
    
    public string Serialize() => JsonSerializer.Serialize(this);
    public static PhotosContainer? Deserialize(string json) 
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;

        PhotosContainer? value = JsonSerializer.Deserialize<PhotosContainer>(json);
        return value;
    }
}