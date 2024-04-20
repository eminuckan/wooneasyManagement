using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WooneasyManagement.Application.Common.Interfaces.Storage;
using WooneasyManagement.Application.Files;

namespace WooneasyManagement.Infrastructure.Services.Storage;

public class AmazonS3Storage : IAmazonS3Storage
{
    private readonly IAmazonS3 _client;

    public AmazonS3Storage(IConfiguration configuration)
    {
        var accessKey = configuration["Storage:S3:AccessKey"];
        var secretKey = configuration["Storage:S3:SecretKey"];
        _client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.EUCentral1);
    }

    public async Task<FileInfoDto> UploadAsync(string bucketOrMainDirectory, string path, IFormFile file)
    {
        await CreateBucketIfNotExistsAsync(bucketOrMainDirectory);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var uploadRequest = new PutObjectRequest
        {
            BucketName = bucketOrMainDirectory,
            Key = string.IsNullOrEmpty(path) ? fileName : $"{path}/{fileName}",
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };

        var response = await _client.PutObjectAsync(uploadRequest);

        if (response.HttpStatusCode == HttpStatusCode.OK)
            return new FileInfoDto
            {
                FileName = fileName,
                FilePath = String.IsNullOrEmpty(path) ? "/" : path,
                BucketOrMainDirectory = bucketOrMainDirectory,
                Storage = typeof(AmazonS3Storage).ToString()
            };

        throw new AmazonS3Exception($"Could not upload {fileName} to {bucketOrMainDirectory}.");
    }

    public async Task<List<FileInfoDto>> UploadAsync(string bucketOrMainDirectory, string path, IFormFileCollection files)
    {
        await CreateBucketIfNotExistsAsync(bucketOrMainDirectory);

        List<FileInfoDto> values = new();
        foreach (var file in files)
        {
            var fileInfo = await UploadAsync(bucketOrMainDirectory, path, file);
            values.Add(fileInfo);
        }

        return values;
    }

    public async Task DeleteAsync(string bucketOrMainDirectory, string path, string fileName)
    {
        await EnsureBucketExistsAsync(bucketOrMainDirectory);

        await _client.DeleteObjectAsync(bucketOrMainDirectory, string.IsNullOrEmpty(path) ? fileName : $"{path}/{fileName}");
    }

    public async Task DeleteAsync(string bucketOrMainDirectory, string path, List<string> fileNames)
    {
        foreach (var fileName in fileNames)
        {
            await DeleteAsync(bucketOrMainDirectory, path, fileName);
        }
    }

    public async Task<List<FileInfoDto>> GetFiles(string bucketOrMainDirectory, string path)
    {
        await EnsureBucketExistsAsync(bucketOrMainDirectory);

        var request = new ListObjectsV2Request
        {
            BucketName = bucketOrMainDirectory,
            MaxKeys = 50,
            Prefix = path
        };

        var response = await _client.ListObjectsV2Async(request);


        var files = response.S3Objects.Select(o => new FileInfoDto
        {
            FileName = o.Key.Split("/").Last(),
            BucketOrMainDirectory = o.BucketName,
            FilePath = String.IsNullOrEmpty(path) ? "/" : path,
            Storage = typeof(AmazonS3Storage).ToString()
        }).ToList();

        return files;
    }

    public async  Task<bool> HasFile(string bucketOrMainDirectory, string path, string fileName)
    {
        await EnsureBucketExistsAsync(bucketOrMainDirectory);

        try
        {
            var response = await _client.GetObjectMetadataAsync(bucketOrMainDirectory,
                string.IsNullOrEmpty(path) ? fileName : $"{path}/{fileName}");
            return response.HttpStatusCode == HttpStatusCode.OK ;
        }
        catch (AmazonS3Exception ex)
        {
            if (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            throw;

        }
    }

    private async Task EnsureBucketExistsAsync(string bucketName)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName);

        if (!bucketExists)
        {
            throw new AmazonS3Exception("Bucket not found");
        }
    }

    private async Task CreateBucketIfNotExistsAsync(string bucketName)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName);

        if (!bucketExists)
        {
            var bucketRequest = new PutBucketRequest
            {
                BucketName = bucketName,
                UseClientRegion = true
            };

            await _client.PutBucketAsync(bucketRequest);
        }
    }

}