using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }
    
    public async Task<Result<string, Error>> UploadFileAsync(FileUploadArgs uploadArgs, CancellationToken cancellationToken)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(uploadArgs.BucketName);
            
            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
            if (!bucketExist)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(uploadArgs.BucketName);
                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
            
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(uploadArgs.BucketName)
                .WithStreamData(uploadArgs.Stream)
                .WithObjectSize(uploadArgs.Stream.Length)
                .WithObject(uploadArgs.FileName);
            
            var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            
            return result.ObjectName;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error uploading file");
            return Error.Failure("upload.file.error", "Error uploading file");
        }
    }
}