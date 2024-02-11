using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common.Dtos;

namespace WooneasyManagement.Application.Interfaces.Storage;

public interface IStorage
{
    Task<List<FileInfoDto>> UploadAsync(string destination, string? path,
        IFormFileCollection files);

    Task DeleteAsync(string destination, string? path, string fileName);
    Task<List<FileInfoDto>> GetFiles(string destination, string? path);
    Task<bool> HasFile(string destination, string? path, string fileName);
}