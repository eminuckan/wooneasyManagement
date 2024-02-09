using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Interfaces.Storage;

namespace WooneasyManagement.Infrastructure.Services.Storage;

public class StorageService(IStorage storage) : IStorageService
{
    public string StorageName => storage.GetType().Name;

    public Task<List<(string fileName, string destination)>> UploadAsync(string destination, IFormFileCollection files)
        => storage.UploadAsync(destination, files);

    public Task DeleteAsync(string destination, string fileName)
        => storage.DeleteAsync(destination, fileName);

    public List<string> GetFiles(string destination)
        => storage.GetFiles(destination);

    public bool HasFile(string destination, string fileName)
        => storage.HasFile(destination, fileName);
}