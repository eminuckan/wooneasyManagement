using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Files;

namespace WooneasyManagement.Application.Common.Interfaces.Storage;

public interface IStorage
{
    Task<FileInfoDto> UploadAsync(string bucketOrMainDirectory, string path, IFormFile file);
    Task<List<FileInfoDto>> UploadAsync(string bucketOrMainDirectory, string path,
        IFormFileCollection files);

    Task DeleteAsync(string bucketOrMainDirectory, string path, string fileName);
    Task DeleteAsync(string bucketOrMainDirectory, string path, List<string> fileNames);
    Task<List<FileInfoDto>> GetFiles(string bucketOrMainDirectory, string path);
    Task<bool> HasFile(string bucketOrMainDirectory, string path, string fileName);
}