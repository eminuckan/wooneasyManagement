using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common.Interfaces.Storage;
using WooneasyManagement.Application.Files;

namespace WooneasyManagement.Infrastructure.Services.Storage;

public class LocalStorage(IWebHostEnvironment webHostEnvironment) : ILocalStorage
{
    public Task<FileInfoDto> UploadAsync(string bucketOrMainDirectory, string path, IFormFile file)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FileInfoDto>> UploadAsync(string bucketOrMainDirectory,
        string path, IFormFileCollection files)
    {
        var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, bucketOrMainDirectory, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<FileInfoDto> values = new();
        foreach (var file in files)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            await CopyFileAsync(Path.Combine(uploadPath, fileName), file);
            values.Add(new FileInfoDto()
            {
                FileName = fileName,
                FilePath = String.IsNullOrEmpty(path) ? "/" : path,
                BucketOrMainDirectory = bucketOrMainDirectory,
                Storage = typeof(LocalStorage).ToString()
            });
        }

        return values;
    }

    public async Task DeleteAsync(string bucketOrMainDirectory, string path, string fileName)
    {
        string filePath = Path.Combine(webHostEnvironment.WebRootPath, bucketOrMainDirectory, path, fileName);
        File.Delete(filePath);
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
        DirectoryInfo directory = new(Path.Combine(webHostEnvironment.WebRootPath, bucketOrMainDirectory, path));
        List<FileInfoDto> files = directory.GetFiles().Select(f => new FileInfoDto
        {
            FileName = f.Name,
            FilePath = String.IsNullOrEmpty(path) ? "/" : path,
            BucketOrMainDirectory = bucketOrMainDirectory,
            Storage = typeof(LocalStorage).ToString()
        }).ToList();

        return files;
    }

    public async Task<bool> HasFile(string bucketOrMainDirectory, string path, string fileName)
    {
        string filePath = Path.Combine(webHostEnvironment.WebRootPath, bucketOrMainDirectory, path,fileName);
        return File.Exists(filePath);
    }

    public async Task<bool> CopyFileAsync(string fullPath, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,
                1024 * 1024, false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}