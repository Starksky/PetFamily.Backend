using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;

public interface IFileProvider
{
    public Task<Result<FileUploadResult, Error>> UploadFileAsync(FileUploadArgs uploadArgs, CancellationToken cancellationToken);
    public Task<(IEnumerable<FileUploadResult>, IEnumerable<Error>)> UploadFilesAsync(IEnumerable<FileUploadArgs> uploadsArgs, CancellationToken cancellationToken);
}