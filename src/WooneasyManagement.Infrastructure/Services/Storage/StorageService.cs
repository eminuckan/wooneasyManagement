using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common.Dtos;
using WooneasyManagement.Application.Interfaces.Storage;

namespace WooneasyManagement.Infrastructure.Services.Storage;

public class StorageService(IStorage storage) : IStorageService
{
    public string StorageName => storage.GetType().Name;


    public Task<List<FileInfoDto>> UploadAsync(string destination, string? path, IFormFileCollection files)
        => storage.UploadAsync(destination, path, files);

    public Task DeleteAsync(string destination, string? path, string fileName)
        => storage.DeleteAsync(destination,path, fileName);

    public Task<List<FileInfoDto>> GetFiles(string destination, string? path)
        => storage.GetFiles(destination,path);

    public Task<bool> HasFile(string destination, string? path, string fileName)
        => storage.HasFile(destination,path, fileName);
}