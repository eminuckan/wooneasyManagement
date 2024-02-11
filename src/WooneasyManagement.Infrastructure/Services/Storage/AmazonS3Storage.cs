using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WooneasyManagement.Application.Common.Dtos;
using WooneasyManagement.Application.Interfaces.Storage;

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

    public async Task<List<FileInfoDto>> UploadAsync(string destination, string? path, IFormFileCollection files)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_client, destination);

        if (!bucketExists)
            try
            {
                var bucketRequest = new PutBucketRequest
                {
                    BucketName = destination,
                    UseClientRegion = true
                };


                await _client.PutBucketAsync(bucketRequest);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error creating bucket: {e.Message}");
                throw;
            }

        List<FileInfoDto> values = new();
        foreach (var file in files)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var uploadRequest = new PutObjectRequest
            {
                BucketName = destination,
                Key = string.IsNullOrEmpty(path) ? fileName : $"{path}/{fileName}",
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            var response = await _client.PutObjectAsync(uploadRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK)
                values.Add(new FileInfoDto
                {
                    FileName = fileName,
                    Path = path,
                    BucketOrMainDirectory = destination
                });
            else
                Console.WriteLine($"Could not upload {fileName} to {destination}.");
        }

        return values;
    }

    public async Task DeleteAsync(string destination, string? path, string fileName)
    {
        await EnsureBucketExistsAsync(destination);

        await _client.DeleteObjectAsync(destination, string.IsNullOrEmpty(path) ? fileName : $"{path}/{fileName}");
    }

    public async Task<List<FileInfoDto>> GetFiles(string destination, string? path)
    {
        await EnsureBucketExistsAsync(destination);

        var request = new ListObjectsV2Request
        {
            BucketName = destination,
            MaxKeys = 50,
            Prefix = path
        };

        var response = await _client.ListObjectsV2Async(request);


        var files = response.S3Objects.Select(o => new FileInfoDto
        {
            FileName = o.Key.Split("/").Last(),
            BucketOrMainDirectory = o.BucketName,
            Path = path
        }).ToList();

        return files;
    }

    public async  Task<bool> HasFile(string destination, string? path, string fileName)
    {
        await EnsureBucketExistsAsync(destination);

        try
        {
            var response = await _client.GetObjectMetadataAsync(destination,
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
}