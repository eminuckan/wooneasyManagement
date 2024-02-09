using Microsoft.AspNetCore.Http;

namespace WooneasyManagement.Application.Interfaces.Storage;

public interface IStorage
{
    Task<List<(string fileName, string destination)>> UploadAsync(string destination, IFormFileCollection files);
    Task DeleteAsync(string destination, string fileName);
    List<string> GetFiles(string destination);
    bool HasFile(string destination, string fileName);
}