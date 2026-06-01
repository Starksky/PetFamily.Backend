namespace PetFamily.Application.Providers;

public record FileUploadArgs(string BucketName, string FileName, string InternalName, Stream Stream);