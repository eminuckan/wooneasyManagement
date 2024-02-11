using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common.Dtos;
using WooneasyManagement.Application.Interfaces.Storage;

namespace WooneasyManagement.Infrastructure.Services.Storage;

public class LocalStorage(IWebHostEnvironment webHostEnvironment) : ILocalStorage
{
    public async Task<List<FileInfoDto>> UploadAsync(string destination,
        string? path, IFormFileCollection files)
    {
        var uploadPath = Path.Combine(webHostEnvironment.WebRootPath, destination, path);
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
                Path = path,
                BucketOrMainDirectory = destination
            });
        }

        return values;
    }

    public async Task DeleteAsync(string destination, string? path, string fileName)
    {
        string filePath = Path.Combine(webHostEnvironment.WebRootPath, destination, path, fileName);
        File.Delete(filePath);
    }

    public async Task<List<FileInfoDto>> GetFiles(string destination, string? path)
    {
        DirectoryInfo directory = new(Path.Combine(webHostEnvironment.WebRootPath, destination, path));
        List<FileInfoDto> files = directory.GetFiles().Select(f => new FileInfoDto
        {
            FileName = f.Name,
            Path = path,
            BucketOrMainDirectory = destination
        }).ToList();

        return files;
    }

    public async Task<bool> HasFile(string destination, string? path, string fileName)
    {
        string filePath = Path.Combine(webHostEnvironment.WebRootPath, destination, path,fileName);
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