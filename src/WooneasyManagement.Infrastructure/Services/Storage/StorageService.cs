using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common.Interfaces.Storage;
using WooneasyManagement.Application.Files;

namespace WooneasyManagement.Infrastructure.Services.Storage;

public class StorageService(IStorage storage) : IStorageService
{
    public string StorageName => storage.GetType().Name;


    public Task<FileInfoDto> UploadAsync(string bucketOrMainDirectory, string path, IFormFile file)
        => storage.UploadAsync(bucketOrMainDirectory, path, file);

    public Task<List<FileInfoDto>> UploadAsync(string bucketOrMainDirectory, string path, IFormFileCollection files)
        => storage.UploadAsync(bucketOrMainDirectory, path, files);

    public Task DeleteAsync(string bucketOrMainDirectory, string path, string fileName)
        => storage.DeleteAsync(bucketOrMainDirectory,path, fileName);

    public Task DeleteAsync(string bucketOrMainDirectory, string path, List<string> fileNames)
        => storage.DeleteAsync(bucketOrMainDirectory,path, fileNames);

    public Task<List<FileInfoDto>> GetFiles(string bucketOrMainDirectory, string path)
        => storage.GetFiles(bucketOrMainDirectory,path);

    public Task<bool> HasFile(string bucketOrMainDirectory, string path, string fileName)
        => storage.HasFile(bucketOrMainDirectory,path, fileName);
}