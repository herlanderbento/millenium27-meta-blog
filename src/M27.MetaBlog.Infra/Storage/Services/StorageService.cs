using Amazon.S3;
using Amazon.S3.Model;
using M27.MetaBlog.Application.Interfaces;
using M27.MetaBlog.Infra.Storage.Configuration;
using Microsoft.Extensions.Options;

namespace M27.MetaBlog.Infra.Storage.Services;

public class StorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly StorageServiceOptions _options;
    
    public StorageService(
        IAmazonS3 s3Client,
        IOptions<StorageServiceOptions> options)
    {
        _s3Client = s3Client;
        _options = options.Value;
    }
    
    public async Task Delete(string filePath, CancellationToken cancellationToken)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _options.BucketName,
            Key = filePath
        };

        await _s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);
    }
    
    public async Task<string> Upload(
        string fileName,
        Stream fileStream,
        string contentType,
        CancellationToken cancellationToken)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = _options.BucketName,
            Key = fileName,
            InputStream = fileStream,
            ContentType = contentType
        };

        await _s3Client.PutObjectAsync(putRequest, cancellationToken);
        return fileName;
    }
}