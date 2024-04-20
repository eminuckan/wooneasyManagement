using Microsoft.AspNetCore.Http;
using WooneasyManagement.Domain.Entities.Common;
using File = WooneasyManagement.Domain.Entities.File;

namespace WooneasyManagement.Application.Files.Interfaces
{
    public interface IFileService<TEntity> where TEntity : File
    {
        Task<TEntity> UploadAsync(string bucketOrMainDirectory, string prefix, IFormFile file);
        Task<List<TEntity>> UploadAsync(string bucketOrMainDirectory, string prefix, IFormFileCollection files);
    }
}
