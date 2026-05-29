using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;

public interface IFileProvider
{
    public Task<Result<string, Error>> UploadFileAsync(FileUploadArgs uploadArgs, CancellationToken cancellationToken);
}