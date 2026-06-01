using PetFamily.Application.Providers;

namespace PetFamily.API.Common;

public class FormFileProcessor : IAsyncDisposable
{
    private List<FileUploadArgs> _fileUploadArgs = [];
    
    public IEnumerable<FileUploadArgs> Process(string bucketName, IFormFileCollection files)
    {
        foreach (var file in files)
            _fileUploadArgs.Add(new FileUploadArgs(bucketName, 
                file.FileName, 
                $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}", 
                file.OpenReadStream()));
        return _fileUploadArgs;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var file in _fileUploadArgs)
            await file.Stream.DisposeAsync();
    }
}